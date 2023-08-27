
using static VBLua.Core.Utilities;
using VBLua.Core;
using System.Windows.Forms;
using System.ComponentModel.Design;
using VB.Lua.GUIController;

namespace VBLua.IDE
{
    public partial class SolarDialog_1 : Form,Loader
    {
        public Element preloadedElement; public Element createdElementData; public string mode; string parentScene;
        public string ID { get => this.setName.Text; set => this.setName.Text = value; }
        public string? type;
        Color FColor = Color.Black, BgColor = Color.WhiteSmoke;
        public int width { get => this.setW.Value.ToInt(); set => this.setW.Value = Convert.ToDecimal(value); }
        public int height { get => this.setH.Value.ToInt(); set => this.setH.Value = Convert.ToDecimal(value); }
        public Color fColor { get => FColor; set => FColor = value; }
        public Color bgColor { get => BgColor; set => BgColor = value; }
        Image? preloadedImage; string resc;
        public string Resc { get => resc; set => resc = value; }

        public Button Okckl { get => okClk; set => okClk = value; }
        //------------------------------------------------------------------------------------------------------------
        public SolarDialog_1(Element element, string parentScene)
        {
            this.parentScene = parentScene;
            InitializeComponent(); this.fColor = element.fontColor; this.bgColor = element.bgColor;
            this.ID = element.name; this.label1.Text = element.name; this.width = element.RealSize.Width; this.height = element.RealSize.Height;
            this.preloadedElement = element; resc = element.Rescource; this.Visible = false; this.LocationChanged += NewLocation;
            FontEdit.Click += ForeClick; BGEdit.Click += BGClick; setAll();
        }

        private void setAll()
        {
            if (preloadedElement != null)
            {
                switch (mode)
                {
                    case "create":
                        this.label1.Text = preloadedElement.name.ToUpper(); break;
                    case "edit":
                        if (createdElementData != null) { this.label1.Text = createdElementData.name.ToUpper(); break; }
                        break;
                }
            }
        }

