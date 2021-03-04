using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace Snäke
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 25);
        
            Walls walls = new Walls(80, 25);
            walls.Draw();


            Console.SetCursorPosition(86, 8);
            Console.WriteLine("Нажмите enter чтобы начать игру");
            Console.SetCursorPosition(86, 9);
            Console.WriteLine("Нажмите esc чтобы выйти");
            ConsoleKeyInfo cki = Console.ReadKey(true);

            Console.SetCursorPosition(86, 9);
            switch (cki.Key)
            {
                case ConsoleKey.Enter:
                    Console.SetCursorPosition(86, 8);
                    Console.WriteLine("                               ");
                    Console.SetCursorPosition(86, 9);
                    Console.WriteLine("                               ");
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }

            PlaySound(2);



            Point p = new Point(50, 10, '░');
            Snäke snake = new Snäke(p, 4, Direction.LEFT);
            Console.SetCursorPosition(86, 12);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, '$');
            Point food = foodCreator.CreateFood();
            Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
            food.Draw();

            PowerUpCreator powerUpCreator = new PowerUpCreator(80, 25, '!');
            Point PowerUp = powerUpCreator.CreatePowerUp();
            Console.ForegroundColor = ConsoleColor.Blue; // устанавливаем цвет
            PowerUp.Draw();

            TrapCreator trapCreator = new TrapCreator(80, 25, '¤');
            Point trap = trapCreator.CreateTrap();
            Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
            trap.Draw();
            Console.ResetColor(); // сбрасываем в стандартный

            int Times = 100;


            int Count = 0;
            WritePoints(Count);
            while (true)

            {
                Random rnd1 = new Random();
                int s = rnd1.Next(8);
                if (walls.IsHit(snake)  || snake.IsHitTail() || (snake.Toch(trap)))
                {
                    WriteGameOver();
                    InputName(Count);

                    break;
                }
                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
                    food.Draw();
                    trap = trapCreator.CreateTrap();
                    Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
                    trap.Draw();
                    Console.ResetColor(); // сбрасываем в стандартный

                    Count += 1;
                    WritePoints(Count);
                    PlaySound(1);
                    if (s == 5)
                    {
                        PowerUp = powerUpCreator.CreatePowerUp();
                        Console.ForegroundColor = ConsoleColor.Blue; // устанавливаем цвет
                        PowerUp.Draw();
                        Console.ResetColor(); // сбрасываем в стандартный
                    }



                }
                if (snake.Found(PowerUp))
                {
                    Count += 5;
                    WritePoints(Count);
                    PlaySound(3);
                    Times -= 10;


                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(Times);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }

            }

            Console.ReadLine();
        }

       




        static void InputName(int Count)
        {
            Console.SetCursorPosition(86, 12);
            Console.WriteLine("Введите ваше имя: ");
            Console.SetCursorPosition(86, 13);
            string userName = Console.ReadLine();
            Console.SetCursorPosition(86, 12);
            Console.WriteLine("Вы добавленны в лидерборд!");
            Console.SetCursorPosition(86, 13);
            Console.WriteLine("Имя: "+userName +"  Очки: "+ Count);
            StreamWriter f = new StreamWriter(@"..\..\LeaderBoard.txt");
            f.WriteLine('\n' + "Имя:" + userName + " Очки:" + Count);
            f.Close();



            
        }
        static void PlayRandomSound()
        {
            Random rnd1 = new Random();
            int s = rnd1.Next(10);
            if (s == 5)
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resouceStream =
                    assembly.GetManifestResourceStream(@"Snäke.Powerup4.wav");
                SoundPlayer player = new SoundPlayer(resouceStream);
                player.Play();
                player.PlaySync();
            }
            else
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resouceStream =
                    assembly.GetManifestResourceStream(@"Snäke.Powerup3.wav");
                SoundPlayer player = new SoundPlayer(resouceStream);
                player.Play();
                player.PlaySync();

            }
            PlayMusicAgain();


        }
        static void PlaySound(int Sound)
        {
            if (Sound == 0)
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resouceStream =
                    assembly.GetManifestResourceStream(@"Snäke.Hit_Hurt4.wav");
                SoundPlayer player = new SoundPlayer(resouceStream);
                player.Play();
                player.PlaySync();
            }
            if (Sound == 1)
            {
                PlayRandomSound();
            }
            if (Sound == 2)
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resouceStream =
                    assembly.GetManifestResourceStream(@"Snäke.dcd.wav");
                SoundPlayer player = new SoundPlayer(resouceStream);
                player.Play();
            }
            if (Sound == 3)
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream resouceStream =
                    assembly.GetManifestResourceStream(@"Snäke.Powerup27.wav");
                SoundPlayer player = new SoundPlayer(resouceStream);
                player.Play();
                PlayMusicAgain();
            }
        }
            static void PlayMusicAgain()
            {

            Thread.Sleep(600);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream resouceStream =
                assembly.GetManifestResourceStream(@"Snäke.dcd.wav");
            SoundPlayer player = new SoundPlayer(resouceStream);
            player.Play();

            }
        

        static void WriteGameOver()
        {
            int xOffset = 86;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("И Г Р А    О К О Н Ч Е Н А", xOffset + 1, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            PlaySound(0);
        }
        static void WritePoints(int Count)
        {
            
            int xOffset = 96;
            int yOffset = 4;
            Console.SetCursorPosition(xOffset, yOffset++);
            Thread.Sleep(80);
            WriteText("======", xOffset, yOffset++);
            Thread.Sleep(80);
            WriteText(" Очки", xOffset, yOffset++);
            Thread.Sleep(80);
            WriteText("======", xOffset, yOffset++);
            Thread.Sleep(80);
            WriteInt(Count, xOffset + 1, yOffset++);

        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);

        }
        static void WriteInt(int points, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(" "+points);
        }

    } 

}