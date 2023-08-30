using VBLua.Terminal;
using VBLua.Core;
using Newtonsoft.Json;
using VB.Lua.GUIController;
using static VBLua.Core.Utilities;
using System.Linq;
using System.ComponentModel;
using static VBLua.IDE.Designer;
using System.Diagnostics;
using static VB.Lua.GUIController.Menu1;
using ScriptingTool;

namespace VBLua.IDE
{
    public partial class Designer : Form, AppCode, CodeUtils, Loader
    {
        public static string? projektpick; Project me; static string? loadedProject; protected static string? solar2dPath;
        static Dictionary<string, Control> ctrList = new(); string mode = "Solar2D";
        List<KnownColor> fontColorList = new(); List<KnownColor> ColorList = new(); bool createNew = false;
        VBLua.IDE.Toolstrip ts; public static Scene sceneInFocus;CoderControl CodeEdit; static Input? rtb1;

        //=============================================================================================================================
        // ------------------- <= Initializer => ------------------ //HighlightOwnCode
        public Designer()
        {
            InitializeComponent(); getColors(); // <= Preloading_Visuals
            CodeEdit = new(); CodeEdit.Show(); rtb1 = CodeEdit.CodeEdit;
        }

        private void Designer_Load(object sender, EventArgs e)
        {
            solar2dPath = Directory.GetCurrentDirectory() + "Corona Simulator.exe";//"\\
            string lastOpened = "Testengine"; loadedProject = lastOpened; Project.lastLoaded = lastOpened;
            Loading(sender, e); List<dynamic> newlist = new(); foreach (var x in me.getElements().Values) { newlist.Add(x); }
            ///Solar2DTableControl test = new("loool", newlist);test.GetLuaCodeFromTable();

        }
        //=============================================================================================================================
        private void Loading(object sender, EventArgs e)
        {
            ToolStripMenuItem t = new();
            if (sender.GetType() == t.GetType()) { createNew = true; } else { createNew = false; }
            loadProject("Testengine", createNew); Project.elementList = getElements(); var list = me.getScenes();
            if (list != null) { foreach (var x in list) { SceneAusw.Items.Add(x?.name); } }

            this.FindForm().Text = me.type + " Designer (Actual: " + me.scenePick.name + ")"; SceneAusw.Text = me.scenePick.name;
            sceneInFocus = me.scenePick; clearScreen(sender, e); LoadScreen();
            me.Save();
        }
        private void loadProject(string pathWithName, bool newProject = false)
        {
            if (Project.lastLoaded != null && 1 == 0) { me = new Project(Project.lastLoaded, this, "Solar2D", new Size(1920, 1080)); this.Controls.Add(sd); }
            else { me = new Project(pathWithName, this, "Solar2D", new Size(1920, 1080), newProject); this.Controls.Add(sd); }
            LoadScreen(); if (me.scenePick != null) { me.scenePick.Res = new(this.ScreenPanel.Size.Width, this.ScreenPanel.Height - mainmenustrip.Height); }
        }
        //============================== Screen =================================
        private void LoadScreen()
        {
            foreach (Element element in Project.elementList.Values.Where((me) => me.ParentScene == SceneAusw.Text))
            {
                Control newCtr = element.createCtr();
                if (newCtr != null)
                {
                    ScreenPanel.Controls.Add(newCtr); newCtr.BringToFront(); newCtr.Show();
                    newCtr.MouseDown += me_MouseDown; newCtr.MouseUp += me_MouseUp; newCtr.MouseDoubleClick += me_DoubleClick;
                    PickedElement = element;
                }
                else { MessageBox.Show("failed"); }
            }
        }

