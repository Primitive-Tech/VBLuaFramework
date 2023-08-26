using VBLua.Core;
using Microsoft.Win32;using System.ComponentModel;using static VBLua.Core.Utilities;
using System.Text.RegularExpressions;using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using VBLua.IDE;
using PrimitiveServerV2;
using System.Configuration;
using System.Security.Permissions;
using System.Text;

namespace VB.Lua.GUIController
{
    public interface Loader
    {
        public static void InitializeContextMenu(Control ctr)
        {
            if (ctr.ContextMenuStrip!= null) { ctr.ContextMenuStrip.Items.Clear(); }   
            // Function calls and descriptions
            string[] functionCalls = new string[]
            {
                ".blink()",
                ".dissolve()",
                ".fadeIn()",
                ".fadeOut()",
                "moveBy()",
                "moveTo()",
                ".scaleBy()",
                ".scaleTo()"
            };

            string[] descriptions = new string[]
            {
                "Repeatedly oscillates the alpha value of an object in and out over the timespan.",
                "Performs a dissolve Movement between two display objects.",
                "Fades an object to alpha of 1.0 over the specified time.",
                "Fades an object to alpha of 0.0 over the specified time.",
                "Moves an object by the specified x and y coordinate amount over a specified time.",
                "Moves an object to the specified x and y coordinates over a specified time.",
                "Scales an object by the specified xScale and yScale amounts over a specified time.",
                "Scales an object to the specified xScale and yScale amounts over a specified time."
            };

            for (int i = 0; i < functionCalls.Length; i++)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(functionCalls[i]);
                menuItem.ToolTipText = descriptions[i];
                string xxx = "transition." + functionCalls[i] + "()";// rect.fill
                //menuItem.Click += MenuItem_Click;
                ctr.ContextMenuStrip.Items.Add(menuItem);
            }
        }

    }
    //========= ======================================================================================================================== ==========================
    ////////////////// <= Syntax =>  //////////////////////// <= Syntax =>  ////////////////////////////////////////        
    public static class SyntaxHighlighterNET
    {
        public static void HighlightOriginal(RichTextBox richTextBox)
        {
            // Löscht zuvor angewendete Formatierungen
            richTextBox.SelectionStart = 0;
            richTextBox.SelectionLength = richTextBox.Text.Length;
            richTextBox.SelectionColor = Color.Black;

            // Definition der Syntax-Hervorhebungsregeln
            var syntaxRules = new[]
            {
                new { RegexPattern = @"\b(Private|Public|Dim|Imports|Module|Class|Void|Sub|Function|If|Then|Else|End\sIf|For|Next|While|Do|Loop|Exit|Break|Return|As)\b", Color = Color.Blue },
                new { RegexPattern = @"""([^""]|"""")*""", Color = Color.Red },
                new { RegexPattern = @"'(.*)$", Color = Color.Green },
                 new { RegexPattern = @"\{|\}|\[|\]|\|=|!|:", Color = Color.Red }
            };

            // Durchlaufen der Syntaxregeln und Anwenden der Formatierung
            foreach (var rule in syntaxRules)
            {
                var regex = new Regex(rule.RegexPattern);
                var matches = regex.Matches(richTextBox.Text);
                foreach (Match match in matches)
                {
                    richTextBox.SelectionStart = match.Index;
                    richTextBox.SelectionLength = match.Length;
                    richTextBox.SelectionColor = rule.Color;
                }
            }
        }

        private static void ConvertKeywordsToUppercase(RichTextBox richTextBox)
        {
            string[] keywords = {
                "private", "public", "dim", "imports", "module", "class",
                "void", "sub", "function", "if", "then", "else", "endif",
                "for", "foreach", "next", "while", "do", "loop",
                "exit", "break", "return", "as"
            };
            string text = richTextBox.Text;
            StringBuilder newText = new StringBuilder(text);

            foreach (string keyword in keywords)
            {
                string pattern = @"\b" + keyword + @"\b";
                string replacement = char.ToUpper(keyword[0]) + keyword.Substring(1);
                newText.Replace(keyword, replacement);
            }
            richTextBox.Text = newText.ToString();
        }


        public static void HighlightOwnCode(RichTextBox richTextBox,string syntax= "lua")
        {         
            // Löscht zuvor angewendete Formatierungen
            richTextBox.SelectionStart = 0;
            richTextBox.SelectionLength = richTextBox.Text.Length;
            richTextBox.SelectionColor = Color.Black;
            // Definition der Syntax-Hervorhebungsregeln für VB.NET
            var syntaxRulesVBL = new[]
            {
                new { RegexPattern = @"\b(Private|Public|Dim|Imports|Module|Class|Void|Sub|Function|If|Then|Else|End\sIf|For|ForEach|Next|While|Do|Loop|Exit|Break|Return|As)\b", Color = Color.Blue },
                new { RegexPattern = @"""([^""]|"""")*""", Color = Color.Red },
                new { RegexPattern = @"'(.*)$", Color = Color.Green },
                 new { RegexPattern = @"\{|\}|\[|\]|\|=|!|:", Color = Color.Red }
            };
            var syntaxRulesLua = new[]{
                    new { RegexPattern = @"\b(local|function|if|then|else|end|for|in|while|do|repeat|until|return|pairs|ipairs|)\b", Color = Color.Blue },
                    new { RegexPattern = @"--(.*)$", Color = Color.Green },
                    new { RegexPattern = @"\b(nil|true|false)\b", Color = Color.Red },
                    new { RegexPattern = @"""([^""]|"""")*""", Color = Color.Red },
                    new { RegexPattern = @"\{|\}|\[|\]|\|=|!|:", Color = Color.Red }
            };

            if (syntax=="lua") { 
                // Durchlaufen der Syntaxregeln und Anwenden der Formatierung
                foreach (var rule in syntaxRulesLua)
                {
                    var regex = new Regex(rule.RegexPattern);
                    var matches = regex.Matches(richTextBox.Text);
                    foreach (Match match in matches)
                    {
                        richTextBox.SelectionStart = match.Index;
                        richTextBox.SelectionLength = match.Length;
                        richTextBox.SelectionColor = rule.Color;
                    }
                }
                richTextBox.SelectionStart = richTextBox.Text.Length;
                richTextBox.SelectionLength = 0;
            }
            else
            {
                ConvertKeywordsToUppercase(richTextBox);

                // Durchlaufen der Syntaxregeln und Anwenden der Formatierung
                foreach (var rule in syntaxRulesVBL)
                {
                    var regex = new Regex(rule.RegexPattern);
                    var matches = regex.Matches(richTextBox.Text);
                    foreach (Match match in matches)
                    {
                        richTextBox.SelectionStart = match.Index;
                        richTextBox.SelectionLength = match.Length;
                        richTextBox.SelectionColor = rule.Color;
                    }
                }
                richTextBox.SelectionStart = richTextBox.Text.Length;
                richTextBox.SelectionLength = 0;
            }
        }
    }
    //========= ======================================================================================================================== ==========================
    ////////////////// <= Formatting =>  //////////////////////// <= Formatting =>  ////////////////////////////////////////        
 
    public static class RTBExtensions
    {
        public static void AutoIndentVB(this RichTextBox richTextBox)
        {
            int currentLineIndex = richTextBox.GetLineFromCharIndex(richTextBox.SelectionStart);
            int currentLineStartIndex = richTextBox.GetFirstCharIndexFromLine(currentLineIndex);
            string currentLineText = richTextBox.Lines[currentLineIndex];
            // Zählen der Leerzeichen am Anfang der aktuellen Zeile
            int indentSpaces = Regex.Match(currentLineText, @"^\s*").Length;
            // Bestimmen der gewünschten Einrückungstiefe für die nächste Zeile
            int nextLineIndentLevel = GetNextLineIndentLevel(currentLineText, indentSpaces);
            // Erzeugen des Einrückungstexts für die nächste Zeile
            string nextLineIndent = new string(' ', nextLineIndentLevel * 4);
            // Einfügen des Einrückungstexts an der aktuellen Cursorposition
            richTextBox.SelectedText = nextLineIndent;
        }
        private static int GetNextLineIndentLevel(string currentLineText, int currentIndentSpaces)
        {
            // Überprüfen, ob die aktuelle Zeile mit einer Steuerstruktur oder einer Codezeile endet
            bool endsWithControlStructure =
                Regex.IsMatch(
                    currentLineText, @"^\s*(if|for|while|do|select|case|sub|function|elif|try|catch|finally|else|elseif|End)\b.*$");
            bool endsWithCodeLine = Regex.IsMatch(currentLineText, @"^\s*(\w|\s)*(?!end\b).*$");
            // Bestimmen der gewünschten Einrückungstiefe für die nächste Zeile basierend auf der aktuellen Zeile
            if (endsWithControlStructure)
            {
                return currentIndentSpaces + 1;
            }
            else if (endsWithCodeLine)
            {
                return currentIndentSpaces;
            }
            else
            {
                return Math.Max(currentIndentSpaces - 1, 0);
            }
        }

        //#################################################################################################
        private static ListBox listBoxSuggestions; // Definition der ListBox
        private static Dictionary<string, string> codeDictionary = new Dictionary<string, string>()
        {
            { "var", "Variable erstellen" },
            { "if", "Bedingte Anweisung" },
            { "for", "Schleifenanweisung" },
        };
  
        public static void InitializeCodeCompletion(RichTextBox rtb)
        {
            listBoxSuggestions = new ListBox(); // Initialisierung der ListBox
            listBoxSuggestions.Visible = false;
            listBoxSuggestions.Location = new Point(0, 0);
            listBoxSuggestions.Size = new Size(200, 100);
            listBoxSuggestions.DoubleClick += listBoxSuggestions_DoubleClick;

            rtb.Controls.Add(listBoxSuggestions); // Hinzufügen der ListBox zur RichTextBox
            rtb.KeyUp += RTB_KeyUp;
            rtb.TextChanged += RTB_TextChanged;
            Rtb = rtb; // <== Übergebe Referenz intern
        }
        private static void RTB_KeyUp(object sender, KeyEventArgs e)
        {
            RichTextBox rtb =(RichTextBox)sender;
            if (e.KeyCode == Keys.Space && e.Modifiers == Keys.Control)
            {
                ShowCodeSuggestions(rtb);
            }
        }
        private static void RTB_TextChanged(object sender, EventArgs e)
        {
            HideCodeSuggestions();
        }
        public static void ShowCodeSuggestions(RichTextBox rtb)
        {
            string[] words = rtb.Text.Split(' ', '\n', '\r');
            string lastWord = words.LastOrDefault();
            if (lastWord != null && lastWord.Length > 0){
                List<string> suggestions = new();
                suggestions.AddRange(codeDictionary
                    .Where(kv => kv.Key.StartsWith(lastWord))
                    .Select(kv => kv.Key));
                if (suggestions.Any()){
                    listBoxSuggestions.Items.Clear();
                    listBoxSuggestions.Items.AddRange(suggestions.ToArray());

                    Point caretLocation = rtb.GetPositionFromCharIndex(rtb.SelectionStart);
                    Point listBoxLocation = new Point(caretLocation.X, caretLocation.Y + 20);
                    listBoxSuggestions.Location = listBoxLocation; listBoxSuggestions.Font = rtb.Font;
                    listBoxSuggestions.Size= listBoxSuggestions.GetPreferredSize(listBoxSuggestions.Size );
                    listBoxSuggestions.Visible = true; 
                    listBoxSuggestions.BringToFront();
                }
            }
        }
        private static void HideCodeSuggestions()
        {
            listBoxSuggestions.Visible = false;
        }
        public static RichTextBox Rtb;
        private static void listBoxSuggestions_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxSuggestions.SelectedItem != null)
            {
                string selectedSuggestion = listBoxSuggestions.SelectedItem.ToString();
                string[] words = Rtb.Text.Split(' ', '\n', '\r');
                words[words.Length - 1] = selectedSuggestion;
                Rtb.Text = string.Join(" ", words);
                HideCodeSuggestions();
            }
        }
}
//========= ======================================================================================================================== ==========================
////////////////// <= Appearence =>  //////////////////////// <= Appearance =>  ////////////////////////////////////////        

