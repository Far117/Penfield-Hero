using System.Media;

namespace AtCS.Music
{
    public class Song
    {
        private readonly uint BPM;
        private readonly int length;
        private readonly SoundPlayer player;
        private readonly string filePath;
        private readonly BeatString beats;

        public Song(string filePath, uint BPM)
        {
            this.BPM = BPM;
            this.filePath = filePath;
            this.player = new SoundPlayer(filePath);

            return;
        }

        public static double BPMtoBPS (uint BPM_)
        {
            return (double) BPM_ / 60;
        }

        public void Start()
        {
            if (!IsLinux)
            {
                player.Load();
                player.Play();
            }
            else
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "aplay";
                //process.StartInfo.WorkingDirectory = Path.GetFullPath("/");
                process.StartInfo.Arguments = filePath;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
            }

            return;
        }

        public static bool IsLinux
        {
            get
            {
                int p = (int)System.Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
    }
}
