using AtCS.Tiles;

namespace AtCS.Tiles
{
    public static class MapParser
    {
        public static Tile[,] Parse(string[] map)
        {
            Tile[,] tiles = new Tile[map.Length, map[0].Length];

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    tiles[y, x] = CharToTile(x, y, map[y][x]);
                }
            }

            return tiles;
        }

        public static Tile CharToTile(int x, int y, char c)
        {
            switch (c)
            {
                case '.': return new Dirt(x, y);
                case '#': return new Wall(x, y);

                default: return new Tile(x, y, c, true);
            }
        }
    }
}
