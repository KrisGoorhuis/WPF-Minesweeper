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
        
        public int squareWidth = 15;
        public int squareHeight = 15;

        public string defaultColor = "gray";
        public string unsureFlagColor = "yellow";
        public string minedFlagColor = "red";

        public int neighboringMines;
        public bool isMined;
        public bool isUncovered;
        public int xCoord;
        public int yCoord;

        // 0 - unmarked, 1 - unsure, 2 - mined
        public int flag = 0;


        public Square()
        {
            Width = squareWidth;
            Height = squareHeight;

            PreviewMouseLeftButtonDown += LeftClick;
            MouseRightButtonDown += RightClick;
        }

        void LeftClick(object sender, RoutedEventArgs e)
        {
            //Debugger.Break();
            Dig();
        }

        void RightClick(object sender, RoutedEventArgs e)
        {
            //Debugger.Break();
            CycleFlag();
        }


        void Dig()
        {
            // 0 is unmarked.
            if (isUncovered || flag != 0)
            {
                return;
            }

            if (isMined)
            {
                MainWindow.gameManager.GameOver(this);

                return;
            }

            if (!isMined)
            {
                Square[,] thing = MainWindow.playField;
                MainWindow.gameManager.UncoverSquares(this);
            }


        }

        void CycleFlag()
        {
            if (flag == 2)
                flag = 0;
            else
                flag++;

            switch (flag)
            {
                case 0:
                    Background = Brushes.LightGray;
                    break;
                case 1:
                    Background = Brushes.Yellow;
                    break;
                case 2:
                    Background = Brushes.Orange;
                    break;
            }
        }


        
    }
}
