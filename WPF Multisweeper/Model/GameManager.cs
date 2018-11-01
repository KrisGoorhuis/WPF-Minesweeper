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
        int xSizeDefault = 20;
        int ySizeDefault = 20;

        int defaultMineSaturation = 25;

        Random random = new Random();

        // Creating a new random inside the random mine populating function
        // successfully returned a different number with each call when stepping through.
        // But not when executing normally. Why?

        // The seed is based on the current time.
        // It ran so quickly that the time was the same.
        // So we're getting the next random number from the returned sequence, which is the first, because it's a new Random each time.
        // But always the same because the time was the same.

        // Stepping through by hand allowed time to progress.

        public GameManager()
        {

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

                    // ...earch the 8 surrounding squares for mines.
                    for (int x = i - 1; x < i + 1; x++)
                    {
                        for (int y = j - 1; y < y + 1; j++)
                        {

                            if (x < 0 || x > xSizeDefault || y < 0 || y > ySizeDefault)
                                continue;

                            if (_playField[x, y].isMined && (x != 0 && y != 0)) // Will we ever call this on a square that IS mined? I don't think so. Right side may be unecessary.
                            {
                                neighboringMines++;
                            }
                        }
                    }

                    _playField[i, j].neighboringMines = neighboringMines;
                    _playField[i, j].Content = neighboringMines;
                }
            }

            return _playField;
        }


        Queue<Square> uncoverQueue = new Queue<Square>();
        public void UncoverSquares(Square clickedSquare)
        {
            clickedSquare.isUncovered = true;
            clickedSquare.Background = Brushes.Blue;

            for (int i = clickedSquare.xCoord - 1; i <= clickedSquare.xCoord + 1; i++)
            {
                for (int j = clickedSquare.yCoord - 1; j <= clickedSquare.yCoord + 1; j++)
                {
                    if ((i >= 0 && i < xSizeDefault) && j >= 0 && j < ySizeDefault)
                    {
                        //if (i != clickedSquare.xCoord && j != clickedSquare.yCoord)
                        //{
                        //    continue;
                        //}

                        Square thisSquare = MainWindow.playField[i, j];

                        if (thisSquare.neighboringMines == 0 && !thisSquare.isUncovered && !thisSquare.isMined)
                        {
                            UncoverBlock(thisSquare);
                            uncoverQueue.Enqueue(thisSquare);
                        }

                        //if (!MainWindow.playField[i, j].isMined && !MainWindow.playField[i, j].isUncovered && MainWindow.playField[i, j].neighboringMines == 0)
                        //{
                        //    UncoverBlock(MainWindow.playField[i, j]);
                        //    uncoverQueue.Enqueue(MainWindow.playField[i, j]);
                        //}

                    }
                }
            }

            if (uncoverQueue.Count != 0)
            {
                Square nextSquare = uncoverQueue.Dequeue();
                UncoverSquares(nextSquare);
            }
            //MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            //mainWindow.DrawPlayField(MainWindow.playField);
            //Debugger.Break();
        }

        public void UncoverBlock(Square centerSquare)
        {
            for (int i = centerSquare.xCoord - 1; i < centerSquare.xCoord + 1; i++)
            {
                for (int j = centerSquare.yCoord - 1; j < centerSquare.yCoord + 1; j++)
                {
                    if ((i >= 0 && i < xSizeDefault) && j >= 0 && j < ySizeDefault && !MainWindow.playField[i, j].isMined)
                    {
                        MainWindow.playField[i, j].isUncovered = true;
                        MainWindow.playField[i, j].Background = Brushes.Blue;
                    }
                }
            }
        }


        
            //MainWindow.playField;


        



        public void GameOver(Square trippedSquare)
        {

        }


       

    }
}
