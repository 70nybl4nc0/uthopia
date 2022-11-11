using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Uthopia Pipeline", menuName = "Uthopia/Create Pipeline")]
public class UthopiaPipelineSettings : SingletonScriptableObject<UthopiaPipelineSettings>
{
    public int seed;
    public List<GameVariant> trainingGames;
    public List<GameVariant> testGames;
}


