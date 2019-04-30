using System;

namespace MazeWalker
{
    class State
    {
        static readonly Random rnd = new Random();
        public Actions[] Actions;
        public Reward Reward;

        public int X;
        public int Y;

        public State(int x, int y, Actions[] actions, Reward reward)
        {
            X = x;
            Y = y;
            Actions = actions;
            Reward = reward;
        }

        public Tuple<int, int> ToCoord()
        {
            return Tuple.Create(X, Y);
        }

        public Actions GetRandomAction()
        {
            return Actions[rnd.Next(0, Actions.Length)];
        }
    }
}