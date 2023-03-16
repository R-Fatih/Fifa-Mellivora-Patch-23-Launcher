using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using File = System.IO.File;

namespace Fifa_Mellivora_Patch_23_Launcher.Tables
{
    internal class Manager
    {
        string[] file = File.ReadAllLines("manager.txt");
        string[][] splited;
        public void ReadData()
        {
            splited = file.Select(x => x.Split('\t')).ToArray();

        }
        public string GetCoachId(string teamid)
        {
            int index = Array.FindIndex(splited[0], x => x.Contains("teamid"));
            int index2 = Array.FindIndex(splited[0], x => x.Contains("managerid"));

            int coachid = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i][index] == teamid)
                    coachid =Convert.ToInt32( splited[i][index2]);
            }
            return "https://raw.githubusercontent.com/R-Fatih/MP23/main/Coaches/heads_staff_" +1+coachid+ ".png" ;
             
        }
        public string GetCoachNation(string teamid)
        {

            int index = Array.FindIndex(splited[0], x => x.Contains("teamid"));
            int index2 = Array.FindIndex(splited[0], x => x.Contains("nationality"));

            int nationid = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i][index] == teamid)
                    nationid = Convert.ToInt32(splited[i][index2]);
            }
            return "https://raw.githubusercontent.com/R-Fatih/MP23/main/Flags/f_"  + nationid + ".png";
        }
        public string GetCoacName(string teamid)
        {

            int index = Array.FindIndex(splited[0], x => x.Contains("teamid"));
            int index2 = Array.FindIndex(splited[0], x => x.Contains("firstname"));
            int index3 = Array.FindIndex(splited[0], x => x.Contains("surname"));


            string coachname = "";
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i][index] == teamid)
                    coachname = splited[i][index2] +" "+ splited[i][index3];
            }
            return coachname ;
        }

    }
}
