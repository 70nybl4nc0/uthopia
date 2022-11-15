using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UthopiaEnv;

public class StartTrigger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<AgentManager>().enabled = true;
        }
    }
}