public static class ControlHighlighter
    {
        public static bool continueHighlight =false;
        public static async Task Blinking(Control control,int speed = 3)
        {
            continueHighlight = false;
            const int animationInterval = 2; // Anpassen des Intervalls der Animation in Millisekunden
            Color highlightColor = Color.White; // Anpassen der Farbe des Hervorhebungseffekts
            // Erstellen eines TaskCompletionSource-Objekts, um das Ende der Animation zu signalisieren
            TaskCompletionSource<object> animationCompletionSource = new TaskCompletionSource<object>();
            // Erstellen eines Timer-Objekts für die Animation
            System.Windows.Forms.Timer animationTimer = new();
            animationTimer.Interval = animationInterval; 
            // Speichern der ursprünglichen Hintergrundfarbe
            Color originalBackColor = control.BackColor;
            // Festlegen der Anfangs- und Endfarbe für die Animation
            Color startColor = originalBackColor;
            Color endColor = highlightColor;
            // Animationsschritte
            int currentStep = 40;
            // Ereignishandler für den Timer
            animationTimer.Tick += (sender, e) =>
            {
                // Überprüfen, ob die Hervorhebung fortgesetzt werden soll
                if (!continueHighlight)
                {
                    // Zurücksetzen der Hintergrundfarbe auf die ursprüngliche Farbe
                    control.BackColor = originalBackColor;
                    // Stoppen des Timers
                    animationTimer.Stop();
                    animationTimer.Dispose();
                    // Signalisieren des Abschlusses der Animation
                    animationCompletionSource.SetResult(null); continueHighlight = true;
                    return;
                }
                // Berechnung der aktuellen Hintergrundfarbe basierend auf dem Animationsschritt
                int red = startColor.R + (int)((endColor.R - startColor.R) * (float)currentStep / 255);
                int green = startColor.G + (int)((endColor.G - startColor.G) * (float)currentStep / 255);
                int blue = startColor.B + (int)((endColor.B - startColor.B) * (float)currentStep / 255);
                Color currentColor = Color.FromArgb(red, green, blue);
                // Festlegen der aktuellen Hintergrundfarbe des Steuerelements
                control.BackColor = currentColor;
                // Inkrementieren des Animationsschritts
                currentStep = (currentStep + speed) % 256;
            };
            // Starten des Timers
            animationTimer.Start();
            // Warten auf das Ende der Animation
            await animationCompletionSource.Task; control.BackColor = originalBackColor; 
        }
    }

    public partial class Menu1 : Form
    {
        public partial class CustomForm : Form
        {
            private List<(string Name, string Location, string Settings)> menuItems; ListView listViewMenu=new();

            public CustomForm(List<(string Name, string Location, string Settings)> items)
            {
                menuItems = items;
            }

            private void CustomForm_Load(object sender, EventArgs e)
            {
                listViewMenu.View = View.Details;
                listViewMenu.FullRowSelect = true;
                listViewMenu.Columns.Add("Name", 100);
                listViewMenu.Columns.Add("Location", 150);
                listViewMenu.Columns.Add("Settings", 150);

                foreach (var item in menuItems)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { item.Name, item.Location, item.Settings });
                    listViewMenu.Items.Add(listViewItem);
                }
            }

            private void buttonAdd_Click(object sender, EventArgs e)
            {
                List<(string Name, string Location, string Settings)> selectedItems = new List<(string, string, string)>();

                foreach (ListViewItem item in listViewMenu.SelectedItems)
                {
                    string name = item.SubItems[0].Text;
                    string location = item.SubItems[1].Text;
                    string settings = item.SubItems[2].Text;

                    selectedItems.Add((name, location, settings));
                }

                // Rückgabe der ausgewählten Einträge als anonymes Objekt
                this.Tag = selectedItems;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        public partial class MenuDB : Form
        {
            private List<(string, string, string)> menuItems;

            public MenuDB()
            {
                InitializeMenuItems();
            }

            private void InitializeMenuItems()
            {
                // Beispiel-Menüeinträge hinzufügen
                menuItems = new List<(string, string, string)>
            {
                ("Entry 1", "Scene1", "Option1"),
                ("Entry 2", "Scene2", "Option2"),
                ("Entry 3", "Scene3", "Option3"),
                // Weitere Einträge können hier hinzugefügt werden
            };
            }

            private void buttonOpenMenu_Click(object sender, EventArgs e)
            {
                // Öffnen der benutzerdefinierten Form
                CustomForm customForm = new CustomForm(menuItems);
                DialogResult result = customForm.ShowDialog();

                // Überprüfen, ob der Benutzer "Add" geklickt hat und die ausgewählten Einträge erhalten
                if (result == DialogResult.OK)
                {
                    List<(string, string, string)> selectedItems = (List<(string, string, string)>)customForm.Tag;

                    // Verarbeite die ausgewählten Einträge
                    foreach (var item in selectedItems)
                    {
                        // Hier kannst du die ausgewählten Einträge verarbeiten
                        // item.Name, item.Location und item.Settings enthalten die entsprechenden Werte
                        MessageBox.Show($"Selected: {item.Item1}, {item.Item2}, {item.Item3}");
                    }
                }
            }
        }


    public partial class Solar2DRadioButtonControl : Panel
        {
            private List<RadioButton> radioButtons; private RadioButton radioButton1;//private Panel this;
            private string radioGroupID { get => this.Name; set => this.Name = value; } public string radioButtonsCode { get => GetLuaCode(); } string txt = "xx";public string Txt { get => txt; set => txt = value; }

            public Solar2DRadioButtonControl(string name,int number )
            {
                radioButtons = new(); this.Size = new(200,200);this.BackColor = Color.Transparent; this.ForeColor = Color.Black;
                    this.Name = name;  this.AutoSize = true;InitializeRadioButtons(number); 
            }
            private void InitializeRadioButtons(int number)
            {
                this.Text = radioGroupID;
                for (int i = 0; i< number; i++) { 
                radioButton1 = new RadioButton();
            // Radio Buttons
            radioButton1.Location = new Point(this.Location.X + 4, this.Location.Y+(this.Size.Height/2) - (number * 2));
            radioButton1.Width = this.Size.Width/2; radioButton1.Text = txt;
                    radioButton1.Height = this.Size.Height/ number;
     
            radioButton1.Checked = true; radioButton1.ForeColor = Color.Black;
                    radioButton1.CheckedChanged += RadioButton_CheckedChanged;
            radioButton1.Name = "radioButton_" + i.ToStr(); this.radioButtons.Add(radioButton1);
            this.Controls.Add(radioButton1);
                }     
            }

            private void RadioButton_CheckedChanged(object sender, EventArgs e){
                RadioButton radioButton = (RadioButton)sender ;
                if (radioButton.Checked)
                {
                    radioButton1.Checked = (radioButton == radioButton1);
                }
            }

        public string RadioButton1ID
        {
            get { return radioGroupID; }
            set
            {
                radioGroupID = value;
                radioButton1.Text = radioGroupID;
            }
        }

        public string GetLuaCode()
        {
                string luaCode = $@"local {this.Name} = display.newRect( {this.Location.X}, {this.Location.Y}, {this.Size.Width}, {this.Size.Height}, {this.Name}) {this.Name}:setFillColor(1, 1, 1) -- White color for the parent rectangle
"; 
                foreach (RadioButton radioButton in radioButtons) { 
                  luaCode += $@"
                    local {radioButton.Name} = widget.newSwitch({ this.Name },
                        {{
                            style = ""radio"",id = ""{radioButton.Name}"",width = 40,height = 10,
                            initialSwitchState = false,frameOff = 1,frameOn = 2
                        }}
                    )
                    radioGroup:insert({radioButton.Name})
                    {radioButton.Name}.x = {this.Name}.x    {radioButton.Name}.y = {this.Name}.y + {radioButton.Name}.height";
                }
            luaCode += $"\nscene.view:insert({this.Name})";
                return luaCode;
        }
        }

        //##################################################################################################################################
        [ToolboxItem(true), DesignTimeVisible(true)]
        public partial class Solar2DTableControl : DataGridView
        {
            private List<dynamic> ListBoxes;  private ListBox ListBox1;//private Panel this;
            public string ListBoxCode { get => GetLuaCodeFromTable(); }
   

            public Solar2DTableControl(string name,List<dynamic> dataframe)
            {
                ListBoxes = new(); this.Name = name; this.AutoSize = true; ListBoxes = dataframe;
                this.Size = new Size(500, 250); this.BackColor = Color.White; this.ForeColor = Color.Black;
                InitializeListBoxs(ListBoxes.Count());
            }

            private void InitializeListBoxs(int number)
            {           
                this.Location = new Point(this.Location.X + 4, this.Location.Y + (this.Size.Height / 2) - (number * 2));
                this.ColumnCount = number;
                this.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
                this.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                this.ColumnHeadersDefaultCellStyle.Font =
                    new Font(this.Font, FontStyle.Bold);

                this.AutoSizeRowsMode =
                    DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                this.ColumnHeadersBorderStyle =
                    DataGridViewHeaderBorderStyle.Single;
                this.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                this.GridColor = Color.Black;
                this.RowHeadersVisible = false;

                this.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;
                this.MultiSelect = false;
                this.Dock = DockStyle.Fill;

                for (int i = 0; i < number; i++)
                {
                    string gameObject = "GameObject_" + i.ToStr()+"_" + ListBoxes[i].id; string gameObjectVariable ="::";
                    this.Columns[i].Name = i.ToStr() + gameObjectVariable;
                    this.Columns[i].DisplayIndex = 3; this.Rows.Add(gameObject); this.Columns[i].DefaultCellStyle.Font =
                    new Font(this.DefaultCellStyle.Font, FontStyle.Italic);

                }
            }
            [
            Category("Console_Specifications"),
            Description("Handles Windows- and Scripting- and PrimitiveServer-CommandLine.")
            ]           
            [SettingsDescription("Rescources and Design")]
            public List<Image> Images => new();


            public string GetLuaCodeFromTable()
            {
                string luaCode = @"local function onRowRender( event )
                            local row = event.row
                            local rowHeight = row.contentHeight local rowWidth = row.contentWidth
                            local rowTitle = display.newText( row, ""Row "" .. row.index, 0, 0, nil, 14 )
                            rowTitle:setFillColor( 0 ) 
                            rowTitle.anchorX = 0
                            rowTitle.x = 0  rowTitle.y = rowHeight * 0.5
                        end";
                luaCode += "\n\r";
                luaCode+= $@"local {this.Name} = widget.newTableView({this.Name},{{
                            id = ""{this.Name}"", left = {this.Location.X},top = {this.Location.X},height = {this.Size.Width}, width={this.Size.Height} ,onRowRender = onRowRender,onRowTouch = onRowTouch,listener = scrollListener
                        }})"; //{this.Name}.x = {this.Name}.x    {this.Name}.y = {this.Name}.y + {this.Name}.height";

                luaCode += $"\nscene.view:insert({this.Name})\n";
                return luaCode;
            }
        }
    }
}