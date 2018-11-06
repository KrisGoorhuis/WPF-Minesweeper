using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Multisweeper
{
    public class GameManager
    {
        public bool gameEnded = false;

        int xSizeDefault = 20;
        int ySizeDefault = 20;

        int defaultMineSaturation = 25;

        Queue<Square> uncoverQueue = new Queue<Square>();
        Queue<Square> uncoverZeroesQueue = new Queue<Square>();


        Random random = new Random();

        public GameManager()
        {

        }

        public Square[,] NewDefaultGame()
        {
            Square[,] _playField = GeneratePlayField();
            _playField = PopulatePlayFieldWithMines(_playField);
            _playField = CalculateNeighboringMineCount(_playField);

            return _playField;
        }

        public Square[,] NewCustomSinglePlayerGame(int width, int height, int saturation)
        {
            Square[,] _playField = GeneratePlayField(width, height);
            _playField = PopulatePlayFieldWithMines(_playField, saturation);
            _playField = CalculateNeighboringMineCount(_playField);

            return _playField;
        }



        public Square[,] GeneratePlayField()
        {
            Square[,] _playField = new Square[xSizeDefault, ySizeDefault];

            for (int i = 0; i < _playField.GetLength(0); i++)
            {
                for (int j = 0; j < _playField.GetLength(1); j++)
                {
                    _playField[i, j] = new Square();

                    _playField[i, j].xCoord = i;
                    _playField[i, j].yCoord = j;
                }
            }

            return _playField;
        }

        public Square[,] GeneratePlayField(int xSize, int ySize)
        {
            Square [,] _playField = new Square[xSize, ySize];

            for (int i = 0; i < _playField.GetLength(0); i++)
            {
                for (int j = 0; j < _playField.GetLength(1); j++)
                {
                    _playField[i, j] = new Square();

                    _playField[i, j].xCoord = i;
                    _playField[i, j].yCoord = j;
                }
            }

            return _playField;
        }


        // This has an overload instead of setting saturation to our game's default 
        // if specified saturation is default int value of 0.
        // This way players can specify a saturation of 0 and actually get a board with no mines.
        public Square[,] PopulatePlayFieldWithMines(Square[,] _playField)
        {

            for (int i = 0; i < _playField.GetLength(0); i++)
            {
                for (int j = 0; j < _playField.GetLength(1); j++)
                {
                    int isMined = random.Next(0, 100);

                    if (isMined <= defaultMineSaturation)
                        _playField[i, j].isMined = true;
                    else
                        _playField[i, j].isMined = false;
                }
            }

            return _playField;
        }

        public Square[,] PopulatePlayFieldWithMines(Square[,] _playField, int specifiedSaturation)
        {

            for (int i = 0; i < _playField.GetLength(0); i++)
            {
                for (int j = 0; j < _playField.GetLength(1); j++)
                {
                    int isMined = random.Next(0, 100);

                    if (isMined <= specifiedSaturation)
                        _playField[i, j].isMined = true;
                    else
                        _playField[i, j].isMined = false;
                }
            }

            return _playField;
        }

        public Square[,] CalculateNeighboringMineCount(Square[,] _playField)
        {
            // For each square in the field...
            for (int i = 0; i < _playField.GetLength(0); i++)
            {
                for (int j = 0; j < _playField.GetLength(1); j++)
                {

                    int neighboringMines = 0;

                    // ...search the 8 surrounding squares for mines.
                    for (int x = i - 1; x <= i + 1; x++)
                    {
                        for (int y = j - 1; y <= j + 1; y++)
                        {
                            if (x < 0 || x >= _playField.GetLength(0) || y < 0 || y >= _playField.GetLength(1))
                                continue;

                            if (_playField[x, y].isMined) 
                                neighboringMines++;
                        }
                    }

                    _playField[i, j].neighboringMines = neighboringMines;
                    _playField[i, j].Content = neighboringMines;
                }
            }
            
            return _playField;
        }

  

        public void UncoverSingleSquare(Square square)
        {
            square.isUncovered = true;
            square.Background = Brushes.Blue;
        }

        public void ClearZeroes(Square centerSquare)
        {
            Square[,] playField = MainWindow.playField;

            for (int i = centerSquare.xCoord - 1; i <= centerSquare.xCoord + 1; i++)
            {
                for (int j = centerSquare.yCoord - 1; j <= centerSquare.yCoord + 1; j++)
                {
                    // If outside range
                    if (i < 0 || i >= playField.GetLength(0) || j < 0 || j >= playField.GetLength(1))
                        continue;

                    Square thisSquare = MainWindow.playField[i, j];
                    if (thisSquare.isMined)
                        continue;

                    if (thisSquare.neighboringMines == 0 && thisSquare.isUncovered == false)
                    {
                        uncoverZeroesQueue.Enqueue(thisSquare);
                    }

                    UncoverSingleSquare(thisSquare);
                }
            }

            if (uncoverZeroesQueue.Count != 0)
            {
                Square next = uncoverZeroesQueue.Dequeue();
                ClearZeroes(next);
            }
        }


        public void GameOver(Square trippedSquare)
        {
            gameEnded = true;

            MessageBoxResult result = MessageBox.Show("I'm a temporary game over screen!",
                                          "Game Over",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Exclamation);
        }


       

    }
}
