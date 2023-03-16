using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fifa_Mellivora_Patch_23_Launcher
{
    public partial class UpdaterExecuter : Form
    {
        public UpdaterExecuter()
        {
            InitializeComponent();
        }

        private void UpdaterExecuter_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UpdaterExecuter_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           Process p= Process.Start(Application.StartupPath+"\\Fifa Mellivora Patch 23 Updater.exe");
            Application.Exit();
            timer1.Stop();
        }
    }
}
