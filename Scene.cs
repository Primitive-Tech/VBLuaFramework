using Newtonsoft.Json;
using System.Linq;
using NLua;
using Fnc = VBLua.Core.Utilities;
using VBLua.IDE;

namespace VBLua.Core
{
    //========= ======================================================================================================================== ==========================
    // ------------------- <= Scene => ------------------ <= Scene => ------------------ <= Scene => ------------------------ 
    //========= ======================================================================================================================== ==========================
    public class Scene : AppCode
    {
        public Size AppRes = new(320, 480); public Size Res= new(880, 580-34); public static string? projektname;//900; 540
        private string Name; public string name { get => Name; set => Name = value; }

        public string? Code; private string? _Code { get => _Code; }
        //public object[]? Script { get => lua.DoFile("data/CodeCreator.lua"); }// public object? SceneObj { get => Script?.First()?.ToStr(); }
        private Lua lua = new();

        public Scene() { }
        // <= Standart_Konstruktor => //
        public Scene(string name)
        {
            this.name = name; this.name = name; this.name = name;
            lua = new(); //Code = SceneObj?.ToStr();

        }
        public string? createElementinApp(Dictionary<string, Element> Elements)
        {
            var elements = from element in Elements.Values
                           where element.ParentScene == this.name
                           select element.createCode();
            string myCode="";
            foreach (var item in elements)
            {
                myCode += item + "\n\r";
            }
            // ================ => Creation Of Script <= =====================//
            Script sc = new("CodeCreator.lua", new() { ("ElementsOnScreen", myCode) });
            string fullCode = sc.Body["output"].ToStr();

            return fullCode;
        }
        public static string? fullScene; protected static string? sceneCFG; public List<string> groupList = new(); // <= Auflistungen
        private string sceneSwitchCode;

    }


    //===========================================================================================================================================================
    // ------------------- <= Presets & More => ------------------ <= Presets & More => ------------------------- <= Presets & More => -------------------------------------- 
    //===========================================================================================================================================================
    public interface AppCode
    {
        public static Size displayRes = new(1920, 1080);

        public string sceneSwitchCode(string name) { return "local function " + name.ToUpper() + "(event) \n\tcomposer.gotoScene( '" + name + "' ) end"; }

        //    public int appCoordX { get => devicePos.X; }
        //      public int appCoordY { get => devicePos.Y; } // <= Get_Access:AppCoords


        //----------------------------------- CodeBl�cke ---------------------------------------//
        public static string globals = "screenW, screenH = display.contentCenterX, display.contentCenterY \r\n";
        public static string padInit = "-- Gamepad --\r\nlocal controller = { device=\" \", displayName=\" \" }\r\nlocal onKeyEvent, onAxisEvent\r\nlocal function setDevice( device, displayName ) controller[\"device\"] = device controller[\"displayName\"] = displayName end\r\nfunction onKeyEvent( event ) setDevice( event.device, event.device.displayName ) end\r\nfunction onAxisEvent( event ) if (math.abs(event.normalizedValue) > 0.5 ) then setDevice( event.device, event.device.displayName ) end end\r\n";
        public static string padListener = "\r\nRuntime:addEventListener( \"axis\", onAxisEvent ) Runtime:addEventListener( \"key\", onKeyEvent )\r\n\r\n";
        //------------------------------------ Aufbau -------------------------------------------//
        public static Dictionary<string, string> padInputs = new() {
            { "A","buttonA" }, { "B","buttonB" }, { "X","buttonX" },{ "Y","buttonY" },
            { "btL","leftShoulderButton1"}, { "btR","rightShoulderButton1" }, { "start","start"},{ "select","buttonSelect"},
            { "stickPressL","leftJoystickButton" }, {"stickPressR","rightJoystickButton" },
            { "left","left" },{ "up","up" }, { "down","down" }, { "right","right" } };

    }

}
