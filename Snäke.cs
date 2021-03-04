using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snäke
{
    class Snäke : Figure
    {
        Direction direction;
        public Snäke(Point tail, int length, Direction direction)
        {
            direction = direction;
            pList = new List<Point>();
            for(int i = 0; i < length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
        }

        internal void Move()
        {
            Point tail = pList.First();
            pList.Remove(tail);
            Point head = GetNextPoint();
            pList.Add(head);

            tail.Clear();
            head.Draw();
        }
        public Point GetNextPoint()
        {
            Point head = pList.Last();
            Point nextPoint = new Point(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }
        public bool IsHitTail()
        {
            var head = pList.Last();
            for (int i = 0; i < pList.Count - 2; i++)
            {
                if (head.IsHit(pList[i]))
                    return true;
            }
            return false;
        }
        public void HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
                direction = Direction.LEFT;
            else if (key == ConsoleKey.RightArrow)
                direction = Direction.RIGHT;
            else if (key == ConsoleKey.DownArrow)
                direction = Direction.DOWN;
            else if (key == ConsoleKey.UpArrow)
                direction = Direction.UP;
            else if (key == ConsoleKey.A)
                direction = Direction.LEFT;
            else if (key == ConsoleKey.D)
                direction = Direction.RIGHT;
            else if (key == ConsoleKey.S)
                direction = Direction.DOWN;
            else if (key == ConsoleKey.W)
                direction = Direction.UP;
        }
        internal bool Eat(Point food)
        {
            Point head = GetNextPoint();
            if (head.IsHit(food)) 
            {
                food.sym = head.sym;
                pList.Add(food);
                return true;
            }
            else
                return false;
        }
        internal bool Toch(Point trap)
        {
            Point head = GetNextPoint();
            if (head.IsHit(trap))
            {
                trap.sym = head.sym;
                pList.Add(trap);
                return true;
            }
            else
                return false;
        }
        internal bool Found(Point PowerUp)
        {
            Point head = GetNextPoint();
            if (head.IsHit(PowerUp))
            {
                PowerUp.sym = head.sym;
                pList.Add(PowerUp);
                return true;
            }
            else
                return false;
        }
    }
}
