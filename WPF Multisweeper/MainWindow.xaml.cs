using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        string previousGameMode = "default"; // default, custom, multiplayer
        int previousCustomMineSaturation;
        int previousCustomWidth;
        int previousCustomHeight;

        UIArea uiArea = new UIArea();

        public MainWindow()
        {
            InitializeComponent();

            playField = gameManager.NewDefaultGame();

            DrawPlayField(playField);
        }
                 
        void SmileyButton(object sender, RoutedEventArgs e)
        {
            if (previousGameMode == "default")
            {
                playField = gameManager.NewDefaultGame();
                DrawPlayField(playField);
            }

            if (previousGameMode == "custom")
            {
                playField = gameManager.NewCustomSinglePlayerGame(previousCustomWidth, previousCustomHeight, previousCustomMineSaturation);
                DrawPlayField(playField);
            }

            if (previousGameMode == "multiplayer")
            {

            }
        }

        void StartDefaultSingplePlayer(object sender, RoutedEventArgs e)
        {
            previousGameMode = "default";
            playField = gameManager.NewDefaultGame();
            DrawPlayField(playField);
        }

        public void ConfigureCustomSinglePlayer(object sender, RoutedEventArgs e)
        {
            // TODO: Input must be an integer between 0 and 100; Validate!

            CustomSinglePlayer w = new CustomSinglePlayer();
            w.Owner = this;
            w.ShowDialog();
        }

        public void StartCustomSinglePlayer(int width, int height, int saturation)
        {
            playField = gameManager.NewCustomSinglePlayerGame(width, height, saturation);

            previousGameMode = "custom";
            previousCustomWidth = width;
            previousCustomHeight = height;
            previousCustomMineSaturation = width;

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
                    Canvas.SetTop(square, square.squareHeight * j);


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
            Application.Current.MainWindow.Height = playField[0, 0].squareHeight * (playField.GetLength(1) + 1) + topBar.Height + 40; // 40 - Don't know where some of the height is coming from yet. Let's fudge it.
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
