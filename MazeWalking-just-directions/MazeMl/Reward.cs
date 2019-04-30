namespace MazeWalker
{
    class Reward
    {
        public readonly float Value;

        public Reward(float reward)
        {
            Value = reward;
        }

        public static implicit operator Reward(float d)
        {
            return new Reward(d);
        }
    }
}