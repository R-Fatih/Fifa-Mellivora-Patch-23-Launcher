using Fifa_Mellivora_Patch_23_Launcher.Tables;
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
using AltoHttp;
using static Fifa_Mellivora_Patch_23_Launcher.Json.ModManagerJson;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using ProgressChangedEventArgs = AltoHttp.ProgressChangedEventArgs;
using CSharpImageLibrary;
using System.Net;
using Fifa_Mellivora_Patch_23_Launcher.Locker;
using System.IO.Compression;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using Octokit;
using ProductHeaderValue = Octokit.ProductHeaderValue;
using Application = System.Windows.Forms.Application;
using System.Data.SqlClient;
using Fifa_Mellivora_Patch_23_Launcher.Classes;
using Ionic.Zip;
using Fifa_Mellivora_Patch_23_Launcher.Rpc;

namespace Fifa_Mellivora_Patch_23_Launcher
{
    public partial class UserPanel : Form
    {
        public UserPanel()
        {
            InitializeComponent();
        }
        public string modmanagerpath = "FIFA Mod Manager\\Mods\\FIFA23\\";
        public string modmanagerjsonpath = "FIFA Mod Manager";
        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        bool modmanager = false;
        string downloadlink;
        public string membertype;
        Teams teams = new Teams();
        List<Links> linkslist = new List<Links>();
        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void UserPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



        private void UserPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveJsonFile();

        }

