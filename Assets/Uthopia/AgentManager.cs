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
            uthopia.Game.instance.Initialize(0);
            uthopia.Game.instance.onWin.AddListener(
                () =>
                {
                    AddReward(1);
                    EndEpisode();
                }
                );
            uthopia.Game.instance.onLose.AddListener(
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
            uthopia.Game.instance.OnEpisodeBegin();
        }


        public override void OnActionReceived(ActionBuffers actions)
        {
            uthopia.Game.instance.OnInputReceived(new uthopia.InputData(actions.DiscreteActions.Array));
            base.OnActionReceived(actions);
        }

        public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
        {
            var mask = uthopia.Game.instance.GetInputMask();

            actionMask.SetActionEnabled(0, 1, mask.action1);
            actionMask.SetActionEnabled(0, 2, mask.action2);

            actionMask.SetActionEnabled(1, 1, mask.action3);
            actionMask.SetActionEnabled(1, 2, mask.action4);

            actionMask.SetActionEnabled(2, 1, mask.action5);
            actionMask.SetActionEnabled(3, 1, mask.action6);
            actionMask.SetActionEnabled(4, 1, mask.action7);
            actionMask.SetActionEnabled(5, 1, mask.action8);

            base.WriteDiscreteActionMask(actionMask);
        }


        public override void Heuristic(in ActionBuffers actionsOut)
        {
            // DIRECTIONAL
            var discrete = actionsOut.DiscreteActions;

            if (Input.GetKey(KeyCode.A) != Input.GetKey(KeyCode.D))
            {
                discrete[0] = Input.GetKey(KeyCode.A) ? 1 : 2;
            }
            else discrete[0] = 0;

            if (Input.GetKey(KeyCode.W) != Input.GetKey(KeyCode.S))
            {
                discrete[1] = Input.GetKey(KeyCode.S) ? 1 : 2;
            }
            else discrete[1] = 0;

            // EX ACTIONS
            discrete[2] = (Input.GetKey(KeyCode.U) ? 1 : 0);
            discrete[3] = (Input.GetKey(KeyCode.I) ? 1 : 0);
            discrete[4] = (Input.GetKey(KeyCode.O) ? 1 : 0);
            discrete[5] = (Input.GetKey(KeyCode.P) ? 1 : 0);
        }
    }
}