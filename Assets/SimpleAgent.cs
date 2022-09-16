using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class SimpleAgent : Agent
{

    public override void OnActionReceived(ActionBuffers actions)
    {
        print(actions);
        base.OnActionReceived(actions);
    }
}
