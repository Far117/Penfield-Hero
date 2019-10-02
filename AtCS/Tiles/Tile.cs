using AtCS.AtScreen;
using AtCS.Entities;

namespace AtCS.Tiles
{
    public class Tile : Entity
    {
        protected bool passable;
        protected bool discovered = false;

        public Tile(int x, int y, char c, bool p) : base(x, y, c)
        {
            this.passable = p;
            return;
        }

        public bool IsPassable()
        {
            return this.passable;
        }

        public void Discover()
        {
            this.discovered = true;
            return;
        }

        public override void Draw(Screen screen)
        {
            screen.PutChar(discovered ? this.symbol : '?', this.x, this.y);
            return;
        }

    }
}
