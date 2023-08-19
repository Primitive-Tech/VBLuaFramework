using VBLua.Core;
namespace VBLua.IDE
{
    partial class Designer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Designer));
            mainmenustrip = new MenuStrip();
            SceneAusw = new ToolStripComboBox();
            NewElement = new ToolStripMenuItem();
            Geometrics = new ToolStripMenuItem();
            Rectangle = new ToolStripMenuItem();
            Circle = new ToolStripMenuItem();
            Line = new ToolStripMenuItem();
            Buttons = new ToolStripMenuItem();
            ButtonStrip = new ToolStripMenuItem();
            RoundedButton = new ToolStripMenuItem();
            Stepper = new ToolStripMenuItem();
            RadioButton = new ToolStripMenuItem();
            Widgets = new ToolStripMenuItem();
            Progressbar = new ToolStripMenuItem();
            sliderToolStripMenuItem = new ToolStripMenuItem();
            webViewToolStripMenuItem = new ToolStripMenuItem();
            Graphics = new ToolStripMenuItem();
            Textures = new ToolStripMenuItem();
            imageRectToolStripMenuItem = new ToolStripMenuItem();
            Text = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            Composer = new ToolStripMenuItem();
            animateToolStripMenuItem = new ToolStripMenuItem();
            spriteToolStripMenuItem = new ToolStripMenuItem();
            specialEffectsToolStripMenuItem = new ToolStripMenuItem();
            File = new ToolStripMenuItem();
            saveAllToolStripMenuItem = new ToolStripMenuItem();
            saveProjectToolStripMenuItem = new ToolStripMenuItem();
            saveSceneToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            DB = new ToolStripMenuItem();
            Scripting = new ToolStripMenuItem();
            TestButton = new ToolStripMenuItem();
            toolStripTextBox1 = new ToolStripTextBox();
            colorDialog1 = new ColorDialog();
            ScreenPanel = new Panel();
            CodeEdit = new Input();
            mainmenustrip.SuspendLayout();
            SuspendLayout();
            // 
            // mainmenustrip
            // 
            mainmenustrip.BackColor = Color.DodgerBlue;
            resources.ApplyResources(mainmenustrip, "mainmenustrip");
            mainmenustrip.GripStyle = ToolStripGripStyle.Visible;
            mainmenustrip.ImageScalingSize = new Size(20, 20);
            mainmenustrip.Items.AddRange(new ToolStripItem[] { SceneAusw, NewElement, Composer, File, DB, Scripting, TestButton, toolStripTextBox1 });
            mainmenustrip.Name = "mainmenustrip";
            mainmenustrip.RenderMode = ToolStripRenderMode.System;
            // 
            // SceneAusw
            // 
            SceneAusw.AutoCompleteCustomSource.AddRange(new string[] { resources.GetString("SceneAusw.AutoCompleteCustomSource"), resources.GetString("SceneAusw.AutoCompleteCustomSource1"), resources.GetString("SceneAusw.AutoCompleteCustomSource2") });
            resources.ApplyResources(SceneAusw, "SceneAusw");
            SceneAusw.BackColor = SystemColors.InactiveCaption;
            SceneAusw.Name = "SceneAusw";
            SceneAusw.TextChanged += SceneAusw_TextChanged;
            // 
            // NewElement
            // 
            NewElement.DropDownItems.AddRange(new ToolStripItem[] { Geometrics, Buttons, Widgets, Graphics, Text, newToolStripMenuItem });
            NewElement.ForeColor = SystemColors.ActiveCaptionText;
            NewElement.Name = "NewElement";
            NewElement.Padding = new Padding(5, 0, 20, 0);
            resources.ApplyResources(NewElement, "NewElement");
            // 
            // Geometrics
            // 
            Geometrics.DropDownItems.AddRange(new ToolStripItem[] { Rectangle, Circle, Line });
            Geometrics.Name = "Geometrics";
            resources.ApplyResources(Geometrics, "Geometrics");
            Geometrics.Click += ElementWindowClick;
            // 
            // Rectangle
            // 
            Rectangle.Name = "Rectangle";
            resources.ApplyResources(Rectangle, "Rectangle");
            Rectangle.Click += ElementWindowClick;
            // 
            // Circle
            // 
            Circle.Name = "Circle";
            resources.ApplyResources(Circle, "Circle");
            Circle.Click += ElementWindowClick;
            // 
            // Line
            // 
            Line.Name = "Line";
            resources.ApplyResources(Line, "Line");
            Line.Click += ElementWindowClick;
            // 
            // Buttons
            // 
            Buttons.DropDownItems.AddRange(new ToolStripItem[] { ButtonStrip, RoundedButton, Stepper, RadioButton });
            Buttons.Name = "Buttons";
            resources.ApplyResources(Buttons, "Buttons");
            Buttons.Tag = "Button";
            Buttons.Click += ElementWindowClick;
            // 
            // ButtonStrip
            // 
            ButtonStrip.Name = "ButtonStrip";
            resources.ApplyResources(ButtonStrip, "ButtonStrip");
            ButtonStrip.Tag = "Button";
            ButtonStrip.Click += ElementWindowClick;
            // 
            // RoundedButton
            // 
            RoundedButton.Name = "RoundedButton";
            resources.ApplyResources(RoundedButton, "RoundedButton");
            RoundedButton.Tag = "RoundedButton";
            RoundedButton.Click += ElementWindowClick;
            // 
            // Stepper
            // 
            Stepper.Name = "Stepper";
            resources.ApplyResources(Stepper, "Stepper");
            Stepper.Tag = "Stepper";
            Stepper.Click += ElementWindowClick;
            // 
            // RadioButton
            // 
            RadioButton.Name = "RadioButton";
            resources.ApplyResources(RadioButton, "RadioButton");
            RadioButton.Tag = "RadioButton";
            RadioButton.Click += ElementWindowClick;
            // 
            // Widgets
            // 
            Widgets.DropDownItems.AddRange(new ToolStripItem[] { Progressbar, sliderToolStripMenuItem, webViewToolStripMenuItem });
            Widgets.Name = "Widgets";
            resources.ApplyResources(Widgets, "Widgets");
            // 
            // Progressbar
            // 
            Progressbar.Name = "Progressbar";
            resources.ApplyResources(Progressbar, "Progressbar");
            Progressbar.Tag = "ProgressBar";
            Progressbar.Click += ElementWindowClick;
            // 
            // sliderToolStripMenuItem
            // 
            sliderToolStripMenuItem.Name = "sliderToolStripMenuItem";
            resources.ApplyResources(sliderToolStripMenuItem, "sliderToolStripMenuItem");
            sliderToolStripMenuItem.Tag = "Slider";
            sliderToolStripMenuItem.Click += ElementWindowClick;
            // 
            // webViewToolStripMenuItem
            // 
            webViewToolStripMenuItem.Name = "webViewToolStripMenuItem";
            resources.ApplyResources(webViewToolStripMenuItem, "webViewToolStripMenuItem");
            webViewToolStripMenuItem.Tag = "WebView";
            webViewToolStripMenuItem.Click += ElementWindowClick;
            // 
            // Graphics
            // 
            Graphics.DropDownItems.AddRange(new ToolStripItem[] { Textures, imageRectToolStripMenuItem });
            Graphics.Name = "Graphics";
            resources.ApplyResources(Graphics, "Graphics");
            Graphics.Click += ElementWindowClick;
            // 
            // Textures
            // 
            Textures.Name = "Textures";
            resources.ApplyResources(Textures, "Textures");
            Textures.Tag = "Texture";
            Textures.Click += ElementWindowClick;
            // 
            // imageRectToolStripMenuItem
            // 
            imageRectToolStripMenuItem.Name = "imageRectToolStripMenuItem";
            resources.ApplyResources(imageRectToolStripMenuItem, "imageRectToolStripMenuItem");
            imageRectToolStripMenuItem.Tag = "ImageRectangle";
            imageRectToolStripMenuItem.Click += ElementWindowClick;
            // 
            // Text
            // 
            Text.Name = "Text";
            resources.ApplyResources(Text, "Text");
            Text.Tag = "Text";
            Text.Click += ElementWindowClick;
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            resources.ApplyResources(newToolStripMenuItem, "newToolStripMenuItem");
            // 
            // Composer
            // 
            Composer.DropDownItems.AddRange(new ToolStripItem[] { animateToolStripMenuItem, spriteToolStripMenuItem, specialEffectsToolStripMenuItem });
            Composer.Name = "Composer";
            Composer.Padding = new Padding(5, 0, 20, 0);
            resources.ApplyResources(Composer, "Composer");
            // 
            // animateToolStripMenuItem
            // 
            animateToolStripMenuItem.Name = "animateToolStripMenuItem";
            resources.ApplyResources(animateToolStripMenuItem, "animateToolStripMenuItem");
            // 
            // spriteToolStripMenuItem
            // 
            spriteToolStripMenuItem.Name = "spriteToolStripMenuItem";
            resources.ApplyResources(spriteToolStripMenuItem, "spriteToolStripMenuItem");
            // 
            // specialEffectsToolStripMenuItem
            // 
            specialEffectsToolStripMenuItem.Name = "specialEffectsToolStripMenuItem";
            resources.ApplyResources(specialEffectsToolStripMenuItem, "specialEffectsToolStripMenuItem");
            // 
            // File
            // 
            File.DropDownItems.AddRange(new ToolStripItem[] { saveAllToolStripMenuItem, saveProjectToolStripMenuItem, saveSceneToolStripMenuItem, loadToolStripMenuItem });
            File.Name = "File";
            File.Padding = new Padding(5, 0, 20, 0);
            resources.ApplyResources(File, "File");
            // 
            // saveAllToolStripMenuItem
            // 
            saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            resources.ApplyResources(saveAllToolStripMenuItem, "saveAllToolStripMenuItem");
            saveAllToolStripMenuItem.Click += Save_Event;
            // 
            // saveProjectToolStripMenuItem
            // 
            saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            resources.ApplyResources(saveProjectToolStripMenuItem, "saveProjectToolStripMenuItem");
            saveProjectToolStripMenuItem.Click += Save_Event;
            // 
            // saveSceneToolStripMenuItem
            // 
            saveSceneToolStripMenuItem.Name = "saveSceneToolStripMenuItem";
            resources.ApplyResources(saveSceneToolStripMenuItem, "saveSceneToolStripMenuItem");
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            resources.ApplyResources(loadToolStripMenuItem, "loadToolStripMenuItem");
            // 
            // DB
            // 
            DB.Name = "DB";
            DB.Padding = new Padding(5, 0, 20, 0);
            resources.ApplyResources(DB, "DB");
            // 
            // Scripting
            // 
            Scripting.Name = "Scripting";
            Scripting.Padding = new Padding(5, 0, 20, 0);
            resources.ApplyResources(Scripting, "Scripting");
            // 
            // TestButton
            // 
            TestButton.Name = "TestButton";
            TestButton.Padding = new Padding(5, 0, 20, 0);
            resources.ApplyResources(TestButton, "TestButton");
            TestButton.Click += TestButton_Click;
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Alignment = ToolStripItemAlignment.Right;
            resources.ApplyResources(toolStripTextBox1, "toolStripTextBox1");
            toolStripTextBox1.BackColor = SystemColors.InactiveCaption;
            toolStripTextBox1.Name = "toolStripTextBox1";
            // 
            // ScreenPanel
            // 
            resources.ApplyResources(ScreenPanel, "ScreenPanel");
            ScreenPanel.BackColor = Color.AliceBlue;
            ScreenPanel.Name = "ScreenPanel";
            ScreenPanel.MouseClick += ScreenPanel_Reset;
            // 
            // CodeEdit
            // 
            CodeEdit.BackColor = SystemColors.HighlightText;
            resources.ApplyResources(CodeEdit, "CodeEdit");
            CodeEdit.Name = "CodeEdit";
            CodeEdit.ShowSelectionMargin = true;
            // 
            // Designer
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.SteelBlue;
            Controls.Add(CodeEdit);
            Controls.Add(mainmenustrip);
            Controls.Add(ScreenPanel);
            DoubleBuffered = true;
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MainMenuStrip = mainmenustrip;
            Name = "Designer";
            Tag = "main";
            Load += Designer_Load;
            MouseDown += me_MouseDown;
            MouseMove += me_MouseMove;
            MouseUp += me_MouseUp;
            mainmenustrip.ResumeLayout(false);
            mainmenustrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mainmenustrip;
        private ToolStripMenuItem File;
        private ToolStripMenuItem DB;
        private ToolStripMenuItem NewElement;
        private ColorDialog colorDialog1;
        private SolarDialog_1 sd;
        private ToolStripComboBox SceneAusw;
        private ToolStripMenuItem Composer;
        private ToolStripMenuItem Scripting;
        private ToolStripMenuItem Buttons;
        private ToolStripMenuItem Geometrics;
        private ToolStripMenuItem Graphics;
        private ToolStripMenuItem Widgets;
        private ToolStripMenuItem Rectangle;
        private ToolStripMenuItem Circle;
        private ToolStripMenuItem Textures;
        private ToolStripMenuItem ButtonStrip;
        private ToolStripMenuItem RoundedButton;
        private ToolStripMenuItem Line;
        private ToolStripMenuItem Stepper;
        private ToolStripMenuItem Progressbar;
        private ToolStripMenuItem Text;
        private ToolStripMenuItem saveAllToolStripMenuItem;
        private ToolStripMenuItem saveProjectToolStripMenuItem;
        private ToolStripMenuItem saveSceneToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private Panel ScreenPanel;
        private ToolStripMenuItem TestButton;
        private ToolStripMenuItem sliderToolStripMenuItem;
        private ToolStripMenuItem imageRectToolStripMenuItem;
        private ToolStripMenuItem webViewToolStripMenuItem;
        private ToolStripMenuItem animateToolStripMenuItem;
        private ToolStripMenuItem spriteToolStripMenuItem;
        private ToolStripMenuItem specialEffectsToolStripMenuItem;
        private Input CodeEdit;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem RadioButton;
    }
}