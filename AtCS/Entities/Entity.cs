using AtCS.AtScreen;
using AtCS.Tiles;

namespace AtCS.Entities
{
    public class Entity
    {
        protected int x = 0, y = 0;
        protected char symbol = ' ';
        protected bool delete = false;

        public Entity(int x, int y, char c)
        {
            this.x = x;
            this.y = y;
            this.symbol = c;
        }

        public virtual void Tick(Screen scr, Tile[,] tiles)
        {
            return;
        }

        public bool Move(int newX, int newY, Screen screen, Tile[,] tiles)
        {
            if (newX < 0 || newX >= screen.GetWidth() ||
                newY < 0 || newY >= screen.GetHeight())
                return false;

            tiles[newY, newX].Discover();

            if (!tiles[newY, newX].IsPassable())
                return false;

            this.x = newX;
            this.y = newY;

            return true;
        }

        public virtual void Draw(Screen screen)
        {
            screen.PutChar(this.symbol, this.x, this.y);
            return;
        }

        public int GetX() { return this.x; }
        public int GetY() { return this.y; }
        public char GetSymbol() { return this.symbol; }

        public void Delete()
        {
            this.delete = true;
            return;
        }

        public bool Deleted()
        {
            return this.delete;
        }

        public void SetX(int x)
        {
            this.x = x;
            return;
        }

        public void SetY(int y)
        {
            this.y = y;
            return;
        }

        public void SetChar(char c)
        {
            this.symbol = c;
            return;
        }
    }
}