        protected void clearScreen(object sender, EventArgs e) { foreach (Control c in ScreenPanel.Controls) { c.Dispose(); } ScreenPanel.Controls.Clear(); }
        //========= ======================================================================================================================== ==========================
        // ------------------- <= Loading_Methoden => ------------------ <= Loading_Methoden => ------------------ <= Loading_Methoden => ------------------------ 
        //========= ======================================================================================================================== ==========================
        Control ctrEdit; Element elementToEdit;
        private void newElement(object sender, EventArgs e)
        {
            sd.DoAction();
            if (sd.createdElementData != null && Project.elementList.Values.Contains(sd.createdElementData) == false)
            {
                Project.elementList.Add(sd.createdElementData.name, sd.createdElementData);
                Control ctr = sd.createdElementData.createCtr(); ctr.MouseDown += me_MouseDown; ctr.MouseUp += me_MouseUp;
                ctrList.Add(sd.createdElementData.name, ctr); ScreenPanel.Controls.Add(ctr);
                sd.Okckl.Click -= newElement; sd.Visible = false;
                me.Save();
            }
        }
        public void EditElement(object sender, EventArgs e)
        {
            sd.DoAction();
            if (PickedElement != null && sd.createdElementData != null)
            {
                ControlAttributeChanger.ChangeControlAttributes(ctrEdit, sd.ID, sd.width, sd.height, sd.bgColor, sd.fColor, 12f);
                Project.elementList[PickedElement.name] = sd.createdElementData; me.Save();
            }
            else { MessageBox.Show("Error Finding Picked Target!"); }

        }

        // ==================== <= Pick & GET => ==================== <= Pick & GET => ==================== <= Pick & GET => ======================================== 
        //===========================================================================================================================================================   
        //--------- <= Collections & IEnumerables => ---------- 
        public static Element? PickedElement; public static Element? newEle;
        //--------------- <= GET_Methoden => ----------
        protected Element? getElementFromScreen() { return Project.elementList?[PickedElement.name]; } // <= GET:Element
        protected Element? getElementByID(string id) { return Project.elementList[id]; } // <= GET:Element
        protected Dictionary<string, Element> getElements()
        {
            var ele = JsonConvert.DeserializeObject<Dictionary<string, Element>>(System.IO.File.ReadAllText(Project.IDEPath + "/elements.json")); return ele;
        }
        protected Control? getCTRByID(string id)
        { // <= GET:Control
            foreach (var c in ctrList.Values)
            {
                if (c.Name == id) { return c; }
                else { return null; }
            }
            return null;
        }

        //========= ======================================================================================================================== ==========================
        ////////////////// <= Mouse_Eventing =>  //////////////////////// <= Mouse_Eventing =>  ////////////////////////////////////////        //public Point eventCoords { get => new Point((int)setEventX.Value, (int)setEventY.Value); }  public int eventTime { get => (int)setTime.Value; }
        //========= ======================================================================================================================== ==========================
        protected Point oldLocation = new(); private Point getMouse { get => new Point(MousePosition.X, MousePosition.Y); }
        bool selected;
        protected void me_MouseDown(object sender, MouseEventArgs e)
        {
            Control ctr = (Control)sender;
            if (e.Button == MouseButtons.Left && ctr.Tag.ToStr().Contains(":/"))
            {
                PickedElement = Project.elementList[ctr.Name]; oldLocation = ctr.Location;
                selected = true; ctr.MouseMove += me_MouseMove;
                ControlHighlighter.continueHighlight = true; ControlHighlighter.Blinking(ctr);
                ts = new(ctr);  // <= CodeEditor.KeyPressed +=richTextBox_KeyPress; this.Controls.Add(CodeEditor).Show()
                CodeEdit.Text =me.scenePick.name; CodeEdit.CodeEdit.Text= PickedElement.createCode("--"); CodeEdit.Visible = true; this.Visible = true;
            }
            else if (e.Button == MouseButtons.Right && ctr.Tag.ToStr().Contains(":/"))
            {
                PickedElement = Project.elementList[ctr.Name]; oldLocation = ctr.Location;
                if (PickedElement != null)
                {
                    if (sd != null) { sd.Dispose(); }
                    ctr.ContextMenuStrip = new();
                    sd = null; sd = new(PickedElement, "view1"); sd.mode = "edit"; sd.Location = ctr.Location;
                    sd.type = PickedElement?.type; sd.Location = new(ScreenPanel.Location.X - (this.Width), ScreenPanel.Location.Y + (this.Height / 3)); ctrEdit = ctr;
                    sd.Visible = true; sd.BringToFront(); sd.Show(); sd.Okckl.Click += EditElement; sd.Okckl.Click += sd.closeMe; sd.delClk.MouseClick += DeleteElement;
                }

            }
        }
        protected void me_MouseUp(object sender, MouseEventArgs e)
        {
            Control ctr = (Control)sender;
            if (e.Button == MouseButtons.Left && PickedElement != null)
            {
                try
                {
                    Project.elementList[ctr.Name].RealPos = new(ctr.Location.X, ctr.Location.Y); Project.elementList[ctr.Name].AppPos = Project.elementList[ctr.Name].setCoords();
                }
                catch
                {
                    MessageBox.Show("Failed to set Elementdata!");
                }
                selected = false; ctr.MouseMove -= me_MouseMove;
            }
        }
        protected void me_MouseMove(object sender, MouseEventArgs e)
        {
            Control ctr = (Control)sender;
            if (selected && PickedElement != null)
            {
                ctr.Location = new Point(MousePosition.X - this.Location.X - (ctr.Width / 2),
                                            MousePosition.Y - this.Location.Y - (ctr.Height / 2));
            }
        }
        protected void me_DoubleClick(object sender, MouseEventArgs e)
        {
            Control ctr = (Control)sender;
            if (e.Button == MouseButtons.Left && PickedElement != null)
            {
                ctr.ContextMenuStrip.Show();
            }
        }

