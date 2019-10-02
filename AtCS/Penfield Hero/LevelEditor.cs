using System.Diagnostics;
using System.Linq;
using System;

using AtCS.Music;

namespace AtCS.PenfieldHero
{
    public class LevelEditor
    {
        public static void Record(SongData songData)
        {
            uint lengthInSeconds = (uint) songData.duration;
            uint BPM = (uint) songData.BPM;
            Song song = new Song(songData.filePath, (uint) songData.BPM);

            double BPS = (double)BPM / 60;
            double beatTime = BPS / 4 / 4;
            int noteTime = (int)(15000 / BPM);
            char[] notes = new string('N',
            (int)(lengthInSeconds * 1000 / noteTime)).ToCharArray();

            Stopwatch timer = new Stopwatch();
            Stopwatch beatTimer = new Stopwatch();
            bool activeBeat = true;

            song.Start();
            timer.Start();
            beatTimer.Start();

            while (timer.Elapsed.TotalSeconds < songData.duration)
            {
                if (beatTimer.Elapsed.TotalSeconds >= songData.noteTimeSeconds)
                {
                    activeBeat = true;
                    beatTimer.Restart();
                    int currentBeat = (int)(timer.Elapsed.TotalSeconds / songData.noteTimeSeconds);
                    if (currentBeat > 2) Console.Write(notes[currentBeat - 2]);
                }

                if (activeBeat && Console.KeyAvailable)
                {
                    int currentNote = (int)(timer.Elapsed.TotalSeconds / songData.noteTimeSeconds);

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow: 
                            notes[currentNote] = 'u'; 
                            activeBeat = false; 
                            break;
                        case ConsoleKey.DownArrow:
                            notes[currentNote] = 'd';
                            activeBeat = false;
                            break;
                        case ConsoleKey.LeftArrow:
                            notes[currentNote] = 'l';
                            activeBeat = false;
                            break;
                        case ConsoleKey.RightArrow:
                            notes[currentNote] = 'r';
                            activeBeat = false;
                            break;
                    }
                }
            }

            Console.WriteLine("\n\n");
            foreach (char c in notes)
                Console.Write(c);

            System.IO.File.WriteAllText("Recording.mus", new string(notes));

            Console.ReadLine();

            return;
        }
    }
}
