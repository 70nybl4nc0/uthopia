using UnityEngine;

namespace Uthopia
{
    public struct InputData
    {
        public int moveX { get; private set; }
        public int moveY { get; private set; }
        public bool action1 { get; private set; }
        public bool action2 { get; private set; }
        public bool action3 { get; private set; }
        public bool action4 { get; private set; }

        public Vector3 direction => moveX * Vector3.right + moveY * Vector3.up;

        public InputData(int[] actions)
        {
            Debug.Assert(actions.Length == 6);


            switch (actions[0])
            {
                case 1:
                    moveX = -1;
                    break;
                case 2:
                    moveX = 1;
                    break;
                default:
                    moveX = 0;
                    break;
            }

            switch (actions[1])
            {

                case 1:
                    moveY = -1;
                    break;
                case 2:
                    moveY = 1;
                    break;
                default:
                    moveY = 0;
                    break;
            }

            action1 = actions[2] == 1;
            action2 = actions[3] == 1;
            action3 = actions[4] == 1;
            action4 = actions[5] == 1;
        }

        
    }


    public struct InputActionMask
    {

        public bool action1;
        public bool action2;
        public bool action3;
        public bool action4;
        public bool action5;
        public bool action6;
        public bool action7;
        public bool action8;

        public InputActionMask(
            bool action1 = true,
            bool action2 = true,
            bool action3 = true,
            bool action4 = true,
            bool action5 = true,
            bool action6 = true,
            bool action7 = true,
            bool action8 = true)
        {
            this.action1 = action1;
            this.action2 = action2;
            this.action3 = action3;
            this.action4 = action4;
            this.action5 = action5;
            this.action6 = action6;
            this.action7 = action7;
            this.action8 = action8;
        }

    }

}