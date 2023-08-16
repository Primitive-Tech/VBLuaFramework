using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using VBLua.Core;
using static VB.Lua.GUIController.Menu1;
using Fnc = VBLua.Core.Utilities;

namespace VBLua.IDE
{
    public partial class Element : AppCode, IDisposable
    {
        private Scene scene = new(); string parentScene; public string ParentScene { get => parentScene; set => parentScene = value; } public static Dictionary<string, object> userInput = new();
        private Size appSize; private Size realSize; public Point realPos; public Point appPos;
        public Size AppSize { get => setSize(); set => appSize = value; } public Size RealSize { get => realSize; set => realSize = value; }
        public Point RealPos { get => realPos; set => realPos = value; }
        public Point AppPos { get => setCoords(); set => appPos = value; }
        string FinishedCode;public int myValue { get => MyValue; set => MyValue = value; }private int MyValue=0;
        //========= ======================================================================================================================== ==========================
        // ------------------- <= Projekt => ------------------ <= Projekt => ------------------ <= Projekt => ------------------------ 
        public static string fullcode = ""; public Dictionary<string, dynamic> adding = new(); public string name;
        private string[] Resc; public string[] resc { get => Resc; set => Resc = value; } protected string Type; public string type { get => Type; set => Type = value; }
       
        private string? ID; public string? id { get => ID; set => ID = value; }  private string? Text = ""; public string text { get => Text; set => Text = value; }

        bool isOnScreen=true, IsLocal; public bool isLocal { get => IsLocal; set => IsLocal = value; }
        public bool toScene = true; public bool IsOnScreen { get => isOnScreen; set => isOnScreen = value; }
        private Color FontColor; private Color BGColor; public Color fontColor { get => FontColor; set => FontColor = value; }
        public Color bgColor { get => BGColor; set => BGColor = value; }
        public string? eventCode; public string? fncCode; // <= Zusätzliche Codeblöcke(inProjCode)
        protected Image RescourceFile { get => Image.FromFile(Project.IDEPath + Rescource); }
        public string? Rescource {  set => resc[0]= value; get => resc[0]; }
        public string? SecondaryRescource { set => resc[1] = value; get => resc[1]; }

        //=======================================================================================================================================
        // ------------------- <= Initializer => ------------------ <= Initializer => ------------------------ <= Initializer => -------------
        //========================================================================================================================================
        public Element(string name, Size size, Point location, string type, (Color,Color) colors,bool isOnScreen = true)
        {
            this.name = name; this.id = name; this.Type = type; this.RealSize = size; this.RealPos = location;
            this.fontColor = colors.Item2; this.bgColor = colors.Item1;
            if (this.Resc == null) { this.Resc = new string[] { "button2.png", "button2-down.png" }; }
        }
        private void AddRescource(string file) { Rescource= file; }
      
        //========= ======================================================================================================================== ==========================
        // ------------------- <= GET/SET => ------------------ <= GET/SET => ------------------------ <= GET/SET => -------------
        protected string Code { get => ElementCodes.codes[type]; set => ElementCodes.codes[type] = value; } // <= Jeweiligen Code zur Initit des Element In der AndroidApp
        Solar2DRadioButtonControl radioBt; string radioButtonsCode;
        public Control createCtr()
        {
            Control newCtr; Button bt = new(); System.Windows.Forms.Label label = new(); TextBox tb = new(); TextBox tb2 = new(); PictureBox WEBv = new(); PictureBox pb = new();
            PictureBox rect = new(); ProgressBar progrBar = new(); CheckBox checkBox = new(); radioBt = new("newPanelOfRB",2);
            Dictionary<string, Control> ElementDummys = new() {{ "Button", bt},{ "InputField", tb},{ "Text", label},{ "TextBox", tb},
                { "Slider", bt},{ "ProgressBar", progrBar},{ "onOff", checkBox},
            { "checkbox", checkBox},{ "WebView", WEBv},{ "TabBar", bt},{ "ImageRectangle", pb},
            { "Rectangle", rect}, { "RadioButton", radioBt}};

            radioBt.BackColor = bgColor;radioBt.ForeColor = fontColor;  radioBt.Txt="RadioBT";radioBt.AutoSizeMode = AutoSizeMode.GrowOnly;radioBt.Font = SystemFonts.DefaultFont;
            radioButtonsCode =radioBt.radioButtonsCode;
            double x = pb.Width * 0.6; pb.Width = x.ToInt(); progrBar.Size =this.realSize; progrBar.Value = 90; myValue = progrBar.Value; pb.Size = this.realSize;
            pb.BackgroundImageLayout = ImageLayout.Stretch; pb.SizeMode = PictureBoxSizeMode.StretchImage;bt.BackgroundImageLayout = ImageLayout.Stretch; 
            if (resc != null) { pb.Image = RescourceFile; bt.BackgroundImage = RescourceFile; }
            WEBv.BackColor = Color.Wheat;
            var doit = () => {
                if (ElementDummys[Type] != null) { return ElementDummys[Type]; } else { return null; }
            };
            newCtr = doit(); newCtr.Name = this.name; newCtr.Visible = this.isOnScreen; newCtr.Tag = ":/" + this.id;
         
            newCtr.Location = this.RealPos; newCtr.Size = this.RealSize; newCtr.Visible = true; tb2.ReadOnly = true;
            newCtr.ForeColor = fontColor; newCtr.BackColor = bgColor; 


            return newCtr;
        }

