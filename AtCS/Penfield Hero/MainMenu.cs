using System.IO;

using AtCS.Music;
using AtCS.AtScreen;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

/*
 * The Sage of Musicality: Ben
 * The Sage of Composition: Matei
 * The Sage of Sound: Nik
 * The Sage of Strings: Abbie
 * The Sage of Scripts: Alex
 * The Splendid Sage: Jordan
 * The Sage of the Lambda: Parity
 * 
 * The Sage of the Seven Paths: Larry
 */
namespace AtCS.PenfieldHero
{
    public class MainMenu
    {
        private static readonly string[] sages =
        {
            "The Sage of Musicality", // Letskey
            "The Sage of Sound", // AoE
            "The Sage of Composition", // Rosalina
            "The Sage of Strings", // For River
            "The Splendid Sage", // Tunnel Vision
            "The Sage of Scripts", // The Imitation Game
            "The Sage of the Lambda", // Snail House

            "The Sage of the Seven Paths" // Megalovania
        };

        private static readonly SongData[] songs =
        {
            SongLibrary.LetskyMemories,
            SongLibrary.AoE2,
            SongLibrary.RosalinasObservatory,
            SongLibrary.ForRiver,
            SongLibrary.TunnelVision,
            SongLibrary.ImitationGame,
            SongLibrary.Lullaby,
            SongLibrary.Megalovania
        };

        private static bool[] wins = { false, false, false, false,
            false, false, false, false };

        private static int CountWins()
        {
            int i = 0;
            foreach (bool b in wins)
                if (b) i++;

            if (i >= sages.Length)
                i = sages.Length - 1;
            return i;
        }

        private static void DrawMenu(Screen scr, int selection)
        {
            int startY = scr.GetHeight() / 2 - sages.Length / 2;
            int num = 0;
            for (int i = 0; i <= CountWins(); i++)
            {
                string s = sages[i];
                if (num == selection)
                    scr.PutStringCenteredColor("> " + s + " <", startY++, wins[num++] ? System.ConsoleColor.Green : System.ConsoleColor.Red);
                else
                    scr.PutStringCenteredColor(s, startY++, wins[num++] ? System.ConsoleColor.Green : System.ConsoleColor.Red);
            }

        }

        public static int Input(int selection, Screen scr)
        {
            switch (System.Console.ReadKey(true).Key)
            {
                case System.ConsoleKey.DownArrow:
                    return (selection == CountWins()) ? selection : ++selection;
                case System.ConsoleKey.UpArrow:
                    return (selection == 0) ? selection : --selection;
                case System.ConsoleKey.Enter:
                    if (MusicGame.PlaySong(songs[selection]))
                    {
                        wins[selection] = true;
                        Save();
                    }

                    scr.Clear(' ');
                    scr.Draw();

                    return selection;

                default: return selection;
            }
        }

        public static void BinarySerializeObject(string path, object obj)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                try
                {
                    binaryFormatter.Serialize(streamWriter.BaseStream, obj);
                }
                catch (SerializationException ex)
                {
                    throw new SerializationException(((object)ex).ToString() + "\n" + ex.Source);
                }
            }
        }

        public static object BinaryDeserializeObject(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                object obj;
                try
                {
                    obj = binaryFormatter.Deserialize(streamReader.BaseStream);
                }
                catch (SerializationException ex)
                {
                    throw new SerializationException(((object)ex).ToString() + "\n" + ex.Source);
                }
                return obj;
            }
        }

        public static void Load()
        {
            if (File.Exists("res/table.dat"))
            {
                wins = (bool[])BinaryDeserializeObject("res/table.dat");
            }
        }

        public static void Save()
        {
            BinarySerializeObject("res/table.dat", wins);
            return;
        }

        public static void Init()
        {
            Load();
            Menu();
        }

        public static void Menu()
        {
            System.Console.CursorVisible = false;
            Screen screen = new Screen(70, 20);
            int selection = 0;

            DrawMenu(screen, selection);

            screen.Draw();

            while (true)
            {
                if (System.Console.KeyAvailable)
                    selection = Input(selection, screen);

                screen.Clear(' ');
                screen.ClearMask(System.ConsoleColor.Gray);
                DrawMenu(screen, selection);
                screen.Draw();

                System.Threading.Thread.Sleep(10);
            }
            return;
        }
    }
}
