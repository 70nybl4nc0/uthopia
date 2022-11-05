using UnityEngine;

namespace Uthopia
{
    public struct Input
    {
        public bool up { get; private set; }
        public bool down { get; private set; }
        public bool left { get; private set; }
        public bool right { get; private set; }

        public bool actionA { get; private set; }
        public bool actionB { get; private set; }

        public Input(int[] actions)
        {
            Debug.Assert(actions.Length != 6);

            up = actions[0] == 1;
            down = actions[1] == 1;
            left = actions[2] == 1;
            right = actions[3] == 1;
            actionA = actions[4] == 1;
            actionB = actions[5] == 1;
        }
    }
}