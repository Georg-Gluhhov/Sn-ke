using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snäke
{
    class TrapCreator
    {
        int mapWidht;
        int mapHeight;
        char sym;
        Random random = new Random();

        public TrapCreator(int mapWidth, int mapHeight, char sym)
        {
            this.mapHeight = mapHeight;
            this.mapWidht = mapWidth;
            this.sym = sym;

        }
        
        public Point CreateTrap()
        {
            int x = random.Next(5, mapWidht - 1);
            int y = random.Next(5, mapHeight - 1);
            return new Point(x, y, sym);
        }
    }

}
