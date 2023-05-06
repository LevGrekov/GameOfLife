using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    public class Life
    {
        private int Width;
        private int Height;

        private bool[,] Grid;
        private bool[,] OperatedGrid;

        public Life(int Width,int Height)
        {
            this.Width = Width;
            this.Height = Height;
            Grid = new bool[Width,Height];
            OperatedGrid = new bool[Width,Height];
        }

        public void Step(out int? CountAlives)
        {
            CountAlives = 0;
            for (int i = 0; i<Width; i++)
            {
                for(int j = 0; j < Height; j++)
                {
                    bool isAlive = Grid[i, j];
                    int neighbors = 0;
                    for (int ni = -1; ni <= 1; ni++)
                    {
                        for (int nj = -1; nj <= 1; nj++)
                        {
                            if (i + ni < 0 || j + nj < 0 || i + ni > Width - 1 || j + nj > Height - 1 || (i==i+ni && j==j+nj) ) continue; //Проверка на края 
                            if (Grid[i + ni,j + nj] == true) neighbors++;
                        }
                    }
                    bool keepAlive = isAlive && (neighbors == 2 || neighbors == 3);
                    bool makeNewLive = !isAlive && neighbors == 3;
                    OperatedGrid[i, j] = keepAlive || makeNewLive;
                }
            }

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Grid[i, j] = OperatedGrid[i, j];
                    if (Grid[i, j]) CountAlives++;
                }
            }

        }

        public bool this[int i,int j]
        {
            get => Grid[i, j];
            set => Grid[i, j] = value;
        }
        public static string GetLiveInStepsForTask
        {
            get
            {
                var L = new Life(50, 50);

                for (int i = 0; i < 5; i++)
                {
                    var a = Console.ReadLine().ToCharArray();
                    for (int j = 0; j < a.Length; j++)
                    {
                        L[i + 10, j + 10] = a[j] == '-' ? false : true;
                    }
                }


                var sb = new StringBuilder();
                for (int i = 0; i < 5; i++)
                {

                    L.Step(out int? Count);
                    sb.Append(Count + " ");
                }
                return sb.ToString();
            }

        }
    }
}
       