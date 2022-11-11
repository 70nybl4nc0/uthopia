using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Info", menuName = "Uthopia/Game Info")]
public class GameInfo : ScriptableObject
{
    public new string name;
    public List<GameVariant> variants;
}
