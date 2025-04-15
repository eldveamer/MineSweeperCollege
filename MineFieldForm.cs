using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MineFieldForm : Form
    {
        private MineSweeperModel model;
        const int buttonSize = 40;
        public void InitializeGame(int size)
        {
            buttons = new Button[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button button = new Button
                    {
                        Location = new Point(j * buttonSize, i * buttonSize),
                        Size = new Size(buttonSize, buttonSize),
                        Tag = (i, j)
                    };

                    //button.Click += Button_Click; 
                    this.Controls.Add(button);
                    buttons[i, j] = button;
                }
            }
        }
        public MineFieldForm(int size, int mines)
        {
            InitializeComponent();

            int border = buttonSize * size;

            this.model = new MineSweeperModel(size, mines);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(border, border);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeGame(size);
        }

        private Button[,] buttons;

        private void MineFieldForm_Load(object sender, EventArgs e)
        {

        }
    }
}
