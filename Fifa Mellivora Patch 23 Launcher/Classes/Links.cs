using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifa_Mellivora_Patch_23_Launcher.Classes
{
    public class Links
    {
        public Links(string name, string size, string type, string extension, bool github, string link, bool mod)
        {
            Name = name;
            Size = size;
            Type = type;
            Extension = extension;
            Github = github;
            Link = link;
            Mod = mod;
        }

        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Extension { get; set; }
        public bool Github { get; set; }
        public string Link { get; set; }
        public bool Mod { get; set; }

    }
}
