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
    public partial class EndGameForm : Form
    {
        Button finalMsg; 

        public EndGameForm(string text)

        {
            this.FormBorderStyle = FormBorderStyle.None;

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            this.StartPosition = FormStartPosition.CenterScreen;

            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 1,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
            };

            finalMsg = new Button
            {
                Text = text,
                ForeColor = Color.Red,
                AutoSize = true,
                Font = new Font("Times New Roman", 24, FontStyle.Bold),
            };

            finalMsg.Click += (sender, e) =>
            {
                this.Close();
            };

            panel.Controls.Add(finalMsg, 0, 0);

            this.Controls.Add(finalMsg);
            this.CancelButton = finalMsg;
        }
    }
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
            bool isMine = model.click(x, y);
            UpdateButtons();

            if (isMine)
            {
                var endGameForm = new EndGameForm("Вы взорвались!");
                this.Hide();
                endGameForm.ShowDialog();
                this.Close();
            }
        }

        private void MineFieldForm_Load(object sender, EventArgs e)
        {

        }
    }
}
