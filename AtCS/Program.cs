using AtCS.AtScreen;
using AtCS.Entities;
using AtCS.Tiles;

using System.Collections.Generic;


namespace AtCS
{
    class MainClass
    {
        private static string[] map =
            {
                "#####",
                "#...#",
                "#...#",
                "#...#",
                "#####"
            };


        private static void FillDirt(Tile[,] tiles)
        {
            for (int y = 0; y < tiles.GetLength(0); y++)
            {
                for (int x = 0; x < tiles.GetLength(1); x++)
                {
                    tiles[y, x] = new Dirt(x, y);
                }
            }

            return;
        }

        /*
        public static void Main(string[] args)
        {
            Screen screen = new Screen(map.Length, map[0].Length);
            List<Entity> entities = new List<Entity>();
            Tile[,] tiles = MapParser.Parse(map);

            entities.Add(new Player(2, 2));

            while (true)
            {
                screen.Clear(' ');

                for (int y = 0; y < tiles.GetLength(0); y++)
                {
                    for (int x = 0; x < tiles.GetLength(1); x++)
                    {
                        tiles[y, x].Tick(screen, tiles);
                        tiles[y, x].Draw(screen);
                    }
                }

                foreach (Entity e in entities)
                {
                    e.Tick(screen, tiles);
                    e.Draw(screen);
                }

                screen.Draw();
            }
        }
        */
    }
}
