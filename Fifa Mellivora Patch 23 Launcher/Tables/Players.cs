using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifa_Mellivora_Patch_23_Launcher.Tables
{
    internal class Players
    {
        string[] file = File.ReadAllLines("players.txt");
        string[][] splited;
        string[] file2 = File.ReadAllLines("teamplayerlinks.txt");
        string[][] splitedteamplayerlinks;
        string[] file3 = File.ReadAllLines("playernames.txt");
        string[][] splitedplayernames;
        public void ReadData()
        {
            splited = file.Select(x => x.Split('\t')).ToArray();
            splitedteamplayerlinks = file2.Select(x => x.Split('\t')).ToArray();
            splitedplayernames = file3.Select(x => x.Split('\t')).ToArray();
        }

        public List<int> GetAllPlayers(string teamid)
        {
            int index = Array.FindIndex(splitedteamplayerlinks[0], x => x.Contains("teamid"));
            int index2 = Array.FindIndex(splitedteamplayerlinks[0], x => x.Contains("playerid"));
            List<int> result = new List<int>();
            for (int i = 0; i < splitedteamplayerlinks.Length; i++)
            {
                if (splitedteamplayerlinks[i][index] == teamid)
                    result.Add(Convert.ToInt32(splitedteamplayerlinks[i][index2]));

            }
            return result;

        }

        public string GetPlayerNameID(string playerid)
        {
            int index = Array.FindIndex(splited[0], x => x.Contains("firstnameid"));
            int index2 = Array.FindIndex(splited[0], x => x.Contains("lastnameid"));
            int index3 = Array.FindIndex(splited[0], x => x.Contains("playerid"));
            string playernameid = "";
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i][index3] == playerid)
                    playernameid = splited[i][index]+" " + splited[i][index2];
            }
            return playernameid;

        }

        public string GetPlayerName(string playernameid)
        {
            int index = Array.FindIndex(splitedplayernames[0], x => x.Contains("nameid"));
            int index2 = Array.FindIndex(splitedplayernames[0], x => x.Contains("name"));
            File.WriteAllText("index.txt", index2.ToString());
            string playername = "";
            string playersurname = "";
            for (int i = 0; i < splitedplayernames.Length; i++)
            {
                if (splitedplayernames[i][index] == playernameid.Split(' ')[0])
                    playername = splitedplayernames[i][2];
                if (splitedplayernames[i][index] == playernameid.Split(' ')[1])
                    playersurname = splitedplayernames[i][2];
            }
            return playername+" "+playersurname;
        }
        public int GetPlayerAge(string playerid)
        {
            int index = Array.FindIndex(splited[0], x => x.Contains("playerid"));
            int index2 = Array.FindIndex(splited[0], x => x.Contains("birthdate"));

            string playernameid = "";
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i][index] == playerid)
                    playernameid = splited[i][index2];
            }
            DateTime dt = DateTime.Now;
            return dt.Year-Convert.ToInt32( ConvertToDate(Convert.ToInt32(playernameid)).ToString("yyy"));

        }
        public int GetPlayerOverall(string playerid)
        {
            int index = Array.FindIndex(splited[0], x => x.Contains("playerid"));
            int index2 = Array.FindIndex(splited[0], x => x.Contains("overallrating"));

            int playernameid = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i][index] == playerid)
                    playernameid =Convert.ToInt32(splited[i][index2]);
            }

            return playernameid;

        }
        public int GetPlayerPotential(string playerid)
        {
            int index = Array.FindIndex(splited[0], x => x.Contains("playerid"));
            int index2 = Array.FindIndex(splited[0], x => x.Contains("potential"));

            int playernameid = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i][index] == playerid)
                    playernameid = Convert.ToInt32(splited[i][index2]);
            }

            return playernameid;

        }
        public int GetPlayerContract(string playerid)
        {
            int index = Array.FindIndex(splited[0], x => x.Contains("playerid"));
            int index2 = Array.FindIndex(splited[0], x => x.Contains("contractvaliduntil"));

            int playercontract = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i][index] == playerid)
                    playercontract = Convert.ToInt32(splited[i][index2]);
            }

            return playercontract;

        }
        public int ConvertFromDate(DateTime date)
        {
            DateTime time = new DateTime(0x62e, 10, 14, 0, 0, 0);
            return (date - time).Days;
        }
        public static DateTime ConvertToDate(int gregorian)
        {
            DateTime time = new DateTime(0x62e, 10, 14, 12, 0, 0);
            return ((gregorian >= 0) ? time.AddDays((double)gregorian) : time);
        }




      
    }
}
