using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VBLua.Core;

namespace ScriptingTool
{
    public partial class CoderControl : Form
    {
        public CoderControl()
        {
            InitializeComponent();
        }
        public void Refr(string x, string y)
        {// this.CodeEdit.Text="";
        }

        private void CoderControl_Load(object sender, EventArgs e)
        {

        }
        private void CodeEdit_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 100;
        }

        private void CodeEdit_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 10;
        }
    }
}
