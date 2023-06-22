namespace GameOfLife
{
    internal class GameOfLifeProcessor
    {
        private static readonly int[,] _neighbours = new int[8, 2]
        {
            { 0, 1 },
            { 1, 0 },
            { 0, -1 },
            { -1, 0 },
            { 1, 1 },
            { -1, -1 },
            { -1, 1 },
            { 1, -1 },
        };
        internal static string[,] CalculateNextGeneration(string[,] cellsGrid)
        {
            var nextGen = (string[,])cellsGrid.Clone();
            for (int x = 0; x < cellsGrid.GetLength(0); x++)
            {
                for (int y = 0; y < cellsGrid.GetLength(1); y++)
                {
                    if (cellsGrid[x, y] == ".")
                    {
                        var lifeNeighbours = 0;

                        for (int z = 0; z < _neighbours.GetLength(0); z++)
                        {
                            try
                            {
                                var firstIndex = _neighbours[z, 0];
                                var secondIndex = _neighbours[z, 1];
                                if (cellsGrid[firstIndex, secondIndex] == "*")
                                {
                                    lifeNeighbours++;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                // neighbour doesn't exist in this place
                            }
                        }

                        if (lifeNeighbours > 3)
                        {
                            nextGen[x, y] = "*";
                        }
                        else
                        {
                            nextGen[x, y] = ".";
                        }
                    }
                    else
                    {
                        nextGen[x, y] = cellsGrid[x, y];
                    }
                }
            }

            return nextGen;
        }
    }
}