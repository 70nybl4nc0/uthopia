using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Uthopia Pipeline", menuName = "Uthopia/Create Pipeline")]
public class UthopiaPipelineSettings : ScriptableObject
{
    public int seed;
    public List<GameData> games;
}


