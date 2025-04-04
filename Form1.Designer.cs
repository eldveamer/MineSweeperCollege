using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;

namespace MineSweeper
{
    public class MineSweeperModel
    {
        private class Cell
        {
            public bool stateIsOpen;
            public bool stateIsMine;
            public int stateNearbyMines;
            
            public Cell()
            {
                stateIsOpen = false;
                stateIsMine = false;
                stateNearbyMines = 0;
            }
            public Cell(bool isOpen, bool IsMine, int nearbyMines)
            {
                stateIsOpen = isOpen;
                stateIsMine = IsMine;
                stateNearbyMines = nearbyMines;
            }
        }

        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private class MineField
        {
            int minesAmount = 0;
            private Cell[,] field;

            public MineField(int size, int mines)
            {
                minesAmount = mines;
                field = new Cell[size, size];
            }


            private List<(int, int)> nearbyIndices(int i, int j)
            {
                int xMin = Math.Max(0, j - 1);
                int xMax = Math.Min(field.GetLength(0) - 1, j + 1);
                int yMin = Math.Max(0, i - 1);
                int yMax = Math.Min(field.GetLength(1) - 1, i + 1);

                var xs = Enumerable.Range(xMin, xMax);
                var ys = Enumerable.Range(yMin, yMax);

                var indices = (from x in xs
                              from y in ys
                              where !(x == j && y == i)
                              select (x, y)).ToList();

                return indices;
            }

            private int countNearbyMines(int x, int y)
            {
                //todo min(0, x - 1), max(length(0), x+1)
                //     min(0, y - 1), max(length(1), y+1)
                //     декартово произведение (или вложенный цикл)

                int count = 0;
                var indices = nearbyIndices(x, y);

                foreach( var (i, j) in indices)
                {
                    count += Convert.ToInt32(field[i, j].stateIsMine);
                }

                return count;             
            }

            public void fillAnew(int i, int j)
            {
                //todo проиницилизировать минное поле. Примерный процесс: При первом клике клетка, которую кликнули должна всегда быть пусто
                //Дальше нужно использовать функцию/метод, который откроет все ближайшие клетки, до тех пор пока не наткнёмся на клетку
                //в радиус которой есть хотя бы одна мина. 
                //Нужно создать очередь/стек, который будет хранить координаты мины ??? и считать сколько уже созданно.
                //Нужно взять генератор рандомных чисел, который будет создавать создавать мину только с определённым шансом, до тех пор
                //пока кол-во мин не достигнет определённого кол-ва
                
                field = new Cell[field.GetLength(0), field.GetLength(1)];
                int mines = minesAmount;
                Random random = new Random();
                while (mines > 0)
                {
                    var xs = Enumerable.Range(0, field.GetLength(0));
                    var ys = Enumerable.Range(0, field.GetLength(0));

                    var coords = (from x in xs
                                  from y in ys
                                  select (x, y)).ToList();

                    Shuffle(coords);
                    foreach (var (x, y) in coords)
                    {   
                        if (mines <= 0)
                        {
                            break;
                        }
                        if ((x, y) != (i, j))
                        {
                            field[x, y].stateIsMine = true;
                        }
                        --mines;
                    }
                    
                    foreach (var (x, y) in coords)
                    {
                        field[x, y].stateNearbyMines = countNearbyMines(x, y);
                    }
                }
            }
        }
        private MineField mineField;
    }

    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
    }
}
