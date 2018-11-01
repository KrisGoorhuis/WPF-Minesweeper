using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Multisweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public static GameManager gameManager = new GameManager();
        public static Square[,] playField;

        UIArea uiArea = new UIArea();

        public MainWindow()
        {
            InitializeComponent();

            Square[,] _playField = gameManager.GeneratePlayField();
            _playField = gameManager.PopulatePlayFieldWithMines(_playField);
            _playField = gameManager.CalculateNeighboringMineCount(_playField);
            playField = _playField;

            DrawPlayField(playField);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            //Do work
        }

        void StartDefaultSingpleplayer(object sender, RoutedEventArgs e)
        {
            Square[,] _playField = gameManager.GeneratePlayField();
            _playField = gameManager.PopulatePlayFieldWithMines(_playField);

            playField = _playField;

            DrawPlayField(playField);

        }

        void StartCustomSinglePlayer(object sender, RoutedEventArgs e)
        {

            // TODO: Input must be an integer between 0 and 100; Validate!

            // TODO: access input field
            int specifiedSaturation = 0;

            Square[,] _playField = gameManager.GeneratePlayField();
            _playField = gameManager.PopulatePlayFieldWithMines(_playField, specifiedSaturation);

            playField = _playField;

            DrawPlayField(playField);
        }

        void HostMultiplayer(object sender, RoutedEventArgs e)
        {

        }

        void JoinMultiplayer(object sender, RoutedEventArgs e)
        {

        }

       



        public void DrawPlayField(Square[,] playField)
        {
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                for (int j = 0; j < playField.GetLength(1); j++)
                {
                    Square square = playField[i, j];

                    Canvas.SetLeft(square, 0 + (square.squareWidth * i));
                    Canvas.SetTop(square, uiArea.UIHeight + square.squareHeight * j);


                    if (!square.isMined && square.isUncovered)
                    {
                        square.Background = Brushes.Blue;
                    }

                    if (square.isMined)
                        square.Background = Brushes.Red;
                    else
                        square.Background = Brushes.Gainsboro;

                    mainCanvas.Children.Add(square);
                };
            }

            Application.Current.MainWindow.Width = playField[0, 0].squareWidth * (playField.GetLength(0) + 1);
            Application.Current.MainWindow.Height = playField[0, 0].squareHeight * (playField.GetLength(1) + 1) + uiArea.UIHeight + 40; // 40 - Don't know where some of the height is coming from yet. Let's fudge it.
        }



        void RevealMines()
        {
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                for (int j = 0; j < playField.GetLength(1); j++)
                {
                    Square square = playField[i, j];

                    if (square.isMined)
                        square.Background = Brushes.Red;
                    else
                        square.Background = Brushes.Gainsboro;
                }
            }
        }

    }
}
