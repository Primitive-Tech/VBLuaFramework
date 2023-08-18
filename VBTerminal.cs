using System;using NLua;
using VBLua.Core;
using System.ComponentModel;
using System.Configuration;
using static VBLua.Core.Utilities;
using System.Speech.Synthesis;


namespace VBLua.IDE
{
    [ToolboxItem(true), DesignTimeVisible(true)]
    public partial class VBTerminal : UserControl, IComponent, IDisposable, IContainerControl
    {
        private Lua? engine; private SpeechSynthesizer? tts; private Lua ScriptState = new(); private Script? script;
        private Server? server; RichTextBox rtb = new();
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string[] Headline = new string[2] { "LUA_Terminal[V1.1.2]\n\r", ">" }; private string[] InputContent = new string[] { "=>|" }; public string? errorCode; private int z = 0;
        //############################################################################################################################
        public VBTerminal()
        {
            InitializeComponent();
        }
        //############################################################################################################################
        private void Terminal_Load(object sender, EventArgs e) { KeyEventArgs enter = new(Keys.Enter); this.ControlInputLine(commandLine, enter); }
        [
        Category("Scripting-Rescources"),
        Description("Handles Scripting Functionalities.")
        ]
        private bool _Editing_Mode = false; private bool _AudioOutput = false; private string myScript = ""; private string? user;
        [
        Category("Side Functions"),
        Description("Functionalities & More..")
        ]
        public bool Editing_Mode
        {
            get { return _Editing_Mode; }
            set { _Editing_Mode = value; commandLine.Multiline = value; Invalidate(); }
        }
        [Category("Scripting"),
        Description("VBLua_Bindings and Settings.")
        ]
        public string MyScript
        {
            get => myScript; set => myScript = @value + " return true, response,respCode";
        }
        public string? User
        {
            get => user; set => user = value;
        }
        // ############################################################################################################################
        [
        Category("Console_Specifications"),
        Description("Handles Windows- and Scripting- and PrimitiveServer-CommandLine.")
        ]
        private bool useDefaultEnvironment = false;
        public bool AudioOutput
        {
            get { return _AudioOutput; }
            set { _AudioOutput = value; }
        }
        private string[]? Input
        {
            get
            {
                switch (commandLine.Text is not null && commandLine.Text != "")
                {
                    case true:
                        return commandLine.Text?.Substring(3)?.Split(' ');
                    case false:
                        return new string[] { "", "", "", "" };
                }
            }
        }
        public string? Environment
        {
            get { return Input?.First(); }
        }
        [SettingsDescription("Uses your Variable instead of Typing in Console.")]
        public bool UseDefaultEnvironment
        {
            get { return useDefaultEnvironment; }
            set { useDefaultEnvironment = value; }
        }
        //###############################################################################################################
        //------------------------------------ IN -/ Output -------------------------------------------
        private object[] _Output = { false, "" }; private object _ResponseBody;
        private object[] Output { get => _Output; set => _Output = value; }
        private string? StandartOutput { get => Output?[1].ToString(); }
        private object ResponseBody { get => _ResponseBody; set => _ResponseBody = value; }
        private string? StandartInput { get => Input?[1]; }
        protected (string?, object?)[] Args { get => new (string?, object?)[] { (Input?[2]?.Split('=')?[0], Input?[2]?.Split('=')?[1]) }; }
        // 
        private bool PerformCommand()
        {
            List<(string, object)> args = new() { ("method", "GET"), ("namespace", Environment), ("userCommand", "cmd"), ("commandStatement", StandartInput), ("this", this) };

            // ----------------------- Execution --------------------- Execution --------------------                              
            switch (Environment)
            {
                case "CMD" when StandartInput != null:
                    {
                        script = new("f3bg.png",args, false);
                        script.MyNamespaces = new string[] { "Newtonsoft.Json", "System.Data" };
                        script.Start(false, false); Output = script.Output;
                        if (script.StatusResponse) { errorCode = "Internal Script Failure"; }
                        else { errorCode = (string)script.Output[1]; }
                        Say();
                        return script.StatusResponse;
                    }

                case "DB" when Args != null:
                    {
                        script = new(File.ReadAllText(StandartInput?.ToLower() + ".vbl"),args, false);
                        script.MyNamespaces = new string[] { "Newtonsoft.Json", "User", "Primitive_Server.Server" };
                        script.Start(false, false); Output = script.Output; ResponseBody = script.Output[1]; ScriptState = script.Engine;
                        if (script.StatusResponse) { errorCode = "Internal Script Failure"; }
                        else { errorCode = (string)script.Output[0]; }
                        Say();
                        return script.StatusResponse;  //var scriptFunc = lua[Environment] as LuaFunction;var script.StatusResponse = scriptFunc.Call(args);            
                    }
                case "DO" when StandartInput != null:
                    {
                        script = new(File.ReadAllText(StandartInput?.ToLower() + ".vbl"), args, false);
                        script.MyNamespaces = new string[] { "Newtonsoft.Json", "User", "System.Net", "System.Net.HTTP", " Primitive_Server.Server" };
                        ResponseBody = script.Start(false, false); Output = script.Output; ResponseBody = script.Output[1]; ScriptState = script.Engine;
                        if (script.StatusResponse) { errorCode = "Internal Script Failure"; }
                        else { errorCode = (string)script.Output[0]; }
                        Say();
                        return script.StatusResponse;  //var scriptFunc = lua[Environment] as LuaFunction;var script.StatusResponse = scriptFunc.Call(args);            
                    }

                case "SAY" when StandartInput != null:
                    {
                        Output = new object[] { true, StandartInput }; ResponseBody = StandartInput;
                        if (AudioOutput == false) { AudioOutput = true; Say(); AudioOutput = false; }
                        else { Say(); }
                        return true;
                    }
                case "GET" when StandartInput != null:
                    {
                        Output = new object[] { true, StandartInput };
                        if (ScriptState[StandartInput] != null) { printToOutputLine(Environment + "StandartInput " + " => " + ScriptState[StandartInput].ToString()); }
                        return true;
                    }
                case "" or null: { Output = new object[] { false, "Failed" }; return false; }
                case "++" or null:
                    {
                        this.commandLine.Multiline = true; this.commandLine.Height += this.commandLine.Height * 2;
                        this.commandLine.Location = new Point(this.commandLine.Location.X, this.commandLine.Location.Y - this.commandLine.Height * 2); this.commandLine.Invalidate(); Output = new object[] { false, "Go Editing-Mode" }; return true;
                    }
            }
            return false;
        }


