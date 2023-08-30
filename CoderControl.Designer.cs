namespace ScriptingTool
{
    partial class CoderControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CodeEdit = new VBLua.Core.Input();
            SuspendLayout();
            // 
            // CodeEdit
            // 
            CodeEdit.BackColor = Color.Snow;
            CodeEdit.Dock = DockStyle.Fill;
            CodeEdit.Font = new Font("Share Tech Mono", 12F, FontStyle.Regular, GraphicsUnit.Point);
            CodeEdit.Location = new Point(0, 0);
            CodeEdit.Name = "CodeEdit";
            CodeEdit.ShowSelectionMargin = true;
            CodeEdit.Size = new Size(800, 450);
            CodeEdit.TabIndex = 0;
            CodeEdit.Text = "local function ccc()";
            CodeEdit.MouseEnter += CodeEdit_MouseEnter;
            CodeEdit.MouseLeave += CodeEdit_MouseLeave;
            // 
            // CoderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(CodeEdit);
            Name = "CoderControl";
            Text = "Coder";
            Load += CoderControl_Load;
            ResumeLayout(false);
        }

        #endregion

        public VBLua.Core.Input CodeEdit;
    }
}