        //===========================================================================================================================================================
        // ------------- <= Code_Creation => ------------ <= Code_Creation => ------------------- <= Code_Creation => -------------------------------------- 
        //==================================================================================================================
        public string createCode(string kommentar="")
        {                 
            string addtextProp = "label = '" + text; if (type == "Text") { addtextProp = "text = '" + text; }           // <= Text/Label
            string toSceneProp = " "; if (toScene) { toSceneProp = " sceneGroup:insert(" + id + ") "; }
            string ZeichenEnde="";

            var Finished=(string code) =>  {
                if (code != "" )
                {
                    StreamWriter writeToDesigner = File.AppendText("Designer.wrze"); writeToDesigner.WriteLine("\t" + code); writeToDesigner.Close();
                    //this.Code = sceneCode;  //Scene.fullScene = this.Code; // <= Scene_Überschreiben
                    return true;
                }
                else { MessageBox.Show("Failed creating ElementeCode!"); return false;}
            };
            //------------------------------------------------------------------------------------------------------------
            if (Code != null) {  
                //-------------- Initializer_Code(Before Designer_Loaded) --------------------
            var InitCode= (bool withTable) => {
                string Zeichen; string Zeichen2="";
                if (withTable) { Zeichen = "({"; Zeichen2 = "})"; ZeichenEnde = Zeichen2; } else { Zeichen = "("; Zeichen2 = ")"; ZeichenEnde = Zeichen2; }
                if (isLocal) { return "local " + id + Code + Zeichen; } else { return id + Code + Zeichen; }             
            };

            var newProperty = (object x) => x?.ToStr() + "='" + text + "', ";
            var newInteger = (int x) => Convert.ToInt32(x).ToStr() + "=" + text + ", "; addtextProp = "" + ZeichenEnde;
                //////// <= Buttons => ////////
                if (Code.Contains("newButton"))
                {
                    addtextProp = newProperty("label");
                    FinishedCode = InitCode(true) + // <= Setze Element_Code => //
                            $"{addtextProp} defaultFile = '{Rescource}', overFile = '{resc?[1]}', width =  {AppSize.Width},  height = {AppSize.Height}" +
                            ZeichenEnde + $"{id}.x = {AppPos.X} {id}.y = {AppPos.Y} {toSceneProp}";
                    if (BGColor!=Color.WhiteSmoke) {  FinishedCode+=getColor(); }

                }
                //////// <= Bilder + Sheets => ////////
                else if (Code.Contains("newImage"))
                {
                    FinishedCode = InitCode(false) + addtextProp +
                        $" '{Rescource}',{AppSize.Width}, {AppSize.Height} " +
                            ZeichenEnde + $"{id}.x = {AppPos.X} {id}.y = {AppPos.Y} {toSceneProp}";
                    if (BGColor != Color.WhiteSmoke&& BGColor != Color.Black) { FinishedCode += getColor(); }
                }
                //////// <= RadioButtonsPanel => ////////
                else if (Code.Contains("RadioButton"))
                {
                    FinishedCode = "\n  local radioGroup = display.newGroup()\n" + radioButtonsCode+ "\nsceneGroup:insert(radioGroup)\n"; ;
                } 
                //////// <= ProgressView => ////////
                else if (Code.Contains("newProgressView"))
                {
                    string fill = id + ": setProgress(" + myValue.ToStr() + ")";
                    string etc = ",isAnimated = true,fillYOffset = 1"; // <= Etc:Configs
                    FinishedCode = InitCode(true) + addtextProp + "width = " + AppSize.Width + ", " + "height = " + AppSize.Height + ", " +
                            "x = " + AppPos.X + ", " + "y = " + AppPos.Y + etc + ZeichenEnde + $"{toSceneProp}" + fill;
                }
                //////// <= Texte + Geometrisches => ////////
                else if (Code.Contains("display"))
                {
                    addtextProp = newProperty("label"); // <= Label/Image_Rescource
                    FinishedCode = InitCode(false) +
                        $"{addtextProp}{AppSize.Width}, {AppSize.Height}" + ZeichenEnde +
                        $"{id}.x = {AppPos.X} {id}.y = {AppPos.Y} {toSceneProp}";

                }
                //////// <= TextBox/Field + WebView => ////////
                else if (Code.Contains("WebView"))
                {
                    if (text == null) { text = "www.google.de"; }
                    string isLocalStr = "local "; if (!this.isLocal) { isLocalStr = ""; }
                    FinishedCode = $"{isLocalStr} {id} =createWebBrowser({appPos.X}, {appPos.Y},{appSize.Width},{appSize.Height} )"+//{id}:request('https://' {text} + '/')" +
                        $"scene.view:insert({id})";
                }             

                else if (Code.Contains("native"))
            {
                FinishedCode =  InitCode(false) +
                        $"{addtextProp} {AppPos.X}, {AppPos.Y}, {AppSize.Width}, {AppSize.Height}" + ZeichenEnde+ $"{toSceneProp}";
                }
           
                //////// <= Slider => ////////
                else if (Code.Contains("Slider"))
                {
                    string myCategories = "{'test1','tst2','tet3'}";
                    string isLocalStr = "local "; if (!this.isLocal) { isLocalStr = ""; } 
                    FinishedCode = $"{ isLocalStr} {id} = createScrollWidget(" +
                        AppSize.Width + ", " + AppSize.Height + ", " + AppPos.X + ", " + AppPos.Y  +
                        $",{bgColor.ToArgb()}) { myCategories}{ toSceneProp})";
                }

                //////// <= ProgressView => ////////
                else if (Code.Contains("newProgressView"))
                {
                    int val = 100; string fill = id + ": setProgress(" + val + ")";
                    string etc = ",isAnimated = true,fillYOffset = 1"; // <= Etc:Configs
                    FinishedCode = InitCode(true) + addtextProp + "width = " + AppSize.Width + ", " + "height = " + AppSize.Height + ", " +
                            "x = " + AppPos.X + ", " + "y = " + AppPos.Y + etc + ZeichenEnde + $"{toSceneProp}" + fill;
                }
            }
            if (Finished(FinishedCode)) { return FinishedCode; } else { return null; };
        }

