using System;

namespace MazeWalker
{
    internal class Walker
    {
        public Walker()
        {
        }

        internal void Walk(Maze m, QMatrix q)
        {
            WalkSteps(m, q, Tuple.Create(0, 0));
            WalkSteps(m, q, Tuple.Create(2, 2));
            WalkSteps(m, q, Tuple.Create(3, 0));
        }

        private static void WalkSteps(Maze m, QMatrix q, Tuple<int, int> current)
        {
            Console.WriteLine();
            Console.WriteLine("walking...." + current);

            while (!m.IsEndState(current))
            {
                Console.Write(current + " -> ");
                var best = q.GetBestMove(current);
                current = best.To;
            }
            Console.WriteLine(current);
        }
    }
}