using System;

namespace MazeWalker
{
    class FromTo
    {
        public readonly Tuple<int, int> From, To;

        public FromTo(Tuple<int, int> from, Tuple<int, int> to)
        {
            From = from;
            To = to;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FromTo);
        }

        private bool Equals(FromTo y)
        {
            if (ReferenceEquals(this, y)) return true;
            if (ReferenceEquals(y, null)) return false;
            return Equals(From, y.From) && Equals(To, y.To);
        }

        public override int GetHashCode()
        {
            return ((From != null ? From.GetHashCode() : 0) * 397) ^ (To != null ? To.GetHashCode() : 0);
        }
    }
}