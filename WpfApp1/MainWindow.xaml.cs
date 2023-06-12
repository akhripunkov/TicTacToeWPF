using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool turnX = true; // X начинает
        private int moveCount = 0;
        private bool computerGame = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Content = turnX ? "X" : "O";
            button.IsEnabled = false;
            turnX = !turnX;
            moveCount++;

            CheckForWinner();

            if (moveCount < 9 && computerGame && !turnX)
            {
                ComputerMove();
            }
        }
        
        private void ComputerMove()
        {
            var rnd = new Random();
            bool moveMade = false;
            while (!moveMade)
            {
                int row = rnd.Next(3);
                int col = rnd.Next(3);
                var button = (Button)FindName($"btn{row}{col}");
                if (string.IsNullOrEmpty(button.Content?.ToString()))
                {
                    button.Content = "O";
                    button.IsEnabled = false;
                    moveMade = true;
                    turnX = !turnX;
                    moveCount++;
                }
            }

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            bool winner = false;

            // Проверяем горизонтали
            for (int i = 0; i < 3; i++)
            {
                if (CheckRow(i))
                {
                    winner = true;
                }
            }

            // Проверяем вертикали
            for (int i = 0; i < 3; i++)
            {
                if (CheckColumn(i))
                {
                    winner = true;
                }
            }

            // Проверяем диагонали
            if (CheckDiagonals())
            {
                winner = true;
            }

            if (winner)
            {
                ShowWinMessage();
            }
            else if (moveCount == 9)
            {
                MessageBox.Show("Ничья!");
                ResetGame();
            }
        }

        private bool CheckRow(int row)
        {
            string firstButton = ((Button)FindName("btn" + row + "0")).Content?.ToString();
            string secondButton = ((Button)FindName("btn" + row + "1")).Content?.ToString();
            string thirdButton = ((Button)FindName("btn" + row + "2")).Content?.ToString();

            return !string.IsNullOrEmpty(firstButton) &&
                   firstButton == secondButton &&
                   firstButton == thirdButton;
        }

        private bool CheckColumn(int column)
        {
            string firstButton = ((Button)FindName("btn0" + column)).Content?.ToString();
            string secondButton = ((Button)FindName("btn1" + column)).Content?.ToString();
            string thirdButton = ((Button)FindName("btn2" + column)).Content?.ToString();

            return !string.IsNullOrEmpty(firstButton) &&
                   firstButton == secondButton &&
                   firstButton == thirdButton;
        }

        private bool CheckDiagonals()
        {
            string topLeftButton = ((Button)FindName("btn00")).Content?.ToString();
            string middleButton = ((Button)FindName("btn11")).Content?.ToString();
            string bottomRightButton = ((Button)FindName("btn22")).Content?.ToString();

            if (!string.IsNullOrEmpty(topLeftButton) &&
                topLeftButton == middleButton &&
                topLeftButton == bottomRightButton)
            {
                return true;
            }

            string topRightButton = ((Button)FindName("btn02")).Content?.ToString();
            string bottomLeftButton = ((Button)FindName("btn20")).Content?.ToString();

            if (!string.IsNullOrEmpty(topRightButton) &&
                topRightButton == middleButton &&
                topRightButton == bottomLeftButton)
            {
                return true;
            }

            return false;
        }

        private void ShowWinMessage()
        {
            MessageBox.Show((turnX ? "O" : "X") + " победил!");
            ResetGame();
        }

        private void ResetGame()
        {
            foreach (UIElement element in grid.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Content = "";
                    ((Button)element).IsEnabled = true;
                }
            }

            turnX = true;
            moveCount = 0;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            computerGame = false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewGameComputer_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            computerGame = true;
        }
    }
}
