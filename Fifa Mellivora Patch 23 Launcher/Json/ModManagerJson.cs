using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifa_Mellivora_Patch_23_Launcher.Json
{
    internal class ModManagerJson
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class AppliedMod
        {
            public string ModFilePath { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class FIFA23
        {
            public string Name { get; set; }
            public GameSettings GameSettings { get; set; }
            public string LocaleIniModFilePath { get; set; }
            public int LocaleIniIndex { get; set; }
            public List<AppliedMod> AppliedMods { get; set; }
        }

        public class GamesConfig
        {
            public List<string> ExecutablePaths { get; set; }
            public string DefaultGameExecutablePath { get; set; }
        }

        public class GameSettings
        {
            public int AudioMixMode { get; set; }
            public int ControllerDefault { get; set; }
            public int DirectXSelect { get; set; }
            public bool FullScreen { get; set; }
            public int MsaaLevel { get; set; }
            public int RenderingQuality { get; set; }
            public int ResolutionHeight { get; set; }
            public int ResolutionWidth { get; set; }
            public bool VoiceChat { get; set; }
            public int WaitForVsync { get; set; }
            public bool WindowedBorderless { get; set; }
        }

        public class LastUsedProfileName
        {
            public string FIFA23 { get; set; }
        }

        public class Profiles
        {
            public List<FIFA23> FIFA23 { get; set; }
        }

        public class Root
        {
            public string LastSavedProgramVersion { get; set; }
            public UpdateConfig UpdateConfig { get; set; }
            public GamesConfig GamesConfig { get; set; }
            public Profiles Profiles { get; set; }
            public LastUsedProfileName LastUsedProfileName { get; set; }
            public bool AlternativeLaunchMethod { get; set; }
            public bool VerifyModChecksumsOnInstall { get; set; }
            public bool VerifyModChecksumsOnStartup { get; set; }
            public bool LowMemoryMode { get; set; }
            public bool DeleteLiveTuningUpdates { get; set; }
            public bool DeleteModDataLocaleIniIfPresentInInitFs { get; set; }
            public bool DeleteAssetsFilesWithDbEdits { get; set; }
        }

        public class UpdateConfig
        {
            public string UpdateCheckUri { get; set; }
        }


    }
}
