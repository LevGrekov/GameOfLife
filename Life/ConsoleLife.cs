using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    public class ConsoleLife
    {
        public int LiveInStep { get; set; }

        private int Width;
        private int Height;
        private Life GameOfLife;
        private double Pause;
        public ConsoleLife(int Width,int Height,double Pause)    
        {
            this.Pause = Pause * 1000;
            this.Width = Width;
            this.Height = Height;
            GameOfLife = new Life(Width, Height);
            printStartCondition();
            Start();
        }

        public void printStartCondition()
        {
            // -------
            // ---XX--
            // -XXX---
            // -------
            // -------

            for (int i = 0; i < 5 ; i++)
            {
                var a = Console.ReadLine().ToCharArray();
                for(int j = 0; j < a.Length; j++)
                {
                    GameOfLife[i+10, j+10] = a[j] == '-' ? false : true;
                }
            }
        }
        public void Start()
        {
            while (true)
            {
                Console.Clear(); // Очистка экрана консоли

                // Отрисовка текущего состояния клеток на консоли
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        Console.Write(GameOfLife[i, j] ? "X" : "-"); // Вывод символа "O" или "." в зависимости от состояния клетки
                    }
                    Console.WriteLine(); // Переход на следующую строку
                }

                GameOfLife.Step(out int? lol);

                // Задержка перед отображением следующего поколения
                System.Threading.Thread.Sleep((int)Pause);
            }
        }
    }
}
