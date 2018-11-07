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
    public class Square : Button
    {
        GameManager gameManager = MainWindow.gameManager;

        public int squareWidth = 17;
        public int squareHeight = 17;

        public string defaultColor = "gray";
        public string unsureFlagColor = "yellow";
        public string minedFlagColor = "red";

        public int neighboringMines;
        public bool isMined;
        public bool isUncovered;
        public int xCoord;
        public int yCoord;

        // 0 - unmarked, 1 - mined, 2 - unsure
        public int flag = 0;

        
        public Square()
        {
            gameManager = MainWindow.gameManager;

            Width = squareWidth;
            Height = squareHeight;

            PreviewMouseLeftButtonDown += LeftClick;
            MouseRightButtonDown += RightClick;
        }

        void LeftClick(object sender, RoutedEventArgs e)
        {
            if (gameManager.gameEnded)
                return;

            Dig();
            gameManager.CheckVictoryConditions();
        }

        void RightClick(object sender, RoutedEventArgs e)
        {
            if (gameManager.gameEnded)
                return;

            CycleFlag();
            gameManager.CheckVictoryConditions();
        }

        public void Dig()
        {
            if (gameManager.takingFirstMove && gameManager.usefulFirstMove)
            {
                if (neighboringMines != 0)
                {
                    //gameManager.totalMines = 0;
                    ((MainWindow)System.Windows.Application.Current.MainWindow).MakeFirstMoveUseful(xCoord, yCoord);
                    return;
                }
            }

            gameManager.takingFirstMove = false;

            // 0 is unmarked.
            if (isUncovered || flag != 0)
            {
                return;
            }

            if (isMined)
            {
                gameManager.GameOver(this);
                ((MainWindow)System.Windows.Application.Current.MainWindow).RevealMines(this);
                return;
            }

            if (neighboringMines >= 1)
            {
                gameManager.UncoverSingleSquare(this);
            }
            else if (neighboringMines == 0)
            {
                gameManager.ClearZeroes(this);
            }

        }

        void CycleFlag()
        {
            if (isUncovered)
                return;

            if (flag == 0)
            {
                flag = 1;

                gameManager.unflaggedMinesSupposed--;
                if (isMined)
                {
                    gameManager.unflaggedMinesActual--;
                }
            }
            else if (flag == 1)
            {
                flag = 0;

                gameManager.unflaggedMinesSupposed++;
                if (isMined)
                {
                    gameManager.unflaggedMinesActual++;
                }

            }

            ((MainWindow)System.Windows.Application.Current.MainWindow).unflaggedMinesCounter.Text = Convert.ToString(gameManager.unflaggedMinesSupposed);

            switch (flag)
            {
                case 0:
                    Background = Brushes.Gainsboro;
                    break;
                case 1:
                    Background = Brushes.Yellow;
                    break;
                    // Warning flag
                //case 2:
                //    Background = Brushes.Yellow;
                //    break;
            }
        }
        
    }
}