        //========= ======================================================================================================================== ==========================
        ////////////////// <= UI_Verwalten =>  //////////////////////// <= UI_Verwalten =>  //////////////////////////////////////// UI_Verwalten ////////////       //public Point eventCoords { get => new Point((int)setEventX.Value, (int)setEventY.Value); }  public int eventTime { get => (int)setTime.Value; }
        //========= ======================================================================================================================== ==========================
        public KnownColor[] getColors()
        {
            KnownColor[] colorCollection = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            foreach (var c in colorCollection) { fontColorList.Add(c); ColorList.Add(c); }
            return colorCollection;
        }
        private void IDE_FormClosing(object sender, FormClosingEventArgs e)
        {
            var prozzesse1 = Process.GetProcessesByName("Corona Simulator"); var prozzesse2 = Process.GetProcessesByName("Corona.Console"); foreach (var prozess in prozzesse1) { prozess.Kill(); }
            foreach (var prozess in prozzesse2) { prozess.Kill(); }
        }
        protected void closeMe(object sender, EventArgs e) { Close(); }  // ElementCodes.editDesigner(me.name, "");
        private void Save_Event(object sender, EventArgs e)
        {
            me.Save();
        }
        // ----------------- <= System_Fenster => ---------------- 
        static string creation = "Project"; static string? lastOpened;
        private void load_Click(object sender, EventArgs e)
        {
            searchProject.InitialDirectory = Project.pathIDE;
            DialogResult result = searchProject.ShowDialog();
            if (result == DialogResult.OK)
            {
                string id = Path.GetFileNameWithoutExtension(searchProject.FileName);
                loadProject(id);
            }
        }
        private void ElementWindowClick(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender; this.Controls.Remove(sd); sd = null;
            sd = new(new Element(item.Tag.ToStr() + me.getElements().Count + 1.ToStr(), new Size(75, 50), new Point(0, 0), item.Tag.ToStr(), (Color.White, Color.Black)), me.scenePick.name);
            sd.mode = "create"; sd.Location = ScreenPanel.Location; sd.preloadedElement.ParentScene = SceneAusw.Text;
            sd.Visible = true; sd.Show(); sd.BringToFront(); sd.Okckl.Click += newElement; sd.Okckl.Click += sd.closeMe;
            sd.ContextMenuStrip.GetItemAt(0, 4).Click += DeleteElement;

        }


