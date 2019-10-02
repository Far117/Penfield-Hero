using System;
namespace AtCS.PenfieldHero
{
    public class ScoreKeeper
    {
        private uint score = 0,
                     combo = 0;

        private uint hit = 0,
                     miss = 0;

        public void Miss() { this.miss++; }

        public void Hit(int error)
        {
            switch (error)
            {
                case 0:
                    this.score += (100 * ++this.combo);
                    this.hit++;
                    break;
                case 1:
                case -1:
                    this.score += (75 * this.combo);
                    this.combo = 1;
                    this.hit++;
                    break;
                case 2:
                case -2:
                    this.score += (50 * this.combo);
                    this.combo = 0;
                    this.hit++;
                    break;
                case 3:
                case -3:
                    this.score += (25 * this.combo);
                    this.combo = 0;
                    this.hit++;
                    break;
                case 4:
                case -4:
                    this.score += (10 * this.combo);
                    this.combo = 0;
                    this.miss++;
                    break;

                default:
                    this.combo = 0;
                    this.miss++;
                    break;
            }

            return;
        }

        public uint GetScore() { return this.score; }
        public uint GetHit() { return this.hit; }
        public uint GetMiss() { return this.miss; }

        public double GetAccuracy()
        {
            return (double)this.hit / (this.hit + this.miss);
        }
    }
}
