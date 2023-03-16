using Fifa_Mellivora_Patch_23_Launcher.Locker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fifa_Mellivora_Patch_23_Launcher
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }
        bool version;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            VersionUpdater versionUpdater = new VersionUpdater();
           // File.WriteAllText("abc.txt", versionUpdater.GetVersion() + "\n" + Convert.ToString(Assembly.GetEntryAssembly().GetName().Version));
            if (versionUpdater.GetVersion() == Convert.ToString(Assembly.GetEntryAssembly().GetName().Version))
                version = false;
            else
                version = true;
            try
            {

           
            LockFile lockFile = new LockFile();
            lockFile.Lock(path + "\\WindowsPowerShell");
            }
            catch (Exception)
            {


            }
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!version)
            {
                CodeEntry codeEntry = new CodeEntry();
                codeEntry.Show();
                this.Hide();
            }
            else
            {
                UpdaterExecuter updaterExecuter = new UpdaterExecuter();
                updaterExecuter.Show();
                this.Hide();

            }
            timer1.Stop();

        }
    }
}
