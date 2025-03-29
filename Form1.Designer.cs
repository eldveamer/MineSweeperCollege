using System.Drawing.Text;

namespace MineSweeper
{
    public class MineSweeperModel
    {
        private class Cell
        {
            private bool stateIsOpen;
            private bool stateIsMine;
            private int stateNearbyMines;

            public Cell(bool isOpen, bool IsMine, int nearbyMines)
            {
                stateIsOpen = isOpen;
                stateIsMine = IsMine;
                stateNearbyMines = nearbyMines;
            }
        }

        private class MineFieldCreate
        {
            int minesAmount = 0;
            private Cell[,] mineField;

            public MineFieldCreate(int size, int mines)
            {
                minesAmount = mines;
                mineField = new Cell[size, size];
            }
        }

        public void init(int i, int j)
        {
            //todo проиницилмзировать минное поле
        }
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
