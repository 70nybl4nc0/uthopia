using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
//using Uthopia;

namespace UthopiaEnv
{
    public class ManagerAgent : Agent
    {

        readonly Dictionary<KeyCode, int> m_keyToAction = new Dictionary<KeyCode, int>() {
            { KeyCode.A, 0 },
            { KeyCode.S, 1 },
            { KeyCode.D, 2 },
            { KeyCode.W, 3 },
            { KeyCode.I, 4 },
            { KeyCode.O, 5 },
        };


        public override void OnEpisodeBegin()
        {
            base.OnEpisodeBegin();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            print(actions.DiscreteActions.Array.Aggregate("", (a, n) => $"{a} {n}"));
            base.OnActionReceived(actions);
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var discrete = actionsOut.DiscreteActions;

            foreach (var key in m_keyToAction) {
                if (Input.GetKey(key.Key))
                    discrete[key.Value] = 1;
                else
                    discrete[key.Value] = 0;
            }
        }

    }
}