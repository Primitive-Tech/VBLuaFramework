
namespace VBLua.Terminal
{
    partial class Terminal
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
            this.outputLine = new System.Windows.Forms.RichTextBox();
            this.commandLine = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // outputLine
            // 
            this.outputLine.BackColor = System.Drawing.SystemColors.Desktop;
            this.outputLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.outputLine.ForeColor = System.Drawing.Color.Lime;
            this.outputLine.Location = new System.Drawing.Point(1, 1);
            this.outputLine.Name = "outputLine";
            this.outputLine.ReadOnly = true;
            this.outputLine.DetectUrls = true;
            this.outputLine.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.outputLine.Size = new System.Drawing.Size(720, 300 - 1);
            this.outputLine.TabIndex = 0;
            this.outputLine.TabStop = false;
            this.outputLine.Text = "";
            this.outputLine.TextChanged += new System.EventHandler(this.outputLine_TextChanged);
            // 
            // commandLine
            // 
            this.commandLine.BackColor = System.Drawing.SystemColors.Desktop;
            this.commandLine.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.commandLine.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.commandLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.commandLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.commandLine.ForeColor = System.Drawing.Color.Lime;
            this.commandLine.Location = new System.Drawing.Point(1, 300);
            this.commandLine.MaxLength = 100;
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(720, 35);
            this.commandLine.TabIndex = 3;
            this.commandLine.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ControlInputLine);
            // 
            // Terminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.outputLine);
            this.Controls.Add(this.commandLine);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Lime;
            this.MaximumSize = new System.Drawing.Size(1280, 960);
            this.Name = "Terminal";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(720, 300);
            this.Load += new System.EventHandler(this.Terminal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private TextBox commandLine;
        private RichTextBox outputLine;
    }
}
