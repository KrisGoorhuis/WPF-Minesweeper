using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multisweeper;

namespace Minesweeper.UnitTests
{
    [TestClass]
    public class GameManagerTests
    {
        // name of method_scenario_expectedbehavior
        [TestMethod]
        public void GeneratePlayField_DefaultGames_ReturnsArrayOfSquares()
        {
            // Arrange 
            GameManager gameManager = new GameManager();
            Square[,] playField;


            // Act
            playField = gameManager.GeneratePlayField();

            // Assert
            for (int i = 0; i < gameManager.xSize; i++)
            {
                for (int j = 0; j < gameManager.ySize; j++)
                {
                    Assert.IsInstanceOfType(playField[i, j], typeof(Square));
                }
            }
        }

        [TestMethod]
        public void GeneratePlayField_CustomGames_ReturnsArrayOfSquares()
        {
            // Arrange 
            GameManager gameManager = new GameManager();
            Square[,] playField;
            int testX = 10;
            int testY = 10;

            // Act
            playField = gameManager.GeneratePlayField(testX, testY);

            // Assert
            for (int i = 0; i < testX; i++)
            {
                for (int j = 0; j < testY; j++)
                {
                    Assert.IsInstanceOfType(playField[i, j], typeof(Square));
                    Console.WriteLine(playField[i, j].GetType());
                }
            }
        }

        [TestMethod]
        public void PopulatePlayFieldWithMines_AnyField_ReturnsMinedField()
        {
            GameManager gameManager = new GameManager();
            Square[,] playField;

            playField = gameManager.GeneratePlayField();
            playField = gameManager.PopulatePlayFieldWithMines(playField);


            for (int i = 0; i < gameManager.xSize; i++)
            {
                for (int j = 0; j < gameManager.ySize; j++)
                {
                    if (playField[i, j].isMined)
                    {
                        Assert.IsTrue(true);
                    }
                }
            }

            gameManager = new GameManager();
            
            playField = gameManager.GeneratePlayField(gameManager.xSize, gameManager.ySize);
            playField = gameManager.PopulatePlayFieldWithMines(playField);


            for (int i = 0; i < gameManager.xSize; i++)
            {
                for (int j = 0; j < gameManager.ySize; j++)
                {
                    if (playField[i, j].isMined)
                    {
                        Debugger.Break();
                        Assert.IsTrue(true);
                    }
                }
            }

        }

        [TestMethod]
        public void CalculateNeighboringMineCount_AnyField_ReturnsNeighborCalculations()
        {
            GameManager gameManager = new GameManager();
            Square[,] playField;

            playField = gameManager.GeneratePlayField();
            playField = gameManager.PopulatePlayFieldWithMines(playField);
            playField = gameManager.CalculateNeighboringMineCount(playField);

            for (int i = 0; i < gameManager.xSize; i++)
            {
                for (int j = 0; j < gameManager.ySize; j++)
                {
                    if (playField[i, j].neighboringMines > 0)
                    {
                        Assert.IsTrue(true);
                    }
                }
            }
        }
    }
}
