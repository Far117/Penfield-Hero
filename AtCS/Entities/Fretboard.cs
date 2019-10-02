
using AtCS.AtScreen;
using AtCS.Tiles;

namespace AtCS.Entities
{
    public class Fretboard : Entity
    {
        public Fretboard() : base(4, 20, '=')
        {
            return;
        }

        public override void Draw(Screen screen)
        {
            screen.PutCharColor('=', 4, 20, System.ConsoleColor.Green);
            screen.PutCharColor('=', 6, 20, System.ConsoleColor.Cyan);
            screen.PutCharColor('=', 8, 20, System.ConsoleColor.Red);
            screen.PutCharColor('=', 10, 20, System.ConsoleColor.Yellow);
            return;
        }
    }
}
