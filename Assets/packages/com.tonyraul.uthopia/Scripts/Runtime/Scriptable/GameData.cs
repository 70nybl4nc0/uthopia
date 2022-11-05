using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName =  "Game", menuName = "Uthopia/New Game")]
public class GameData : ScriptableObject
{
    public string gameName;
    public string gameDescription;
    public string scenePath = "/Scenes/Game";
}
