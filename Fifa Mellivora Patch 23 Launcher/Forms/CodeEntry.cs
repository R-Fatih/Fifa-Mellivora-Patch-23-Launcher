using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Fifa_Mellivora_Patch_23_Launcher
{
    public partial class CodeEntry : Form
    {
        public CodeEntry()
        {
            InitializeComponent();
        }
        string mac = Mac();
        string macadress;
        string membertype;
        SqlConnector sqlConnector;
        private void CodeEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); 
        }
        static string Mac()
        {
            ManagementClass manager = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject obj in manager.GetInstances())
            {
                if ((bool)obj["IPEnabled"])
                {
                    return obj["MacAddress"].ToString();
                }
            }

            return String.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           sqlConnector = new SqlConnector();
           sqlConnector.Connection().Open();
            using (SqlCommand command1 = new SqlCommand("Select * From Users where Code=@p1", sqlConnector.Connection()))
            {
                command1.Parameters.AddWithValue("@p1", textBox1.Text);
                SqlDataReader sqlDataReader = command1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    macadress = sqlDataReader["MacAdress"].ToString();
                    membertype = sqlDataReader["MemberType"].ToString();
                }
            }
            using (SqlCommand command2 = new SqlCommand("Select * from Users Where Code=@p1 and MacAdress like '%" + mac + "%'",sqlConnector.Connection()))
            {
                command2.Parameters.AddWithValue("@p1", textBox1.Text);
                command2.Parameters.AddWithValue("@p2", mac);
                SqlDataReader sqlDataReader1 = command2.ExecuteReader();
                if (macadress == "")
                {
                    using (SqlCommand command3 = new SqlCommand("update Users set MacAdress=@p2 where Code=@p1", sqlConnector.Connection()))
                    {
                        command3.Parameters.AddWithValue("@p1", textBox1.Text);
                        command3.Parameters.AddWithValue("@p2", mac);
                        command3.ExecuteNonQuery();

                    }
                    UserPanel userPanel = new UserPanel();
                    userPanel.membertype = membertype;
                    userPanel.Show();
                    File.WriteAllText("code.txt", textBox1.Text);
                    this.Hide();
                }
                else if (sqlDataReader1.Read())
                {

                    UserPanel userPanel = new UserPanel();
                    userPanel.membertype = membertype;
                    userPanel.Show();
                    File.WriteAllText("code.txt", textBox1.Text);
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Böyle bir kod bulunamadı ya da bu kod daha önce kullanılmış!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            sqlConnector.Connection().Close();
        }

        private void CodeEntry_Load(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = File.ReadAllText("code.txt");
            }
            catch (Exception)
            {


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = !textBox1.UseSystemPasswordChar;
        }
    }
}
