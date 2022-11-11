using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uthopia;
using UnityEngine.SceneManagement;



public class UthopiaManager : MonoBehaviour
{
    public GameController currentController;

    IEnumerator<GameVariant> trainingGames;
    IEnumerator<GameVariant> testGames;

    private void StartTraining()
    {
        trainingGames = UthopiaPipelineSettings.instance.trainingGames.GetEnumerator();
    }



    void NextTrainigGame() {

        if (!trainingGames.MoveNext())
        {
            EndTraining();
            return;
        }

        SceneManager.LoadScene(trainingGames.Current.sceneName, LoadSceneMode.Additive);

    }




    void EndTraining()
    {

    }


}
