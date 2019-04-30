using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeWalker
{
    class QMatrix
    {
        public readonly Dictionary<FromTo, double> Matrix = new Dictionary<FromTo, double>();

        public QMatrix(Maze m)
        {
            foreach (var from in m.States)
            foreach (var to in m.States)
                Matrix.Add(new FromTo(from.ToCoord(), to.ToCoord()), 0.0d);
        }

        public double Get(Tuple<int, int> from, Tuple<int, int> to)
        {
            var f = new FromTo(from, to);
            try
            {
                return Matrix[f];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "--> " + from + "," + to, e);
            }
        }

        public FromTo GetBestMove(Tuple<int,int> from)
        {
            var best = Matrix.Where(x => x.Key.From.Equals(from)).OrderByDescending(x=>x.Value).Take(1).Single();
            return best.Key;
        }

        public void Print()
        {
            Console.WriteLine();
            Console.WriteLine("Q-matrix");

            var legend = string.Join("  ", Matrix.Keys
                .Select(x => x.From)
                .Distinct()
                .Select(x => $"({x.Item1},{x.Item2})"));

            Console.WriteLine($"   |   {legend}");
            Console.Write("---+--------------------------------------------------------------------------------------");

            var last = -1;
            foreach (var d in Matrix)
            {
                if (d.Key.From.Item1 != last)
                {
                    last = d.Key.From.Item1;
                    Console.WriteLine();
                    Console.Write($"{d.Key.From.Item1},{d.Key.From.Item2}| ");
                }

                Console.Write($"   {d.Value:N2}");
            }
        }
    }
}