        //========= ======================================================================================================================== ==========================                      
        ////////////////// <= Dialogs & Interactions =>  ////////////////////// <= Dialogs & Interactions =>  /////////////////////////
        //========= ======================================================================================================================== ==========================
        private OpenFileDialog searchProject = new();
        private void searchSolar_FileOk(object sender, CancelEventArgs e)
        {
            string mypath = searchProject.FileName;
            if (System.IO.File.Exists(mypath))
            {
                MessageBox.Show("Simulator Found!Setting UP...");
                if (System.IO.File.Exists(solar2dPath + "Corona Simulator.exe")) { string inf = "CoronaSimulator Deteteced!"; }
            }
        }
        bool debuggerIsRunning = false;
        protected void openDebugger(object sender, EventArgs e)
        {
            switch (debuggerIsRunning)
            {
                case (false):
                    Process sim = new(); ProcessStartInfo inf = new();
                    inf.FileName = Directory.GetCurrentDirectory() + "\\debugger\\Corona.Shell.exe";//
                    inf.Arguments = Directory.GetCurrentDirectory() + "\\projects\\Testengine\\main.lua"; inf.UseShellExecute = false; sim.StartInfo = inf;
                    inf.WorkingDirectory = Directory.GetCurrentDirectory() + "\\debugger\\"; inf.CreateNoWindow = false; inf.RedirectStandardOutput = true;
                    using (sim) { sim.StartInfo = inf; sim.Start(); }
                    //debuggerIsRunning = true;
                    break;
            }
        }

        //========= ======================================================================================================================== ==========================
        // ---------------==============---- <= Interfaces => --==============---------------- <= Interfaces => =============================================
        //=======================================================================================================================================================
        interface CodeUtils
        {
            public static void addLuaCode(string file, string content)
            {
                StreamWriter writeToFile = System.IO.File.AppendText(file); writeToFile.WriteLine("\t " + content); writeToFile.Close();
            }
            public static string sceneElements()
            {
                return System.IO.File.ReadAllText(Designer.loadedProject + Designer.loadedProject + "/" + "Designer.wrze"); // <= Lese:Designer_File
            }

            public static void addLogic(string content) { addLuaCode(Project.logicPath, content); }
            public static void addClassOrInstance(string content) { addLuaCode(Project.classPath, content); }
            public static void addMaincode(string content) { addLuaCode(Project.mainPath, content); }
            public static void addSceneCode(string content) { addLuaCode(Project.scenePath, content); }
            public static string logicCode { get => System.IO.File.ReadAllText(Project.logicPath); }
            public static string[] readClassOrInstance() { string[] lines = System.IO.File.ReadAllLines("Classes.wrze"); return lines; }
            public static string[] readLogic() { string[] lines = System.IO.File.ReadAllLines(Project.logicPath); return lines; }
        }

        static Button bt = new(); static Label label = new(); static TextBox tb = new(); static TextBox tb2 = new(); static PictureBox WEBv = new(); static PictureBox pb = new();
        static PictureBox rect = new(); static ProgressBar progrBar = new(); static CheckBox checkBox = new(); static RadioButton radioBt = new();
        private void SceneAusw_TextChanged(object sender, EventArgs e)
        {
            var newScenePick = from scene in me.getScenes()
                               where scene.name == SceneAusw.Text
                               select scene;
            me.scenePick = newScenePick.First();
            if (SceneAusw.Text != "main") { clearScreen(sender, e); }
            this.FindForm().Text = me.type + " Designer (Actual: " + me.scenePick.name + ")";
        }
        private void TestButton_Click(object sender, EventArgs e)
        {
            // me.scenePick.createElementinApp(me.getElements()); ;
            string outp = Syntaxer.Convert(CodeEdit.CodeEdit.Text);

        }

        private void ScreenPanel_Reset(object sender, MouseEventArgs e)
        {
            if (PickedElement != null) { getCTRByID(PickedElement.id)?.ContextMenuStrip.Hide(); }
        }
        public void DeleteElement(object sender, EventArgs e)
        {
            if (PickedElement != null)
            {
                Project.elementList.Remove(PickedElement.name); ctrList.Remove(PickedElement.name); me.Save();
                clearScreen(sender, e); LoadScreen();
            }
        }

        private void ScreenPanel_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}