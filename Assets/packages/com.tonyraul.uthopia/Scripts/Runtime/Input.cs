using UnityEngine;

namespace Uthopia
{
    public struct InputData
    {
        public int moveX { get; private set; }
        public int moveY { get; private set; }
        public bool action { get; private set; }

        public Vector3 direction => moveX * Vector3.right + moveY * Vector3.up;

        public InputData(int[] actions)
        {
            Debug.Assert(actions.Length == 6);

            moveX = actions[0];
            moveY = actions[1];

            action = actions[4] == 1;
        }
    }
}