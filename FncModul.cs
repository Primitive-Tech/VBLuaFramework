using System.Runtime.CompilerServices;
using System.Speech.Synthesis;
using BasicAudio;using Image =  System.Drawing.Image;
using Microsoft.VisualBasic.CompilerServices;
using NLua;
using Newtonsoft.Json;
using System.IO.Compression;
using static VBLua.Core.Utilities;
using System.Data;
using System;

namespace VBLua.Core
{
    //################################################ Converter_All #######################################################
    [StandardModule]
    public static class Utilities
    {
        // Check
        public static bool Check(this object obj)
        {
            return obj.GetType() != null;
        }

        // CheckType (object)
        public static bool CheckType(this object obj, Type type)
        {
            return obj.GetType() == type;
        }

        // CheckType (object[])
        public static bool CheckType(this object[] obj, Type type)
        {
            return obj.GetType() == type;
        }

        // ToStr
        public static string ToStr(this int array)
        {
            return Convert.ToString(array);
        }      
        // ToStr
        public static string ToStr(this object integer)
        {
            return (string)integer;
        }

    // ToInt (double)
    public static int ToInt(this double x)
        {
            return Convert.ToInt32(x);
        }

        // ToNumber (object)
        public static int ToNumber(this object obj)
        {
            return (int)obj;
        }

        // ToInt (decimal)
        public static int ToInt(this decimal x)
        {
            return Convert.ToInt32(x);
        }

        // ToInt (string)
        public static int ToInt(this string x)
        {
            return Convert.ToInt32(x);
        }

        // ToDouble (int)
        public static double ToDouble(this int x)
        {
            return Convert.ToDouble(x);
        }

        // ToDouble (decimal)
        public static double ToDouble(this decimal x)
        {
            return Convert.ToDouble(x);
        }

        // ToDec (int)
        public static decimal ToDec(this int x)
        {
            return Convert.ToDecimal(x);
        }

        // ToDec (double)
        public static decimal ToDec(this double x)
        {
            return Convert.ToDecimal(x);
        }

        // ToBool (string)
        public static bool ToBool(this string x)
        {
            return Convert.ToBoolean(x);
        }
        //################################################ Utility #######################################################
        private static AudioPlayer player;
        private static SpeechSynthesizer tts;
        // Mp3
        public static void Mp3(this string audioFile, string path = "")
        {
            player = new AudioPlayer(path);
            try
            {
                player.Filename = audioFile;
                player.Play();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                player.Close();
            }
        }

        // Say
        public static void Say(this string txt, int volume = 100)
        {
            tts = new SpeechSynthesizer();
            tts.SetOutputToDefaultAudioDevice();
            tts.Rate = 2;
            tts.Volume = volume;
            var prompt = new Prompt(txt);
            if (txt.Check())
            {
                tts.Speak(prompt);
                tts.Dispose();
            }
        }

        // SayAsync
        public static async Task<Action<string>> SayAsync(string txt, int volume = 100)
        {
            tts = new SpeechSynthesizer();
            tts.SetOutputToDefaultAudioDevice();
            tts.Rate = 2;
            tts.Volume = volume;
            var prompt = new Prompt(txt);
            tts.SpeakAsync(prompt);
            tts.Dispose();
            return str => { };
        }

        // Unzip
        public static void Unzip(string fileName, string path)
        {
            using (FileStream stream = File.Open(fileName + ".zip", FileMode.Open))
            {
                try
                {
                    using (var archive = new ZipArchive(stream))
                    {
                        archive.ExtractToDirectory(path);
                    }
                }
                catch
                {
                    Msg("Failed Compression");
                }
            }
        }

        // Zip
        public static void Zip(string path, string destination)
        {
            try
            {
                ZipFile.CreateFromDirectory(path, destination);
            }
            catch
            {
                Msg("Failed Compression");
            }
        }

