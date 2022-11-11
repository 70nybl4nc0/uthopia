using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using uthopia = Uthopia;

namespace UthopiaEnv
{
    public class AgentManager : Agent
    {
        public override void Initialize()
        {
            base.Initialize();
            uthopia.GameController.instance.Initialize(0);
            uthopia.GameController.instance.onWin.AddListener(
                () =>
                {
                    AddReward(1);
                    EndEpisode();
                }
                );
            uthopia.GameController.instance.onLose.AddListener(
                () =>
                {
                    AddReward(-1);
                    EndEpisode();
                }
                );
        }


        public override void OnEpisodeBegin()
        {
            base.OnEpisodeBegin();
            uthopia.GameController.instance.OnEpisodeBegin();
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            print(actions.DiscreteActions.Array.Aggregate("", (a, n) => $"{a} {n}"));
            uthopia.GameController.instance.OnInputReceived(new uthopia.InputData(actions.DiscreteActions.Array));
            base.OnActionReceived(actions);
        }


        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var discrete = actionsOut.DiscreteActions;

            discrete[0] = (Input.GetKey(KeyCode.A) ? -1 : 0)
                + (Input.GetKey(KeyCode.D) ? 1 : 0);

            discrete[1] = (Input.GetKey(KeyCode.S) ? -1 : 0)
                + (Input.GetKey(KeyCode.W) ? 1 : 0);

            discrete[2] = (Input.GetKey(KeyCode.O) ? 1 : 0);

        }

    }
}