        private void resetInputLine() { commandLine.Lines = InputContent; commandLine.DeselectAll(); commandLine.SelectionStart = 3; }
        private void ControlInputLine(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Environment != null)
            {
                PerformCommand();
                printToOutputLine(Environment + "::" + "\r\n" + ResponseBody.ToString());
                resetInputLine();
            }
        }
        public void Writing(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                if (commandLine.SelectionStart <= 3 || commandLine.SelectedText.Contains("|") || commandLine.SelectedText.Contains("=") || commandLine.SelectedText.Contains(">"))
                {
                    resetInputLine();
                }
                if (e.KeyChar == (char)Keys.Back | e.KeyChar == (char)Keys.Back && commandLine.SelectionStart <= 3) { e.Handled = true; }
            }
            else if (commandLine.SelectedText.Contains("|") || commandLine.SelectedText.Contains("=") || commandLine.SelectedText.Contains(">"))
            {
                resetInputLine();
            }
        }    
        //-------------------------------------------------------------------------------------------------------------
        public void printToOutputLine(string str)
        {
            if (str != null && str.Any())
            {
                outputLine.Text += str; outputLine.Text += "\r\n" + ">>";
            }
            else { outputLine.Text += ">>Error while accessing Namespace/Input... " + Environment + ">>" + "\t"; outputLine.Text += "\r\n" + ">>"; }
        }
        public void LogToOutputLine(IEnumerable<string>? queue)
        {
            if (queue != null && queue.Any())
            {
                foreach (string x in queue)
                {
                    if (x != null)
                    {
                        printToOutputLine(Environment + "::" + "\r\n" + ResponseBody.ToString());
                    }
                    else { outputLine.Text += ">>Error while accessing Namespace/Input... " + Environment + ">>" + "\t"; outputLine.Text += "\r\n" + ">>"; }
                }
            }
        }
        // ################################################################################################################
        private Color color = Color.Teal; private Color hovercolor = Color.FromArgb(0, 0, 140);
        private Color clickcolor = Color.FromArgb(160, 180, 200); ContentAlignment align = ContentAlignment.TopCenter;
        private int textX = 6; private int textY = -20; Padding padding1 = new(1, 1, 1, 1); Padding padding2 = new(1, 0, 0, 1);
        [
        Category("ConsoleUI"),
        Description("Appearance of the Console.")
        ]
        public Size TerminalSize
        {
            get { return Size; }
            set { Size = value; Invalidate(); }
        }
        public ContentAlignment TextAlignment { get { return align; } set { align = value; Invalidate(); } }
        public Color BZBackColor
        {
            get { return color; }
            set { color = value; Invalidate(); }
        }
        public FlatStyle FlatStyle { get; private set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); StringFormat style = new StringFormat();
            style.Alignment = StringAlignment.Near; style.FormatFlags = new();
            style.FormatFlags = StringFormatFlags.LineLimit; style.FormatFlags = StringFormatFlags.NoClip;
            style.FormatFlags = StringFormatFlags.DisplayFormatControl; style.LineAlignment = StringAlignment.Near;
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), ClientRectangle, style);
        }

        private void Say()
        {
            if (AudioOutput && StandartOutput != null)
            {
                tts = new SpeechSynthesizer(); tts.SetOutputToDefaultAudioDevice();
                Prompt content = new Prompt(StandartOutput); tts.Rate = 2; tts.Volume = 100; // tts.SelectVoiceByHints(gender, speakerAge);              
                tts.SpeakAsync(content);
            }
        }
    }
}