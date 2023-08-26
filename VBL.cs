using System.IO;
using static VBLua.Core.Utilities;using System.Linq;
using NLua;using VBLua.Core;
using System;
using System.Resources;
using VB.Lua.GUIController;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Newtonsoft.Json.Linq;

namespace VBLua.Core
{
    public static class Syntaxer
    {
        static Script mes; public static string? code="";

        public static string? Convert(string inputCode) {
            mes = new("Syntaxer" + ".lua", new() {("input",inputCode) }); mes.isLocalFile = true;
            mes.Start();
            return mes.Body["output"]?.IntToStr();
        }
    
    } 
    
    //========= ======================================================================================================================== ==========================
    ////////////////// <= VBL_Textbox =>  //////////////////////// <= VBL_Textbox =>  /////////////////////////
    //========= ======================================================================================================================== ==========================

    public class Input: RichTextBox
    {
        private Form parent; public Form Parent { get => parent;  }
        public string syntax="lua";
        public void Init()
        {
            // CodeEditor
            this.BackColor = SystemColors.HighlightText;this.Cursor = Cursors.IBeam; 
            this.Size = new Size(450, 100); this.ClientSize = new Size(450,100);
            this.Name = "CodeEditor"; this.BringToFront(); 
            this.ShowSelectionMargin = true;
        }
        public Input() {
            if (Parent != null) { } else {  this.parent = new Form();this.parent.Location = new(1920/2,1080/2);this.parent.Size = new(960,700);
               Init(); this.VisibleChanged += CodeEdit_VisibleChanged; this.KeyDown += CodeEdit_TextChanged; this.Visible = true; this.Show();
                if (this.Text!=""){ this.SelectionStart =this.TextLength; SyntaxHighlighterNET.HighlightOwnCode(this, syntax); }
            }
        }
        bool StartingSeq = true;
        //--Events
        private void CodeEdit_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.BringToFront();
                this.Text = "local function ccc()";// this.Text=PickedElement.eventCode; 
                RTBExtensions.InitializeCodeCompletion(this);//PickedElement.createCode("--");
            }
            else { }
        }
        private void CodeEdit_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) { if (syntax!="lua") { RTBExtensions.AutoIndentVB(this); } SyntaxHighlighterNET.HighlightOwnCode(this, syntax); StartingSeq = false; }
            else { RTBExtensions.ShowCodeSuggestions(this); }
        }
    }

    //========= ======================================================================================================================== ==========================
    ////////////////// <= VBL_Script =>  //////////////////////// <= VBL_Script =>  /////////////////////////
    //========= ======================================================================================================================== ==========================

    public class Script
    {
        public Lua Engine; public string DataPath = "data/"; public string File { get => DataPath + Name; } public bool isLocalFile = false; public bool _UseOwnSyntax; public string[] CodeSource;

        public string Name; public string Method = "GET"; public string Typ= ".lua"; public bool UseOwnSyntax { get { return _UseOwnSyntax; } set { _UseOwnSyntax = value; } }

        public User? User; public LuaFunction Request; public Dictionary<string, object> ScriptArgs = new(){ { "method", "GET" } }; public List<KeyValuePair<string, object>> Dataframe = new(); public string ErrorCode; public object Erg;

       
        public LuaTable Body => (LuaTable)Output[1]; 
        private object[] respBody; public bool Succeed;public string Code; 

        public object[] Output; public object[] Response; public bool StatusResponse; 

        public string[] MyNamespaces = { "VBLib", "System.IO", "Newtonsoft.Json" };

        public object[] RespBody
        {
            get
            {
                return respBody;
            }
            set
            {
                respBody = value;
            }
        }
        public object Answer => Engine["response"];

        //========= ======================================================================================================================== ==========================
        public Script() { }
        public Script(string name, List<(string, object)> args)
        {
            Name = name;
            Engine = new(); insertParams(args);
            this.Code= System.IO.File.ReadAllText(File);
            Output = Engine.DoFile(File); RespBody = Output;
        }

        public Script(string name, List<(string, object)> args, bool asFile = false, string Code = " ", bool useOwnSyntax = false, User? user = null)
        {
            Name = name;
            isLocalFile = asFile;

            Output = new object[3] { false,new LuaTable(1,Engine), "Not loaded!" };
            Engine = new(); insertParams(args);
            
            this.Code = Code; RespBody = Output;

            if (asFile)
            {
                CodeSource = System.IO.File.ReadAllLines(File);
                this.Code = System.IO.File.ReadAllText(File); 
            }
            else
            {
                this.Code = Code;
                CodeSource = new string[1] { Code };
            }
            UseOwnSyntax = useOwnSyntax;
        }

        private void insertParams(List<(string, object)> args)
        {
            foreach (var variable in args)
            {
                ScriptArgs.Add(variable.Item1, variable.Item2);//SaveforBackupReasons
                Engine[variable.Item1] = variable.Item2;//Apply
            } 

        }
        //===================================================================================================

        public object[] Start(bool useOwnSyntax = false, bool localFile = true)
        {
            UseOwnSyntax = useOwnSyntax;
            return Execute( false);
        }

        public object[] Execute(bool useOwnSyntax = false)
        {
            Output = new object[3] { false, new LuaTable(1, Engine), "ScriptObject Not found°" };

            if (Engine == null)
            {
                Engine = new Lua();
                Preload(ref Engine);
                if (MyNamespaces.Any())
                {
                    LoadImports(ref Engine, MyNamespaces);
                }
            }
            bool flag = this.Code.Check();
            if (flag)
            {
                Output = new object[3] { false,new object[]{}, "Error while Preloading" };
                if (ScriptArgs.Any())
                {
                    foreach (var item in ScriptArgs)
                    {
                        Engine[item.Key] = item.Value;
                    }
                }
                    switch (isLocalFile)
                    {
                        case false:
                            Output = Engine.DoString(Code);
                            StatusResponse = (bool)(Output.First());
                            RespBody = (object[])Output;
                            Succeed = (bool)(Output[0]);
                            ErrorCode = (string)(Output[2]);
                            break;
                        case true:
                            Output = Engine.DoFile(File);
                            StatusResponse = (bool)(Output.First());
                            RespBody = (object[])Output;
                            Succeed = (bool)(Output[0]);
                            ErrorCode = (string)(Output[2]);
                            break;
                    }
                }           
            return Output;
        }
    }

//===========================================================================================================================================================
// ------------------- <= CodeSyntax => ------------------ <= CodeSyntax => ------------------------- <= CodeSyntax => -------------------------------------- 
//===========================================================================================================================================================
public interface Syntax
    {
    




    } 
    // ------------------- <= Eventing => ------------------ <= Eventing => ------------------------- <= Eventing => -------------------------------------- 
    internal interface Events
    {
        private static string[] eventList = { "tap", "sprite", "create", "show", "hide", "destroy" }; public static string eArgs = eventList[0];
        public void setEvent() { string e = "scene:addEventListener( '" + eArgs + "', scene )"; }
    }

}
///////////////////////local function SaveScript( file )
 ///////////////////////   if file  ~= nil then aktFile = file end
  ///////////////////////  local code = ConvertCode(this.text)
  ///////////////////////  f = io.open(aktFile,"w")
  ///////////////////////  if f ~= nil then f:write(code) f:close()end
///////////////////////end
    
