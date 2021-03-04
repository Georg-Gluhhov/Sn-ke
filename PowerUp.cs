using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snäke
{
    class PowerUpCreator
    {
        int mapWidht;
        int mapHeight;
        char sym;
        Random random = new Random();

        public PowerUpCreator(int mapWidth, int mapHeight, char sym)
        {
            this.mapHeight = mapHeight;
            this.mapWidht = mapWidth;
            this.sym = sym;

        }

        public Point CreatePowerUp()
        {
            int x = random.Next(1, mapWidht - 10);
            int y = random.Next(1, mapHeight - 10);
            return new Point(x, y, sym);
        }
    }

}
