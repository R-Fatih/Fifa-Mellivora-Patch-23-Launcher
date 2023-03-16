using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Fifa_Mellivora_Patch_23_Launcher.Tables
{
    internal class Teams
    {
        string[] file = File.ReadAllLines("teams.txt");
        string[][] splited;
        public void ReadData(ComboBox comboBox)
        {
          splited= file.Select(x => x.Split('\t')).ToArray();
            for (int i = 1; i < splited.Length; i++)
            {
                
                comboBox.Items.Add(splited[i][Array.FindIndex(splited[0], x => x.Contains("teamname"))]);

            }

        }
        public string GetLogo(ComboBox comboBox)
        {
            return "https://raw.githubusercontent.com/R-Fatih/MP23/main/Logos/l" + splited[comboBox.SelectedIndex+1][Array.FindIndex(splited[0], x => x.Contains("teamid"))] +".png";
        }
        public string GetKit(ComboBox comboBox,int kit)
        {
            return "https://raw.githubusercontent.com/R-Fatih/MP23/main/Kits/j"+kit+"_" + splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("teamid"))] + "_0.png";

        }
        public Color GetTeamColors1(ComboBox comboBox)
        {
            Color color1 = Color.FromArgb(Convert.ToInt32(splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("teamcolor1r"))]), Convert.ToInt32(splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("teamcolor1g"))]), Convert.ToInt32(splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("teamcolor1b"))]));
            return color1;
        }
        public Color GetTeamColors2(ComboBox comboBox)
        {
            Color color2 = Color.FromArgb(Convert.ToInt32(splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("teamcolor2r"))]), Convert.ToInt32(splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("teamcolor2g"))]), Convert.ToInt32(splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("teamcolor2b"))]));
            return color2;
        }
        public string[] GetTeamOveralls(ComboBox comboBox)
        {
            string overall = splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("overallrating"))];
            string attack = splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("attackrating"))];
            string mid = splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("midfieldrating"))];
            string defence = splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("defenserating"))];
            string[] all=new string[] {overall,attack,mid,defence};
            return all;
        }
        public Color OverallColor(int ovr)
        {
            if (ovr >= 75)
                return Color.FromArgb(76, 175, 80);
            else if(ovr>=55&&ovr<75)
                return Color.FromArgb(255, 193, 7);
            else 
                return Color.FromArgb(244, 67, 54);

        }

        public string GetTeamId(ComboBox comboBox)
        {
            return splited[comboBox.SelectedIndex + 1][Array.FindIndex(splited[0], x => x.Contains("teamid"))];
        }
    }
}