        public void DoAction()
        {
            setAll();
            if (preloadedElement != null && ID != null)
            {
                switch (mode)
                {
                    case "create":
                        createdElementData = new(this.ID, new Size(this.width, this.height),
                            this.Location, preloadedElement.type, (bgColor, fColor));
                        break;
                    case "edit":
                        createdElementData = new(this.ID, new Size(this.width, this.height),
                           this.Location, preloadedElement.type, (bgColor, fColor));
                        break;
                }
                createdElementData.ParentScene = parentScene; createdElementData.name = this.ID; createdElementData.id = this.ID;
                createdElementData.text = setName.Text; createdElementData.Rescource = Resc;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        string pickedColor = "bg";
        private void ColorDialog1_1Go()
        {
            if (pickedColor == "bg")
            {
                this.bgColor = colorDialog1.Color;
            }
            else if (pickedColor == "font") { this.fColor = colorDialog1.Color; }
        }

        private void BGClick(object sender, EventArgs e)
        {
            pickedColor = "bg"; var result = colorDialog1.ShowDialog(); if (result == DialogResult.OK) { ColorDialog1_1Go(); } else { MessageBox.Show("Failed!"); }
        }
        private void ForeClick(object sender, EventArgs e)
        {
            pickedColor = "font"; var result = colorDialog1.ShowDialog(); if (result == DialogResult.OK) { ColorDialog1_1Go(); } else { MessageBox.Show("Failed!"); }
        }
        private void EditMoreClick(object sender, EventArgs e)
        {
            Loader.InitializeContextMenu(createdElementData.createCtr());
        }
         
        public void closeMe(object sender, EventArgs e) { this.Visible = false; }

        public void NewLocation(object sender, EventArgs e)
        {
            // Überprüfen, ob das ContextMenuStrip den erlaubten Randbereich verlässt
            if (this.Left < 0)
            {
                // Den linken Rand korrigieren
                this.Left = 0;
            }
            else if (this.Right > Screen.PrimaryScreen.WorkingArea.Right)
            {
                // Den rechten Rand korrigieren
                this.Left = Screen.PrimaryScreen.WorkingArea.Right - this.Width * 2;
            }

            if (this.Top < 0)
            {

            }
            else if (this.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
            {
                // Den unteren Rand korrigieren
                this.Location = new(Screen.PrimaryScreen.WorkingArea.Bottom - this.Height * 2);
            }
        }


        private void EditResc_Click(object sender, EventArgs e)
        {
            if (ImageDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = ImageDialog1.SafeFileName; mode = "edit";
                if (selectedPath != null) { Resc = selectedPath; DoAction(); } //=> Apply 

            }
        }

        private void setName_TextChanged(object sender, EventArgs e)
        {

        }

        private void DialogPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SolarDialog_1_Load(object sender, EventArgs e)
        {

        }

        private void delClk_Click(object sender, EventArgs e)
        {

        }
    }

    public static class ControlAttributeChanger
    {
        public static void ChangeControlAttributes(Control control, string name, int width, int height, Color backgroundColor, Color foreColor, float fontSize)
        {
            control.Name = name;
            control.Height = height;
            control.Width = width;
            control.BackColor = backgroundColor;
            control.ForeColor = foreColor;
            control.Font = new Font(control.Font.FontFamily, fontSize, control.Font.Style);

            // Untergeordnete Elemente ändern
            foreach (Control childControl in control.Controls)
            {
                ChangeControlAttributes(childControl, name, width, height, backgroundColor, foreColor, fontSize);
            }
        }
    }
    ////------------------------------------------------------------------------------------------------------------

    public class Toolstrip : UserControl
    {
        private PictureBox Preview; private Bitmap preBitmap; private Image preImg; private OpenFileDialog openFileDialog;
        public ContextMenuStrip contextMenuStrip; private ToolStripMenuItem editToolStripMenuItem; private ToolStripMenuItem renameToolStripMenuItem; private ToolStripMenuItem attachToolStripMenuItem; private ToolStripMenuItem scriptingToolStripMenuItem; private ToolStripMenuItem deleteToolStripMenuItem;

        public Toolstrip(Control ctr)
        {
            this.contextMenuStrip = new ContextMenuStrip();
            this.Name = "Toolstrip";
            this.contextMenuStrip.BackColor = Color.FloralWhite;
            this.contextMenuStrip.GripStyle = ToolStripGripStyle.Visible;
            this.contextMenuStrip.ImageScalingSize = new Size(20, 20);
            this.contextMenuStrip.LayoutStyle = ToolStripLayoutStyle.Flow;
            this.contextMenuStrip.RenderMode = ToolStripRenderMode.System;
            this.contextMenuStrip.Name = "Toolstrip";

            this.editToolStripMenuItem = new ToolStripMenuItem("Edit..");
            this.attachToolStripMenuItem = new ToolStripMenuItem("Attach..");
            this.scriptingToolStripMenuItem = new ToolStripMenuItem("Scripting");
            this.renameToolStripMenuItem = new ToolStripMenuItem("Rename");
            this.deleteToolStripMenuItem = new ToolStripMenuItem("Delete");

            this.contextMenuStrip.Items.Add(editToolStripMenuItem);
            this.contextMenuStrip.Items.Add(attachToolStripMenuItem);
            this.contextMenuStrip.Items.Add(scriptingToolStripMenuItem);
            this.contextMenuStrip.Items.Add(deleteToolStripMenuItem);
            this.contextMenuStrip.BringToFront();

            this.scriptingToolStripMenuItem.Click += ScriptingToolClick;
            this.attachToolStripMenuItem.Click += AttachRescource;
            openFileDialog = new OpenFileDialog();
            this.contextMenuStrip.BringToFront();

            this.contextMenuStrip.AutoSize = true;
            this.contextMenuStrip.AutoClose = true; this.contextMenuStrip.Dock = DockStyle.Right;
            ctr.ContextMenuStrip = this.contextMenuStrip;
        }
        //////// <= Events => ////////

        private void ScriptingToolClick(object sender, EventArgs e)
        {

        }
        private void AttachRescource(object sender, EventArgs e)
        {

            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
            openFileDialog.Title = "Select an Image File";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog.FileName;
                Image image = Image.FromFile(selectedFile);
                Preview.Image = image; preImg = image;
                Preview.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        //taskkill /IM <Prozessname>.exe  NeedForSpeedHeat
        //wmic process where name='<Prozessname>.exe' CALL setpriority "<Priorität>"
    }
}