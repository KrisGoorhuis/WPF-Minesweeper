using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multisweeper;

namespace Minesweeper.UnitTests
{
    [TestClass]
    public class AllOtherTests
    {
        [TestMethod]
        public void StartCustomGame_AnyParameters_CreatesPlayField()
        {
            void StartCustomGame()
            {
                int width = 10;
                int height = 10;
                int mines = 10;

                GameManager gameManager = new GameManager();
                Square[,] playField = gameManager.NewCustomGame(width, height, mines);


                
            }
        }

        [TestMethod]
        public void StartDefaultSinglePlayer()
        {

        }


        [TestMethod]
        public void StartCustomSingplePlayer()
        {

        }

    }
}
