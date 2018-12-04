using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Multisweeper
{
    public class GameManager
    {
        public bool gameEnded = false; // Disallowing clicks handled by square class.
        public bool takingFirstMove = true;
        public bool usefulFirstMove = true;
        public bool isMultiplayer = false;
        //public int totalMines = 0;
        public int unflaggedMinesSupposed; // Not necessarily accurate.
        public int unflaggedMinesActual;

        int safeSquaresRemaining;
        int totalSquares;

        int gameDuration = 0;

        int xSize = 15;
        int ySize = 15;
        int mineCount = 30;

        string gameOverFace = "x__x"; // First underscore isn't visible. Don't know why.
        string winFace = ":D";
        string normalFace = ":)";

        //int defaultMineSaturationThreshold = 10;

        Queue<Square> uncoverQueue = new Queue<Square>();
        Queue<Square> uncoverZeroesQueue = new Queue<Square>();

        Timer timer = new System.Timers.Timer(1000);


        Random random = new Random();

        public GameManager()
        {

        }


        public void SetDifficulty(string difficulty)
        {
            switch (difficulty)
            {
                case "Easy":
                    xSize = 10;
                    ySize = 10;
                    mineCount = 10;
                    break;
                case "Intermediate":
                    xSize = 15;
                    ySize = 15;
                    mineCount = 30;
                    break;
                case "Hard":
                    xSize = 20;
                    ySize = 20;
                    mineCount = 75;
                    break;
            }
        }


        public Square[,] NewDefaultGame()
        {
            Reset();

            Square[,] _playField = GeneratePlayField();
            _playField = PopulatePlayFieldWithMines(_playField);
            _playField = CalculateNeighboringMineCount(_playField);

            return _playField;
        }

        public Square[,] NewCustomGame(int width, int height, int saturation)
        {
            Reset();

            Square[,] _playField = GeneratePlayField(width, height);
            _playField = PopulatePlayFieldWithMines(_playField, width, height, saturation);
            _playField = CalculateNeighboringMineCount(_playField);

            return _playField;
        }

        public void Reset()
        {
            timer.Stop();
            timer = new Timer(1000);
            SetGameClock();

            takingFirstMove = true;
            gameEnded = false;

            SetSmiley(normalFace);

            gameDuration = 0;
            ((MainWindow)System.Windows.Application.Current.MainWindow).Timer.Text = gameDuration.ToString();

            //totalMines = 0;
        }

        public void StartTimer()
        {
            timer.Elapsed += TimerAction;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        void TimerAction(Object source, ElapsedEventArgs e)
        {
            SetGameClock();
        }

        void SetGameClock()
        {
            // Dispatcher.Invoke fixes the error:
            // The calling thread cannot access this object because a different thread owns it.
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                gameDuration++;

                ((MainWindow)System.Windows.Application.Current.MainWindow).Timer.Text = gameDuration.ToString();
            });
        }

        public Square[,] GeneratePlayField()
        {
            Square[,] _playField = new Square[xSize, ySize];
            totalSquares = xSize * ySize;

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
            totalSquares = xSize * ySize;

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


        public Square[,] PopulatePlayFieldWithMines(Square[,] _playField)
        {
            int currentMineCount = 0;
            int mineX;
            int mineY;

            while (currentMineCount < mineCount) { 
                mineX = random.Next(0, xSize);
                mineY = random.Next(0, ySize);

                if (_playField[mineX, mineY].isMined == false)
                {
                    _playField[mineX, mineY].isMined = true;
                    currentMineCount++;
                }
                else
                    continue;

            }

            unflaggedMinesSupposed = mineCount;
            unflaggedMinesActual = mineCount;
            safeSquaresRemaining = totalSquares - mineCount;

            //for (int i = 0; i < _playField.GetLength(0); i++)
            //{
            //    for (int j = 0; j < _playField.GetLength(1); j++)
            //    {
            //        int isMined = random.Next(0, 100);

            //        if (isMined <= defaultMineSaturationThreshold)
            //        {
            //            _playField[i, j].isMined = true;
            //            totalMines++;
            //        }
            //        else
            //            _playField[i, j].isMined = false;
            //    }
            //}

            //unflaggedMinesSupposed = totalMines;
            //unflaggedMinesActual = totalMines;
            //safeSquaresRemaining = totalSquares - totalMines;

            return _playField;
        }

        public Square[,] PopulatePlayFieldWithMines(Square[,] _playField, int width, int height, int specifiedMineCount)
        {
            int currentMineCount = 0;
            int mineX;
            int mineY;

            while (currentMineCount <= specifiedMineCount)
            {
                mineX = random.Next(0, width);
                mineY = random.Next(0, height);

                if (_playField[mineX, mineY].isMined == false)
                {
                    _playField[mineX, mineY].isMined = true;
                    currentMineCount++;
                }
                else
                    continue;

            }

            unflaggedMinesSupposed = specifiedMineCount;
            unflaggedMinesActual = specifiedMineCount;
            safeSquaresRemaining = totalSquares - specifiedMineCount;

            //for (int i = 0; i < _playField.GetLength(0); i++)
            //{
            //    for (int j = 0; j < _playField.GetLength(1); j++)
            //    {
            //        int isMined = random.Next(0, 100);

            //        if (isMined <= specifiedSaturationThreshold)
            //        {
            //            _playField[i, j].isMined = true;
            //            totalMines++;
            //        }
            //        else
            //            _playField[i, j].isMined = false;
            //    }
            //}

            //unflaggedMinesSupposed = totalMines;
            //unflaggedMinesActual = totalMines;
            //safeSquaresRemaining = totalSquares - totalMines;

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
                }
            }
            
            return _playField;
        }

  


        public void UncoverSingleSquare(Square square)
        {
            square.isUncovered = true;
            square.Background = Brushes.DarkGray;
            safeSquaresRemaining--;

            if (square.neighboringMines != 0)
            {
                square.Content = square.neighboringMines;
            }

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

                    if (thisSquare.isUncovered)
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



        public void SetSmiley(string face)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Smiley.Content = face;
        }


        public void GameOver(Square trippedSquare)
        {
            gameEnded = true;

            SetSmiley(gameOverFace);

            //MessageBoxResult result = MessageBox.Show("I'm a temporary game over screen!",
            //                              "Game Over",
            //                              MessageBoxButton.OK,
            //                              MessageBoxImage.Exclamation);
        }

        public void CheckVictoryConditions()
        {

            if (takingFirstMove)
            {
                takingFirstMove = false;
            }

            if (unflaggedMinesActual == 0 && unflaggedMinesSupposed == 0)
            {
                WinGame();
            }
            if (safeSquaresRemaining == 0)
            {
                WinGame();
            }
        }
       
        void WinGame()
        {
            gameEnded = true;
            SetSmiley(winFace);

            MessageBoxResult result = MessageBox.Show("I'm a temporary victory screen!",
                                          "Success",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
        }
    }
}
