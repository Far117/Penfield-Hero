using System.Collections.Generic;

using AtCS.AtScreen;
using AtCS.Entities;
using AtCS.Tiles;
using AtCS.Music;
using System.IO;
using System.Diagnostics;

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
    public class MusicGame
    {
        private static void KeyPress(List<Entity> entities, ScoreKeeper playerScore)
        {
            int column = 0;

            switch (System.Console.ReadKey(true).Key)
            {
                case System.ConsoleKey.UpArrow:    column = 6;  break;
                case System.ConsoleKey.DownArrow:  column = 8; break;
                case System.ConsoleKey.LeftArrow:  column = 4;  break;
                case System.ConsoleKey.RightArrow: column = 10;  break;
                default: return;
            }


            Entity lowest = null;

            foreach (Entity e in entities)
            {
                if (e is Arrow a && a.GetX() == column)
                {
                    if (lowest == null)
                        lowest = a;
                    else
                    {
                        if (a.GetY() > lowest.GetY())
                            lowest = a;
                    }
                }
            }


            if (lowest != null)
            {
                playerScore.Hit(lowest.GetY() - 20);
                lowest.Delete();
            }


        }

        //private static void SpawnNote(int note, string notes, 
        //    List<Entity> entities, int noteTime)
        private static void SpawnNote(int note, string notes,
            List<Entity> entities, double noteTime)
        {
            if (note < notes.Length && notes[note] != 'N')
            {
                switch (notes[note])
                {
                    case 'u': entities.Add(new Arrow(6, 0, 'u', noteTime)); break;
                    case 'd': entities.Add(new Arrow(8, 0, 'd', noteTime)); break;
                    case 'l': entities.Add(new Arrow(4, 0, 'l', noteTime)); break;
                    case 'r': entities.Add(new Arrow(10, 0, 'r', noteTime)); break;

                }
            }
        }

        public static bool PlaySong(SongData songData)
        {
            Screen screen = new Screen(50, 22);

            Song song = new Song(songData.filePath, (uint)songData.BPM);
            ScoreKeeper playerScore = new ScoreKeeper();

            List<Entity> entities = new List<Entity>();
            Tile[,] tiles = new Tile[0, 0];

            entities.Add(new Fretboard());

            Stopwatch noteTimer = new Stopwatch();
            Stopwatch songTimer = new Stopwatch();

            int time = 15000 / songData.BPM;
            string notes = songData.notes;
            int currentNote = 22;

            song.Start();
            noteTimer.Start();
            songTimer.Start();

            SpawnNote(currentNote++, notes, entities, songData.noteTimeSeconds);
            while (songTimer.Elapsed.TotalSeconds <= songData.duration)
            {
                List<Entity> toDelete = new List<Entity>();

                if (noteTimer.Elapsed.TotalSeconds >= songData.noteTimeSeconds)
                {
                    SpawnNote(currentNote++, notes, entities, songData.noteTimeSeconds);
                    noteTimer.Restart();
                }

                foreach (Entity e in entities)
                {
                    e.Tick(screen, tiles);
                    e.Draw(screen);

                    if (e.Deleted())
                        toDelete.Add(e);
                }

                foreach (Entity e in toDelete)
                {
                    entities.Remove(e);
                    if (e is Arrow a)
                        if (a.Missed())
                            playerScore.Miss();
                }


                screen.PutInt((int)playerScore.GetScore(), 15, 0);
                screen.PutString(string.Format("Accuracy: {0:00}%", playerScore.GetAccuracy() * 100),
                    15, 1);

                screen.Draw();
                screen.Clear(' ');

                if (System.Console.KeyAvailable)
                    KeyPress(entities, playerScore);

                //System.Threading.Thread.Sleep(GlobalConstants.SLEEP_OFFSET);
            }

            return playerScore.GetAccuracy() >= 0.6;
        }

        public static int Main(string[] args)
        {
            System.Console.CursorVisible = false;
            System.Console.Title = "Penfield Hero";
            MainMenu.Init();
           

            //LevelEditor.Record(SongLibrary.LetskyMemories);
            //return 0;

            //System.Console.ReadLine();
            System.Console.WriteLine("\nFinal score: " + 
                PlaySong(SongLibrary.ImitationGame));

            System.Console.ReadLine();

            return 0;
        }
    }
}
