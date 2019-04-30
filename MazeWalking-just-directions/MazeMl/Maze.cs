using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeWalker
{
    class Maze
    {
        State[] cache;

        private readonly string Coords;
        readonly Random rnd = new Random();
        private readonly string[] rows;

        public Maze(string maze)
        {
            Coords = maze;
            rows = Coords.Split(new[] {"\r\n"}, StringSplitOptions.None).Skip(1).ToArray();
        }

        public State[] States => cache ?? (cache = Problem().ToArray());

        public State GetStateFromAction(State state, Actions action)
        {
            switch (action)
            {
                case Actions.West:
                    return States.First(x => x.X == state.X - 1 && x.Y == state.Y);
                case Actions.East:
                    return States.First(x => x.X == state.X + 1 && x.Y == state.Y);
                case Actions.North:
                    return States.First(x => x.X == state.X && x.Y == state.Y - 1);
                case Actions.South:
                    return States.First(x => x.X == state.X && x.Y == state.Y + 1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public bool IsEndState(Tuple<int, int> x)
        {
            return rows[x.Item2][x.Item1] == 'X';
        }

        public State GetRandomState()
        {
            return States[rnd.Next(0, States.Length)];
        }

        IEnumerable<State> Problem()
        {
            var maxX = rows.First().Length;
            var maxY = rows.Length;

            for (var y = 0; y < maxY; y++)
            for (var x = 0; x < maxX; x++)
            {
                Reward r = rows[y][x] == 'X' ? 9.99f : -0.1f;

                var actions = new List<Actions>();
                if (y > 0) actions.Add(Actions.North);
                if (x > 0) actions.Add(Actions.West);
                if (y < maxY - 1) actions.Add(Actions.South);
                if (x < maxX - 1) actions.Add(Actions.East);

                yield return new State(x, y, actions.ToArray(), r);
            }
        }

        public void PrintMaze(State current)
        {
            foreach (var state in States)
            {
                var c = FindCharToPrint(state, current);
                Console.SetCursorPosition(state.X, state.Y);
                Console.Write(c);
            }
        }

        private char FindCharToPrint(State printing, State current)
        {
            var printingCoord = printing.ToCoord();

            if (IsEndState(printingCoord))
                return 'X';

            if (Equals(printingCoord, current.ToCoord()))
                return '*';

            return '.';
        }

        public void PrintWithRewards()
        {
            var last = -1;
            foreach (var d in States)
            {
                if (d.X != last)
                {
                    last = d.X;
                    Console.WriteLine();
                }

                Console.Write($"{d.X},{d.Y} ({d.Actions.Length}) r: {d.Reward.Value}   ");
            }

            Console.WriteLine();
        }
    }
}