        public void Info() { MessageBox.Show(this.RealSize.ToString() + this.AppSize.ToString() + "  " + this.RealPos.ToString() + this.AppPos.ToString()); }public void Dispose()
        {
            throw new NotImplementedException();
        }

        // ------------------- <= Etc => ------------------ <= Etc => ------------------------- <= Etc => -------------------------------------- 
        public Point setCoords()
        {
            double x = RealPos.X + this.realSize.Width/2; double y = RealPos.Y - this.realSize.Height/2+(40*2)+(realSize.Height/2);
            x /= scene.Res.Width; y /= scene.Res.Height;
            x *= scene.AppRes.Width; y *= scene.AppRes.Height; return new Point((int)x, (int)y);
        }
        public Size setSize()
        {
            double w = RealSize.Width; double h = RealSize.Height;
            w /= scene.Res.Width; h /= scene.Res.Height;
            w *= scene.AppRes.Width; h *= scene.AppRes.Height; return new Size((int)w, (int)h);
        }
        // !!    enableDragAndDrop(hallo,true)
        // Get RGB values from the Color object
        private string getColor()
        {
            int red = bgColor.R; int green = bgColor.G; int blue = bgColor.B;
            string ausw; string rgbString = $"{red}, {green}, {blue}";
            // Create the final string with the color information
            if (type.Contains("Button")) { ausw = $" {name}:setFillColor({rgbString})"; }
            else { ausw = $" {name}.fill  = paint"; }
            string result = $"local paint = {{ {rgbString} }}\n" + ausw;
            return result;
        }
        //===========================================================================================================================================================
        // ------------------- <= Interfaces => ------------------ <= Interfaces => ------------------------- <= Interfaces => -------------------------------------- 
        //===========================================================================================================================================================
        public interface ElementCodes
        {
            public static Dictionary<string, string> codes = new(){
                ////// <= Solar2D_Elemente => /////
                { "Button", " = widget.newButton" },{ "RoundedButton", " = widget.newButton" },{ "Text", " = display.newText" },{ "TabBar", "  = widget.newTabBar" },
                { "ImageRectangle", " = display.newImageRect" },{ "RoundedRectangle", " = display.newRoundedRect" },
                { "TextBox", " = native.newTextBox" }, { "InputField", " = native.newTextField" },
                { "ProgressBar", " = widget.newProgressView" },{ "Rectangle", " = display.newImageRect" },
                { "onOff", " = widget.newSwitch" },{ "checkbox", " = widget.newSwitch" },
                { "RadioButton", " = widget.RadioButton" },
                { "Slider", " = widget.newSlider" },{ "WebView", " = native.newWebView" }
        };
            public static Dictionary<string, string> codes2 = new(){ ////// <= Forms_Elemente => //////
                { "Forms_Button", " = ui.Button" }, { "Input", "= ui.Edit" },
                { "Label", " = ui.Label" }, { "ComboBox", " = ui.Combobox" },
                { "CheckBox", " = ui.Checkbox" },{ "RadioButton", " = ui.Radiobutton" },
                { "EntryField", " =  ui.Entry" }, { "ListBox", " = ui.List" },
                { "TabControl", "  =  ui.Tab" },{ "GroupBox", " = ui.Groupbox" },
                { "Video", " = native.newVideo" }, { "Circle", " = display.newCircle" },
                { "Calendar", "ui.Calendar" }
        };
            public string newFont(string id, string fontName)
            {
                return "native.newFont(" + id + "," + fontName;
            }
        }
    }
}