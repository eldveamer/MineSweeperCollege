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
        private Button[,] buttons;
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

                    button.Click += Button_Click; 
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

        private void UpdateButtons()
        {

            foreach (var button in buttons) 
            {
                var (i, j) = ((int, int))button.Tag;

                if (model.isOpen(i, j))
                {
                    button.Text = model.nearbyMines(i, j) > 0 ? model.nearbyMines(i, j).ToString() : "";
                    button.BackColor = Color.LightGray;
                    button.Enabled = false;
                }

                if (model.isOpen(i, j) && model.hasMine(i, j))
                {
                    button.Text = "💣"; 
                    button.BackColor = Color.Red;
                    button.Enabled = false; 
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button cell = sender as Button;
            var (x, y) = ((int, int))cell.Tag;
            model.click(x, y);
            UpdateButtons();
        }

        private void MineFieldForm_Load(object sender, EventArgs e)
        {

        }
    }
}
