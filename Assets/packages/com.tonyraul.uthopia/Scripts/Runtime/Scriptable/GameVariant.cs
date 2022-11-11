using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName =  "Game Variant", menuName = "Uthopia/New Game Variant")]
public class GameVariant : ScriptableObject
{
    public new string name;
    public string description;
    public string sceneName;
}
