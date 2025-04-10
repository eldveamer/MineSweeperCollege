using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;

namespace MineSweeper
{
    public static class ArrayExtensions
{
    public static void Initialize<T>(this T[,] array, Func<T> initializer)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = initializer();
            }
        }
    }
}
    public class MineSweeperModel
    {

        private class Cell
        {
            public bool stateIsOpen;
            public bool stateIsMine;
            public int stateNearbyMines;
            
            public Cell(bool isOpen, bool IsMine, int nearbyMines)
            {
                stateIsOpen = isOpen;
                stateIsMine = IsMine;
                stateNearbyMines = nearbyMines;
            }
        }

        private static Random rng = new Random();
       
        public static void Shuffle<T>(IList<T> list)
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
                int xMin = Math.Max(0, i - 1);
                int xMax = Math.Min(field.GetLength(0) - 1, i + 1);
                int yMin = Math.Max(0, j - 1);
                int yMax = Math.Min(field.GetLength(1) - 1, j + 1);

                var xs = Enumerable.Range(xMin, xMax - xMin + 1);
                var ys = Enumerable.Range(yMin, yMax - yMin + 1);

                var indices = (from x in xs
                              from y in ys
                              where !(x == i && y == j)
                              select (x, y)).ToList();

                return indices;
            }

            private int countNearbyMines(int x, int y)
            {
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
                field = new Cell[field.GetLength(0), field.GetLength(1)];
                field.Initialize(() => new Cell(false, false, 0));

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
            
            public bool openCell(int x, int y)
            {   
                if(field[x, y].stateIsOpen)
                {
                    return true;
                }
                if (field[x, y].stateIsMine)
                {
                    return false;
                }
                else
                {
                    field[x, y].stateIsOpen = true;
                }
                if (field[x, y].stateNearbyMines > 0)
                {
                    return true;
                }

                foreach (var (i, j) in nearbyIndices(x, y))
                {
                    openCell(i, j);
                }
                return true;
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
