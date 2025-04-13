using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MineSweeper
{
    public partial class MineSweeperForm : Form
    {
        const int spaceInBetween = 10;
        private Button restartButton;
        private TextBox sizeTextBox;
        private TextBox minesTextBox;

        public MineSweeperForm()
        {
            InitializeComponent();
           
            this.StartPosition = FormStartPosition.CenterScreen;

            restartButton = new Button
            {
                Text = "Новая игра",
                Size = new Size(100, 30)
            };
           
            sizeTextBox = new TextBox
            {
                Text = "Введите размер поля", 
                ForeColor = Color.Gray, 
                Width = 100
            };

            minesTextBox = new TextBox
            {
                Text = "Введите кол-во мин",
                ForeColor = Color.Gray,
                Width = 100
            };

            //restartButton.Click += (sender, e) => ResetGame();
            this.Controls.Add(restartButton);
            this.Controls.Add(sizeTextBox);
            this.Controls.Add(minesTextBox);

            this.centerButton();
            this.Resize += (_, _) => centerButton();

            sizeTextBox.Enter += (sender, e) =>
            {
                if (Regex.IsMatch(sizeTextBox.Text, @"^\d+$"))
                {
                    return;
                }
                sizeTextBox.Text = "10";
                sizeTextBox.ForeColor = Color.Black;
            };

            minesTextBox.Enter += (sender, e) =>
            {
                if (Regex.IsMatch(minesTextBox.Text, @"^\d+$"))
                {
                    return;
                }
                minesTextBox.Text = "15";
                minesTextBox.ForeColor = Color.Black;
            };
        }

        private void centerButton()
        {
            // Вычисляем координаты кнопки относительно формы
            int buttonX = (this.ClientSize.Width - restartButton.Width) / 2;
            int buttonY = (this.ClientSize.Height - restartButton.Height) / 2;

            restartButton.Location = new Point(buttonX, buttonY);
            sizeTextBox.Location = new Point(buttonX, buttonY + restartButton.Height + spaceInBetween);
            minesTextBox.Location = new Point(buttonX, sizeTextBox.Location.Y + sizeTextBox.Height + spaceInBetween);
        }


        private void MineSweeperForm_Load(object sender, EventArgs e)
        {

        }
    }
}