        private void SaveJsonFile()
        {

        }
        HttpDownloader httpDownloader;
        private DiscordRpc.EventHandlers handlers;
        private DiscordRpc.RichPresence presence;
        void RPC()
        {
            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("1066284801310605353", ref this.handlers, true, null);
            this.presence.largeImageKey = "logo";
            this.presence.largeImageText = "FIFA MP23";
            this.presence.startTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            DiscordRpc.UpdatePresence(ref this.presence);
        }
        private void UserPanel_Load(object sender, EventArgs e)
        {
            //dataGridView1.RowTemplate.MinimumHeight = 128;//25 is height.


            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WindowsPowerShell\\FIFA Mod Manager.exe"))
            {
                if (MessageBox.Show("Mod Manager bulunamadı." + "\n" + "İndirmek istiyor musunuz?", "Mod Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    try
                    {
                        Directory.Delete(path + "\\WindowsPowerShell");
                    }
                    catch (Exception)
                    {


                    }

                    tabControl1.SelectedIndex = 2;
                    button1.Enabled = false;
                    Directory.CreateDirectory(path + "\\WindowsPowerShell");
                    httpDownloader = new HttpDownloader("https://raw.githubusercontent.com/R-Fatih/MP23/main/FIFA%20Mod%20Manager.zip", path + "\\WindowsPowerShell\\FMM.zip");
                    httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;
                    httpDownloader.ProgressChanged += HttpDownloader_ProgressChanged;
                    httpDownloader.Start();
                    modmanager = true;
                }

                else

                {
                    Application.Exit();
                }
            }

            //DirectoryInfo taskDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WindowsPowerShell\\Mods\\FIFA23");
            //FileInfo[] taskFiles = taskDirectory.GetFiles("MP-AnaPaket*.fifamod");
            //string version = "";

            //if (taskFiles.Count()== 1)
            //{
            //    version = taskFiles[0].ToString().Substring(12).Replace(".fifamod", "");
            //    label11.Text = "Yama sürümünüz: " + version;
            //}
            //else
            //{
            //    version = taskFiles[taskFiles.Length - 1].ToString().Substring(12).Replace(".fifamod", "");
            //    label11.Text = "Yama sürümünüz: " +version;
            //}


            SqlConnector sqlConnector = new SqlConnector();
            sqlConnector.Connection().Open();
            SqlCommand sqlCommand = new SqlCommand("Select * from Links where Type=@p1 order by Date asc", sqlConnector.Connection());
            sqlCommand.Parameters.AddWithValue("@p1", membertype);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Links links = new Links(dataReader["Name"].ToString(), dataReader["Size"].ToString(), dataReader["Type"].ToString(), dataReader["Extension"].ToString(), Convert.ToBoolean(dataReader["Github"].ToString()), dataReader["Link"].ToString(), Convert.ToBoolean(dataReader["Mod"].ToString()));
                linkslist.Add(links);
            }
            for (int i = 0; i < linkslist.Count; i++)
            {
                listBox1.Items.Add(linkslist[i].Name);

            }
            pictureBox2.LoadAsync("https://raw.githubusercontent.com/R-Fatih/MP23/main/content.png");
            teams.ReadData(comboBox1);

            RPC();
         
    }
        public string Ftp(string game)
        {
            WebClient webClient = new WebClient();
            ((NameValueCollection)webClient.Headers).Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");
            byte[] newFileData = webClient.DownloadData(new Uri(game));
            return System.Text.Encoding.UTF8.GetString(newFileData);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveJsonFile();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox4.LoadAsync(teams.GetLogo(comboBox1));
            pictureBox3.LoadAsync(teams.GetKit(comboBox1, 0));

            pictureBox5.LoadAsync(teams.GetKit(comboBox1, 1));
            pictureBox6.LoadAsync(teams.GetKit(comboBox1, 3));
            pictureBox7.LoadAsync(teams.GetKit(comboBox1, 2));

            button2.BackColor = teams.GetTeamColors1(comboBox1);
            button3.BackColor = teams.GetTeamColors2(comboBox1);

            button4.Text = teams.GetTeamOveralls(comboBox1)[0];
            button5.Text = teams.GetTeamOveralls(comboBox1)[1];
            button6.Text = teams.GetTeamOveralls(comboBox1)[2];
            button7.Text = teams.GetTeamOveralls(comboBox1)[3];

            button4.BackColor = teams.OverallColor(Convert.ToInt32(button4.Text));
            button5.BackColor = teams.OverallColor(Convert.ToInt32(button5.Text));
            button6.BackColor = teams.OverallColor(Convert.ToInt32(button6.Text));
            button7.BackColor = teams.OverallColor(Convert.ToInt32(button7.Text));

            Manager manager = new Manager();
            manager.ReadData();

            pictureBox8.LoadAsync(manager.GetCoachId(teams.GetTeamId(comboBox1)));
            pictureBox9.LoadAsync(manager.GetCoachNation(teams.GetTeamId(comboBox1)));
            label8.Text = manager.GetCoacName(teams.GetTeamId(comboBox1));



            Players player = new Players();
            player.ReadData();

            dataGridView1.Rows.Clear();
            for (int i = 0; i < player.GetAllPlayers(teams.GetTeamId(comboBox1)).Count; i++)
            {


                dataGridView1.Rows.Add(player.GetPlayerName(player.GetPlayerNameID(player.GetAllPlayers(teams.GetTeamId(comboBox1))[i].ToString()))
                    , player.GetPlayerAge(player.GetAllPlayers(teams.GetTeamId(comboBox1))[i].ToString())
                    , player.GetPlayerOverall(player.GetAllPlayers(teams.GetTeamId(comboBox1))[i].ToString())
                    , player.GetPlayerPotential(player.GetAllPlayers(teams.GetTeamId(comboBox1))[i].ToString())
                    , player.GetPlayerContract(player.GetAllPlayers(teams.GetTeamId(comboBox1))[i].ToString()));

                // listBox1.Items.Add(player.GetPlayerName("25299 25299"));
            }
        }
        public static Image GetImageFromUrl(string url)
        {
            Image a = Fifa_Mellivora_Patch_23_Launcher.Properties.Resources.notfound;





            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse httpWebReponse;


            httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();

            Stream stream = httpWebReponse.GetResponseStream();


            a = Image.FromStream(stream);
            httpWebReponse.Close();
            stream.Flush();
            httpWebRequest.Abort();





            return a;

        }
        public string GetUrl()
        {
            var githubToken = "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";
            var url = "https://raw.githubusercontent.com/R-Fatih/MPPrivate/main/v2.zip?";
            var path = "";

            using (var client = new System.Net.Http.HttpClient())
            {
                //var credentials = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}:", githubToken);
                //credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(credentials));
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                //var contents = client.GetByteArrayAsync(url).Result;
                //System.IO.File.WriteAllBytes(path, contents);

            }
            return path;
        }
        async void A(string extension)
        {
            var gitHubClient = new GitHubClient(new ProductHeaderValue("app-name"));
            var credentials = new Credentials(token: "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");
            gitHubClient.Credentials = credentials;

            var repositoryContents =
                await gitHubClient.Repository.Content.GetAllContents("R-Fatih", "MPPrivate", extension);
            var firstOrDefault = repositoryContents.FirstOrDefault(c => c.Path == @extension);
            var downloadUrl = firstOrDefault?.DownloadUrl;
            downloadlink = downloadUrl;
            //File.WriteAllText("a.txt", downloadUrl);
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            var pathdocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            try
            {
                if (linkslist[listBox1.SelectedIndex].Mod == true)
                    httpDownloader = new HttpDownloader(downloadlink, path + "\\WindowsPowerShell\\Mods\\FIFA23\\" + linkslist[listBox1.SelectedIndex].Extension);
                else
                    httpDownloader = new HttpDownloader("https://raw.githubusercontent.com/R-Fatih/MP23/main/LastSquad.zip", pathdocs + "\\FIFA 23\\settings\\LastSquad.zip");

                httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;
                httpDownloader.ProgressChanged += HttpDownloader_ProgressChanged;
                httpDownloader.Start();
                button1.Enabled = false;
                button8.Enabled = true;

            }
            catch (Exception ex)
            {

                // File.WriteAllText("Eror.txt", ex.Message);
            }
        }
        private void HttpDownloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = (int)e.Progress;
            label10.Text = $"%{e.Progress.ToString("0.00")} ";
            label9.Text = "Hız: " + string.Format("{0} MB/s", (e.SpeedInBytes / 1024d / 1024d).ToString("0.00"));
            label2.Text = "İndirilen: " + string.Format("{0} MB/s", (httpDownloader.TotalBytesReceived / 1024d / 1024d).ToString("0.00"));
        }

        private void HttpDownloader_DownloadCompleted(object sender, EventArgs e)
        {
            var pathdocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Invoke((MethodInvoker)delegate
            {
                label10.Text = "100%";

                button8.Enabled = false;
                button9.Enabled = false;
                //  MessageBox.Show("İndirme işlemi başarıyla tamamlandı.");
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("İndirme işlemi tamamladı. Evete basarak zip çıkarma işlemini yapabilirsiniz.", "Zip Çıkart", buttons);
                if (result == DialogResult.Yes)
                {
                    if (modmanager == false)
                    {
                        if (linkslist[listBox1.SelectedIndex].Mod == true)
                        {
                            Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read((path + "\\WindowsPowerShell\\Mods\\FIFA23\\" + linkslist[listBox1.SelectedIndex].Extension));
                            zip.ExtractAll((path + "\\WindowsPowerShell\\Mods\\FIFA23"), ExtractExistingFileAction.OverwriteSilently);
                             MessageBox.Show("İşlem başarıyla tamamlandı.");

                        }
                        else
                        {
                            Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read((pathdocs + "\\FIFA 23\\settings\\LastSquad.zip"));
                            zip.ExtractAll((pathdocs + "\\FIFA 23\\settings"), ExtractExistingFileAction.OverwriteSilently);
                            MessageBox.Show("İşlem başarıyla tamamlandı.");

                        }
                    }
                    else
                    {
                        Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read((path + "\\WindowsPowerShell\\FMM.zip"));
                        zip.ExtractAll((path + "\\WindowsPowerShell"), ExtractExistingFileAction.OverwriteSilently);
                        MessageBox.Show("İşlem başarıyla tamamlandı.");

                    }

                    LockFile lockFile = new LockFile();
                    try
                    {

                        lockFile.Lock(path + "\\WindowsPowerShell");
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    // Do something  
                }
            });
        }
        //async void ZipExFMM()
        //{
        //    await Task.Run(async () =>
        //    {
        //        ZipFile.ExtractToDirectory((path + "\\WindowsPowerShell\\FMM.zip"), (path + "\\WindowsPowerShell"));
        //    });
        //    File.Delete(path + "\\WindowsPowerShell\\FMM.zip");
        //    Application.Restart();
        //    LockFile lockFile = new LockFile();
        //    lockFile.Lock(path + "\\WindowsPowerShell");
        //}
        //async void ZipExPatch()
        //{
        //    await Task.Run(async () =>
        //    {
        //        ZipFile.ExtractToDirectory((path + "\\WindowsPowerShell\\Mods\\FIFA23\\LastVersion.zip"), (path + "\\WindowsPowerShell\\Mods\\FIFA23"));
        //    });
        //    File.Delete(path + "\\WindowsPowerShell\\Mods\\FIFA23\\LastVersion.zip");
        //    LockFile lockFile = new LockFile();
        //    lockFile.Lock(path + "\\WindowsPowerShell");
        //    //Application.Restart();
        //}

        //async void ZipExLastSquad()
        //{
        //    var pathdocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //    await Task.Run(async () =>
        //    {
        //        ZipFile.ExtractToDirectory((pathdocs + "\\FIFA 23\\settings\\LastSquad.zip"), (pathdocs + "\\FIFA 23\\settings"));
        //    });
        //    File.Delete(pathdocs + "\\FIFA 23\\settings\\LastSquad.zip");
        //    Application.Restart();
        //}
        private void button8_Click(object sender, EventArgs e)
        {
            if (httpDownloader != null)
                httpDownloader.Pause();
            button8.Enabled = false;
            button9.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (httpDownloader != null)
                httpDownloader.Resume();
            button8.Enabled = true;
            button9.Enabled = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            LockFile lockFile = new LockFile();
            try
            {
                lockFile.Unlock(path + "\\WindowsPowerShell");
            }
            catch (Exception)
            {
            }
            lockFile.RunProcessAsync(path + "\\WindowsPowerShell\\FIFA Mod Manager.exe");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/FrkERezkjR");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/@MellivoraPatch");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/MellivoraPatch");

        }

        private void button14_Click(object sender, EventArgs e)
        {
            var pathdocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            tabControl1.SelectedIndex = 2;
            button1.Enabled = false;

            httpDownloader = new HttpDownloader("https://raw.githubusercontent.com/R-Fatih/MP23/main/LastSquad.zip", pathdocs + "\\FIFA 23\\settings\\LastSquad.zip");
            httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;
            httpDownloader.ProgressChanged += HttpDownloader_ProgressChanged;
            httpDownloader.Start();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            modmanager = false;
            if (listBox1.SelectedIndex != -1)
            {
                if (File.Exists(path + "\\WindowsPowerShell\\Mods\\FIFA23\\" + linkslist[listBox1.SelectedIndex].Extension))
                    button1.Enabled = true;
                else
                    button1.Enabled = true;
                if (linkslist[listBox1.SelectedIndex].Github == true)
                    A(linkslist[listBox1.SelectedIndex].Extension);
                else
                    downloadlink = linkslist[listBox1.SelectedIndex].Link;
                label1.Text = "Toplam boyut: " + linkslist[listBox1.SelectedIndex].Size;
            }

        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            LockFile lockFile = new LockFile();
            try
            {
                lockFile.Unlock(path + "\\WindowsPowerShell");
            }
            catch (Exception)
            {
            }
            Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(path + "\\WindowsPowerShell\\FMM.zip");
            //Directory.CreateDirectory(path + "\\WindowsPowerShell");
            zip.ExtractAll(path + "\\WindowsPowerShell", ExtractExistingFileAction.OverwriteSilently);
            try
            {
                lockFile.Lock(path + "\\WindowsPowerShell");
            }
            catch (Exception)
            {
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var pathdocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (modmanager == false)
            {
                if (linkslist[listBox1.SelectedIndex].Mod == true)
                {
                    Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read((path + "\\WindowsPowerShell\\Mods\\FIFA23\\" + linkslist[listBox1.SelectedIndex].Extension));
                    zip.ExtractAll((path + "\\WindowsPowerShell\\Mods\\FIFA23"), ExtractExistingFileAction.OverwriteSilently);
                    MessageBox.Show("İşlem başarıyla tamamlandı.");

                }
                else
                {
                    Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read((pathdocs + "\\FIFA 23\\settings\\LastSquad.zip"));
                    zip.ExtractAll((pathdocs + "\\FIFA 23\\settings"), ExtractExistingFileAction.OverwriteSilently);
                    MessageBox.Show("İşlem başarıyla tamamlandı.");

                }
            }
            else
            {
                Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read((path + "\\WindowsPowerShell\\FMM.zip"));
                zip.ExtractAll((path + "\\WindowsPowerShell"), ExtractExistingFileAction.OverwriteSilently);
                MessageBox.Show("İşlem başarıyla tamamlandı.");

            }
        }
    }
}