        // Msg (string)
        public static void Msg(string txt)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                MessageBox.Show(txt);
            }
        }

        // Msg (List<string>)
        public static void Msg(this List<string> list, string comment = "")
        {
            if (list.Count > 0)
            {
                var txtAsRichText = list
                    .Where(line => line.GetType() == typeof(string))
                    .Select(line => comment + " => " + line + " ");

                if (txtAsRichText.Any())
                {
                    var fullText = string.Join("", txtAsRichText);
                    MessageBox.Show(fullText + "\nLines: " + list.Count + ", " + txtAsRichText.Count());
                }
            }
        }
        //PictureBoxen Laden
        public static void LoadAllAsync(this Control[] controls)
        {
            var UIElements = from Control ctr in controls
                             where ctr.GetType() == typeof(PictureBox) && ctr.Tag != null
                             select ctr;

            var imageList = new Dictionary<PictureBox, Image>();

            // Load Images
            foreach (var element in UIElements)
            {
                string newResc = element.Tag.ToStr();
                if (newResc != null && element.GetType() == typeof(PictureBox))
                {
                    var createdResc = Image.FromFile(newResc);
                    imageList.Add((PictureBox)element, createdResc);
                }
            }
            foreach (var element in imageList)
            {
                element.Key.SizeMode = PictureBoxSizeMode.StretchImage;
                element.Key.Image = element.Value;
            }
        }   
        //################################################ JSON Loaders #######################################################
        private static JsonSerializerSettings settings = new JsonSerializerSettings();

        // fromJSON (string)
        public static object[] FromJson(this string array)
        {
            settings.Formatting = Formatting.Indented;
            return JsonConvert.DeserializeObject<object[]>(array, settings);
        }

        // fromJSON (string, bool)
        public static List<object> FromJson(this string array, bool shuffle = false)
        {
            settings.Formatting = Formatting.Indented;
            return JsonConvert.DeserializeObject<List<object>>(array, settings);
        }

        // toJSON (object[])
        public static string ToJson(this object[] array)
        {
            settings.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(array, settings);
        }

        // toJSON (List<object>)
        public static string ToJson(this List<object> list)
        {
            settings.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(list, settings);
        }

        // toJSON (object)
        public static string ToJson(this object list)
        {
            settings.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(list, settings);
        }

        //################################################ VBL Scripting_Language #######################################################
        public static Lua? vbl;

        public static object Execute( ref Script script, bool useOwnSyntax = false, bool localFile = false)
        {
            if (script.Engine == null)
            {
                script.Output = new object[3] { false, new LuaTable(1, script.Engine), "ScriptObject Not found°" };
                    script.Engine = new Lua();
                Preload(ref script.Engine);
                if (script.MyNamespaces.Any())
                {
                    LoadImports(ref script.Engine, script.MyNamespaces);
                }
            }
            bool flag = script.Code.Check();
            if (flag)
            {
                    script.Output = new object[3] { false,new object[]{}, "Error while Preloading" };
                if (script.ScriptArgs.Any())
                {
                    foreach (var item in script.ScriptArgs)
                    {
                            script.Engine[item.Key] = item.Value;
                    }
                }
                try
                {
                    switch (script.isLocalFile)
                    {
                        case false:
                            script.Output = script.Engine.DoString(script.Code);
                            script.StatusResponse = (bool)(script.Output.First());
                            script.RespBody = (object[])script.Output[1];
                            script.Succeed = (bool)(script.Output[2]);
                                script.ErrorCode = (string)(script.Output[3]);
                            break;
                        case true:
                            script.Output = script.Engine.DoFile(script.DataPath);
                            script.StatusResponse = (bool)(script.Output.First());
                            script.RespBody = (object[])script.Output[1];
                            script.Succeed = (bool)(script.Output[2]);
                               script.ErrorCode = (string)(script.Output[3]);
                            break;
                    }
                }
                catch
                {
                        script.Output = new object[3] { false,new object[]{}, "Error while Executing!" };
                        script.StatusResponse = false;
                }
            }
            return script.Output;           
        }

        public static Lua Preload(ref Lua lua)
        {
            lua.LoadCLRPackage();
            lua.DoString(" import ('System.IO') import ('System.Windows.Forms')import ('System.Net') import ('System.Web') import ('System.Net.Http') ");
            return lua;
        }
        public static Lua LoadImports(ref Lua lua, string[] importList)
        {
            if (importList.Any())
            {
                IEnumerable<string> enumerable = importList.Select((string importName) => importName);
                foreach (string item in enumerable)
                {
                    lua.DoString(" import('" + item + "') ");
                }
            }
            return lua;
        }

        public static Script Log(ref Script script, bool localFile = false)
        {
            object[] array = (object[])Execute(ref script, localFile);
            File.WriteAllLines("Logs.txt", array.ToText());
            return script;
        }
        //########################################## Collections #########################################################
        // NoneType_Array to String_Array
        public static string[] ToText(this object[] array)
        {
            return (string[])array;
        }
        // Table_Object (INI File_Style)
        public static void ToDataframe(this Script Data)
        {
            if (Data.CodeSource != null)
            {
                if (Data.Code.Any())
                {
                    object[] outp = null;
                    string[] dataEinträge = Data.Code.Split(",");
                    // GET_ALL
                    var datasets = from codeZeile in dataEinträge
                                   let dataEintrag = codeZeile
                                   select dataEintrag;
                    var Table = new List<KeyValuePair<string, object>>();
                    object[] SearchClient = Data.Engine.DoString(Data.Code);
                    foreach (var founded in SearchClient)
                    {
                        Table.Add(KeyValuePair.Create("", founded));
                    }
                    if (Table.Any())
                    {
                        Data.Dataframe = Table;
                        MessageBox.Show("Loaded");
                    }
                }
            }
        }
        // Table_Object (From Text)
        public static IEnumerable<KeyValuePair<string, string>>? ToStringTable(this Script Data, char chunkBorder = '^')
        {
            if (Data.CodeSource != null)
            {
                var datasets = from codeZeile in Data.CodeSource
                               let code = KeyValuePair.Create(codeZeile.Split(chunkBorder)[0], codeZeile.Split(chunkBorder)[1])
                               select code; 
                return datasets;

            }
            return null;
        }
        public static void Msg(string title, string txt) { MessageBox.Show(title,txt); }
    }
}