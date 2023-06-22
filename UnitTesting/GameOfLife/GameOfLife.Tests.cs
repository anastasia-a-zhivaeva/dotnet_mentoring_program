namespace GameOfLife
{
    public class GameOfLifeTests
    {
        [Fact]
        public void CalculateNextGeneration_AllDeadCellsRemainDead()
        {
            var cellsGrid = new string[3, 3] 
            { 
                { ".", ".", "." }, 
                { ".", ".", "." }, 
                { ".", ".", "." } 
            };

            string[,] result = GameOfLifeProcessor.CalculateNextGeneration(cellsGrid);


            foreach(var cell in result)
            {
                Assert.Equal(".", cell);
            }
        }
    }
}