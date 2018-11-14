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
        public string face = ":)";

        string previousGameMode = "default"; // default, custom, multiplayer
        //int previousCustomMineSaturation;
        int previousCustomMines;
        int previousCustomWidth;
        int previousCustomHeight;

        UIArea uiArea = new UIArea();

        public MainWindow()
        {
            InitializeComponent();

            playField = gameManager.NewDefaultGame();

            DrawPlayField(playField);
        }

        // Game start methods
        void StartDefaultSingplePlayer(object sender, RoutedEventArgs e)
        {
            previousGameMode = "default";
            playField = gameManager.NewDefaultGame();
            DrawPlayField(playField);
        }

        public void StartCustomSinglePlayer(int width, int height, int mines)
        {
            playField = gameManager.NewCustomSinglePlayerGame(width, height, mines);

            previousGameMode = "custom";
            previousCustomWidth = width;
            previousCustomHeight = height;
            previousCustomMines = mines;
            //previousCustomMineSaturation = saturation;

            DrawPlayField(playField);
        }

        void HostMultiplayer(object sender, RoutedEventArgs e)
        {
            GameHost gameHost = new GameHost();

        }

        void OpenJoinSetup(object sender, RoutedEventArgs e)
        {
            JoinMultiplayerSetup window = new JoinMultiplayerSetup();
            window.Owner = this;
            window.ShowDialog();
        }

        public void JoinMultiplayerGame()
        {

        }


        public void SmileyButton(object sender, RoutedEventArgs e)
        {

            if (previousGameMode == "default")
            {
                playField = gameManager.NewDefaultGame();
                DrawPlayField(playField);
            }

            if (previousGameMode == "custom")
            {
                playField = gameManager.NewCustomSinglePlayerGame(previousCustomWidth, previousCustomHeight, previousCustomMines);
                DrawPlayField(playField);
            }

            if (previousGameMode == "multiplayer")
            {

            }
        }


        // Options
        void ToggleUsefulFirstMove(object sender, RoutedEventArgs e)
        {
            if (gameManager.usefulFirstMove == true)
                gameManager.usefulFirstMove = false;
            else
                gameManager.usefulFirstMove = true;
        }

        void SetDifficulty(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            gameManager.SetDifficulty(mi.Header as string);

            MenuItem parent = mi.Parent as MenuItem;
            foreach (object subitem in parent.Items)
            {
                if (subitem is MenuItem)
                {
                    MenuItem submenu = subitem as MenuItem;
                    if (submenu.Tag as string == "difficulty" && submenu.Header as string != mi.Header as string)
                    {
                        submenu.IsChecked = false;
                    }
                }
            }
        }

        // Called by Dig() in the Square class.
        public void MakeFirstMoveUseful(int x, int y)
        {
            if (previousGameMode == "default")
            {
                playField = gameManager.NewDefaultGame();
                DrawPlayField(playField);
            }

            if (previousGameMode == "custom")
            {
                playField = gameManager.NewCustomSinglePlayerGame(previousCustomWidth, previousCustomHeight, previousCustomMines);
                DrawPlayField(playField);
            }

            if (previousGameMode == "multiplayer")
            {

            }

            playField[x, y].Dig();
        }



        // Visual bits
        public void DrawPlayField(Square[,] playField)
        {
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                for (int j = 0; j < playField.GetLength(1); j++)
                {
                    Square square = playField[i, j];

                    Canvas.SetLeft(square, 0 + (square.squareWidth * i));
                    Canvas.SetTop(square, square.squareHeight * j);

                    
                    // Reveal everything on game start:
                    //if (!square.isMined && square.isUncovered)
                    //{
                    //    square.Background = Brushes.Blue;
                    //}

                    //if (square.isMined)
                    //    square.Background = Brushes.Red;
                    //else
                    //    square.Background = Brushes.Gainsboro;

                    mainCanvas.Children.Add(square);
                };
            }

            unflaggedMinesCounter.Text = Convert.ToString(gameManager.unflaggedMinesSupposed);

            Application.Current.MainWindow.Width = playField[0, 0].squareWidth * (playField.GetLength(0) + 1);
            Application.Current.MainWindow.Height = playField[0, 0].squareHeight * (playField.GetLength(1) + 1) + topBar.Height + 40; // 40 - Don't know where some of the height is coming from yet. Let's fudge it.
        }

        public void RevealMines(Square clickedMined)
        {
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                for (int j = 0; j < playField.GetLength(1); j++)
                {
                    Square square = playField[i, j];

                    clickedMined.Background = Brushes.DarkRed;
                    if (square.isMined)
                        square.Background = Brushes.OrangeRed;
                    //else
                    //    square.Background = Brushes.Gainsboro;
                }
            }
        }

        public void ConfigureCustomSinglePlayer(object sender, RoutedEventArgs e)
        {
            // TODO: Validate input!
            CustomSinglePlayer window = new CustomSinglePlayer();
            window.Owner = this;
            window.ShowDialog();
        }


    }
}
