using System.Collections.Generic;
using System.Diagnostics;
using AtCS.AtScreen;
using AtCS.Tiles;

namespace AtCS.Entities
{
    public class Arrow : Entity
    {
        public static readonly IDictionary<char, char> DIRS = new Dictionary<char, char>()
        {
            {'u','^'},
            {'d', 'V'},
            {'l', '<'},
            {'r', '>'}
        };
        public static readonly IDictionary<char, System.ConsoleColor> COLORS = 
            new Dictionary<char, System.ConsoleColor>()
        {
            {'^', System.ConsoleColor.Cyan},
            {'V', System.ConsoleColor.Red},
            {'<', System.ConsoleColor.Green},
            {'>', System.ConsoleColor.Yellow}
        };

        private readonly Stopwatch timer;
        private readonly double delta;
        private bool missed = false;

        //public Arrow(int x, int y, char c, int delta) : base(x, y, DIRS[c])
        public Arrow(int x, int y, char c, double delta) : base(x, y, DIRS[c])
        {
            this.timer = new Stopwatch();
            this.timer.Start();

            this.delta = delta;
        }

        public override void Tick(Screen scr, Tile[,] tiles)
        {
            base.Tick(scr, tiles);
            if (timer.Elapsed.TotalSeconds >= delta)
            {
                timer.Restart();
                this.y++;

                if (y > scr.GetHeight())
                {
                    this.Delete();
                    this.missed = true;
                }

            }

            return;
        }

        public override void Draw(Screen screen)
        {
            screen.PutCharColor(this.symbol, this.x, this.y, COLORS[this.symbol]);
            return;
        }

        public bool Missed() { return this.missed; }
    }
}
