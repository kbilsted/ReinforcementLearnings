using System;
using System.Linq;
using System.Threading;

namespace MazeWalker
{
    class ReinforcementLearning
    {
        public QMatrix Train(Maze m)
        {
            m.PrintWithRewards();

            var q = new QMatrix(m);
            Train(m, q);

            return q;
        }

        public void Train(Maze m, QMatrix q)
        {
            q.Print();

            Learn(q, m, 75);

            q.Print();
        }

        private void Learn(QMatrix qMatrix, Maze m, int maxEpoc)
        {
            Console.WriteLine();
            Console.WriteLine("learning...");

            for (var epoc = 0; epoc < maxEpoc; epoc++)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 4);
                Console.WriteLine($"Epoc {epoc} /{maxEpoc} ");

                qMatrix.Print();

                LearnStep(qMatrix, m, epoc % 10 == 0);
            }
        }

        private void LearnStep(QMatrix qMatrix, Maze m, bool printStep)
        {
            var gamma = 0.5;
            var learnRate = 0.5d;

            var currentState = m.GetRandomState();

            while (!m.IsEndState(currentState.ToCoord()))
            {
                if(printStep)
                    m.PrintMaze(currentState);

                if (currentState.Actions.Length == 0)
                    break;

                var action = currentState.GetRandomAction();
                var nextState = m.GetStateFromAction(currentState, action);
                var actionsInNextState = nextState.Actions;
                var nextStateAsCoord = nextState.ToCoord();

                var maxQ = actionsInNextState
                    .Select(act => m.GetStateFromAction(nextState, act))
                    .Select(state => state.ToCoord())
                    .Select(co => qMatrix.Get(nextStateAsCoord, co))
                    .Max();

                var transition = new FromTo(currentState.ToCoord(), nextStateAsCoord);
                var exploitComponent = (1 - learnRate) * qMatrix.Matrix[transition];
                var exploreComponent = learnRate * (nextState.Reward.Value + gamma * maxQ);
                qMatrix.Matrix[transition] = exploitComponent + exploreComponent;
                currentState = nextState;

                if(printStep)
                    Thread.Sleep(30);
            }
        }
    }
}