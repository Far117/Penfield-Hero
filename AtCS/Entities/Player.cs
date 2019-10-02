using AtCS.AtScreen;
using AtCS.Tiles;

namespace AtCS.Entities
{
    public class Player : Entity
    {
        public Player(int x, int y) : base(x, y, '@')
        {
            return;
        }

        public override void Tick(Screen scr, Tile[,] tiles)
        {
            System.Console.SetCursorPosition(System.Console.WindowWidth - 1,
                                             System.Console.WindowHeight - 1);

            char key = System.Console.ReadKey().Key.ToString()[0];

            switch (key)
            {
                case 'W': this.Move(this.x, this.y - 1, scr, tiles); break;
                case 'A': this.Move(this.x - 1, this.y, scr, tiles); break;
                case 'S': this.Move(this.x, this.y + 1, scr, tiles); break;
                case 'D': this.Move(this.x + 1, this.y, scr, tiles); break;
            }

            return;
        }
    }
}
