namespace MazeWalker
{
    class Program
    {
        static void Main(string[] args)
        {
            var coords =
                @"
    
    
   X";

            Maze maze = new Maze(coords);
            var q = new ReinforcementLearning().Train(maze);
            new Walker().Walk(maze, q);
        }
    }
}