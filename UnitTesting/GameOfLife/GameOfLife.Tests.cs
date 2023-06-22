namespace GameOfLife
{
    public class GameOfLifeTests
    {
        [Fact]
        public void CalculateNextGeneration_AllDeadCellsRemainDead()
        {
            var dc = GameOfLifeProcessor.DeadCell;
            var cellsGrid = new string[3, 3]
            {
                { dc, dc, dc },
                { dc, dc, dc },
                { dc, dc, dc }
            };

            string[,] result = GameOfLifeProcessor.CalculateNextGeneration(cellsGrid);


            foreach (var cell in result)
            {
                Assert.Equal(dc, cell);
            }
        }

        [Fact]
        public void CalculateNextGeneration_AliveCellsWithLessThan2AliveNeighboursBecomeDead()
        {
            var dc = GameOfLifeProcessor.DeadCell;
            var ac = GameOfLifeProcessor.AliveCell;
            var cellsGrid = new string[3, 3]
            {
                { dc, dc, dc },
                { dc, ac, dc },
                { dc, dc, dc }
            };

            string[,] result = GameOfLifeProcessor.CalculateNextGeneration(cellsGrid);


            foreach (var cell in result)
            {
                Assert.Equal(dc, cell);
            }
        }

        [Fact]
        public void CalculateNextGeneration_AliveCellsWithMoreThan3AliveNeighboursBecomeDead()
        {
            var dc = GameOfLifeProcessor.DeadCell;
            var ac = GameOfLifeProcessor.AliveCell;
            var cellsGrid = new string[3, 3]
            {
                { dc, dc, dc },
                { ac, ac, ac },
                { dc, ac, dc }
            };

            string[,] result = GameOfLifeProcessor.CalculateNextGeneration(cellsGrid);


            for (int x = 0; x < cellsGrid.GetLength(0); x++)
            {
                for (int y = 0; y < cellsGrid.GetLength(1); y++)
                {
                    if ((x == 1 && y == 0) || (x == 1 && y == 2) || (x == 2 && y == 1))
                    {
                        Assert.Equal(ac, cellsGrid[x, y]);
                    }
                    else
                    {
                        Assert.Equal(dc, cellsGrid[x, y]);
                    }
                }
            }
        }
    }
}