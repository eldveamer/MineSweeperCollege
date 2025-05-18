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
        private Label[,] cells;
        const int cellSize = 40;

        public void InitializeGame(int size)
        {
            cells = new Label[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Label cell = new Label
                    {
                        Location = new Point(j * cellSize, i * cellSize),
                        Size = new Size(cellSize, cellSize),
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Tag = (i, j)
                    };
                    cell.MouseClick += Cell_MouseClick; 
                    this.Controls.Add(cell);
                    cells[i, j] = cell;
                }
            }
        }
        public MineFieldForm(int size, int mines)
        {
            InitializeComponent();

            int border = cellSize * size;

            this.model = new MineSweeperModel(size, mines);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(border, border);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeGame(size);
        }

        private void UpdateCells()
        {

            foreach (var cell in cells) 
            {
                var (i, j) = ((int, int))cell.Tag;

                if (model.isOpen(i, j))
                {
                    if (model.hasMine(i, j))
                    {
                        cell.Text = "💣";
                        cell.BackColor = Color.Red;
                        cell.Enabled = false;
                    }
                    else 
                    {
                        cell.Text = model.nearbyMines(i, j) > 0 ? model.nearbyMines(i, j).ToString() : "";
                        cell.BackColor = Color.LightGray;
                        cell.Enabled = false;
                        cell.ForeColor = Color.Black;
                    }
                }
                else if (model.hasFlag(i, j))
                {
                    cell.Text = "🚩";
                    cell.ForeColor = Color.Red;
                }
                else if (!model.hasFlag(i, j))
                {
                    cell.Text = "";
                }
            }
        }

        private void Cell_MouseClick(object sender, MouseEventArgs e)
        {
            Label cell = sender as Label;
            var (x, y) = ((int, int))cell.Tag;

            if (e.Button == MouseButtons.Left && !model.hasFlag(x, y))
            {
                bool isMine = model.click(x, y);
                UpdateCells();
                if (isMine)
                {
                    var endGameForm = new EndGameForm("Вы взорвались!");
                    endGameForm.ShowDialog();
                    this.Close();
                }
                else if (model.winCondtion())
                {
                    var endGameForm = new EndGameForm("Вы настоящий сапёр!");
                    endGameForm.ShowDialog();
                    this.Close();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                model.toggle(x, y);
                UpdateCells();
            }
        }



        private void MineFieldForm_Load(object sender, EventArgs e)
        {

        }
    }
}
