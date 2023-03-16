using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifa_Mellivora_Patch_23_Launcher
{
    internal class User
    {

        private int _id;
        private string _code;
        private string _macAdress;
        private string _mail;
        private string _nameusername;
        private string _discordname;
        private string _selldate;
        public User(int id,string code,string macAdress,string mail,string nameusername,string discordname,string selldate)
        {
            _id = id;
            _code = code;   
            _macAdress = macAdress;
            _mail = mail;
            _nameusername = nameusername;    
            _discordname = discordname;
            _selldate = selldate;
        }
       
    }
}
