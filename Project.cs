using Newtonsoft.Json;
using NLua;
using Fnc= VBLua.Core.Utilities;
using VBLua.Properties;
using VBLua.IDE;

namespace VBLua.Core
{
    //========= ======================================================================================================================== ==========================
    // ------------------- <= Projekt => ------------------ <= Projekt => ------------------ <= Projekt => ------------------------ 
    //========= ======================================================================================================================== ==========================
    public class Project : AppCode
    {
        public Scene scenePick; public static Dictionary<string, Element> elementList = new(); string typ; public string Type { get => typ; set => typ = value; }
        string[] mode = { "Solar2D", "Forms" };
        // <=  Projekt_Eigenschaften => //
        public static string? IDEPath; public string type; public string name; private Size displaySize; private Color bgColor; private Color fontColor;
        public Scene MainScene = new("view1"); public readonly Scene MainFile = new("main"); public string info = "";

        public Scene edit; public List<Scene>? formulare = new(); public string mainfile;

        // ------------------- <= Init => ------------------ <= Init => ------------------ <= Init => ------------------------ 
        public Project(string name, Form designer, string type, Size displaySize, bool newProject = true)
        {
            typ = mode[0]; IDEPath = AppSaves + name + "/"; mainfile = IDEPath + "main.lua";
            scenePick = MainScene; // <= Lese MainScene ein
            if (this.name == lastLoaded) { New(); }
            lastLoaded = name; Settings.Default.LastLoaded = lastLoaded;

            this.name = name; this.displaySize = displaySize; this.type = type; edit = new("");
            bgColor = designer.BackColor; fontColor = designer.ForeColor;

            if (this.name == lastLoaded)
            {
                if (newProject) { New(); }
                try { formulare = getScenes(); info = "Solar2D-IDE (Loaded: " + formulare.Count().ToStr(); }
                catch
                {
                   MessageBox.Show("Failed loadingScenes!"); //MessageBox.Show(this.path + "\n" + mainfile);
                }
            }
            if (Directory.Exists(IDEPath) == false) { Fnc.Unzip(name, AppSaves); } // <= Unzip(ggf.): Leere_App    
            saveProps(); //try {  FncMessageBox.Show(this.formulare?.Count().ToStr() + "\n" + scenePick.name);  } catch { FncMessageBox.Show("Error while unpacking!"); }              
        }
        public void getInfo() { try { foreach (var f in formulare) { MessageBox.Show(f.name + "" + f.Code); } } catch { MessageBox.Show("No Scenes found!"); }; }

        //========================================================================================================================
        // ------------------- <= Basics => ------------------ <= Basics => ------------------ <= Basics => ------------------------ 
        // Create New Projekt //
        public void New()
        {
            formulare.Clear(); elementList.Clear();
            formulare.Add(MainScene); formulare.Add(MainFile);
            string x = JsonConvert.SerializeObject(formulare, Formatting.Indented); File.WriteAllText(IDEPath + "/scenes.json", x); // <= Save Scene_File
            Save();
        }
        public void Save()
        {
            string SceneData = JsonConvert.SerializeObject(formulare, Formatting.Indented); /// <= GetData From_Source
            File.WriteAllText(IDEPath + "/scenes.json", SceneData); // <= Save All_Files
            string ElementData = JsonConvert.SerializeObject(elementList, Formatting.Indented); /// <= GetData From_Source
            File.WriteAllText(IDEPath + "/elements.json", ElementData);
            string? sceneCode = scenePick.createElementinApp(elementList);
            if (sceneCode != null) { File.WriteAllText(IDEPath + scenePick.name + ".lua", sceneCode); } else { MessageBox.Show("Failed generating SceneCode!"); }
        }
        public void editDesigner(string id, string codezeile)
        {
            File.AppendAllText(IDEPath + id + ".wrze", codezeile);
        }

        //========= ======================================================================================================================== ==========================
        // ------------------- <= JSON(Save) => ------------------ <= JSON(Save) => ------------------ <= JSON(Save) => ------------------------ 
        //========= ======================================================================================================================== ==========================
        private Dictionary<string, object[]> cfgFile = new(); public JsonSerializerSettings options = new() { Formatting = Formatting.Indented }; //<= MainConfig
        public Dictionary<string, object[]> appObjekt { get => cfgFile; set => cfgFile = value; }

        public List<Scene>? getScenes()
        {
            string reader = File.ReadAllText(IDEPath + "/scenes.json");
            if (reader != null) { return JsonConvert.DeserializeObject<Scene[]>(reader)?.ToList(); } else { return null; } 
        }
        public Dictionary<string, Element>? getElements()
        {
            string reader = File.ReadAllText(IDEPath + "/elements.json");
            if (reader != null) {return JsonConvert.DeserializeObject<Dictionary<string, Element>>(reader);}
            else { return null; }

        }

        public static string pathIDE = Settings.Default.path1; public const string AppSaves = "projects/";
        public static string classPath { get => AppSaves + "Classes.wrze"; }
        public static string mainPath { get => AppSaves + "mainCode.wrze"; }
        public static string scenePath { get => AppSaves + "sceneCode.wrze"; }
        public static string logicPath { get => AppSaves + "logicals.wrze"; }
        public static string lastLoaded = Settings.Default.LastLoaded;

        private static void saveProps() { Settings.Default.Save(); }
    }

}
