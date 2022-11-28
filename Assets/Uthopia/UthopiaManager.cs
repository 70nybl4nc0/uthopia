using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uthopia;
using UnityEngine.SceneManagement;
using UthopiaEnv;



public class UthopiaManager : MonoBehaviour
{
    public Game currentController;

    IEnumerator<GameVariant> games;

    private void StartTraining()
    {
        games = UthopiaPipelineSettings.instance.games.GetEnumerator();
    }



    void NextTrainigGame() {

        if (!games.MoveNext())
        {
            EndTraining();
            return;
        }

        SceneManager.LoadScene(games.Current.sceneName, LoadSceneMode.Additive);

    }




    void EndTraining()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FindObjectOfType<AgentManager>().enabled = true;
        }
    }


}
