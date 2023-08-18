namespace VBLua.IDE
{
    partial class VBTerminal
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            outputLine = new RichTextBox();
            commandLine = new TextBox();
            SuspendLayout();
            // 
            // outputLine
            // 
            outputLine.BackColor = SystemColors.Desktop;
            outputLine.Dock = DockStyle.Fill;
            outputLine.Font = new Font("Microsoft Sans Serif", 18.23F, FontStyle.Regular, GraphicsUnit.Point);
            outputLine.ForeColor = Color.Lime;
            outputLine.Location = new Point(1, 1);
            outputLine.Name = "outputLine";
            outputLine.ReadOnly = true;
            outputLine.ScrollBars = RichTextBoxScrollBars.Vertical;
            outputLine.Size = new Size(718, 298);
            outputLine.TabIndex = 0;
            outputLine.TabStop = false;
            outputLine.Text = "";
            // 
            // commandLine
            // 
            commandLine.Location = new Point(0, 0);
            commandLine.Name = "commandLine";
            commandLine.Size = new Size(100, 42);
            commandLine.TabIndex = 1;
            // 
            // VBTerminal
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            BackColor = SystemColors.Desktop;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(outputLine);
            Controls.Add(commandLine);
            Cursor = Cursors.Hand;
            Font = new Font("Microsoft Sans Serif", 18.23F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.Lime;
            MaximumSize = new Size(1280, 960);
            Name = "VBTerminal";
            Padding = new Padding(1);
            RightToLeft = RightToLeft.No;
            Size = new Size(720, 300);
            Load += Terminal_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox commandLine; 
        private RichTextBox outputLine;
    }
}
