using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fifa_Mellivora_Patch_23_Launcher
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = new
            {
                GamePath = folderBrowserDialog1.SelectedPath,
                FirstOpen=false
            };
            var json=JsonConvert.SerializeObject(data);
            File.WriteAllText("Settings.json", json);
            this.Hide();
        }
    }
}
