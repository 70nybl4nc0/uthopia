using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Uthopia {
    public abstract class GameController : MonoBehaviour
    {
        abstract public void Initialize(int seed, GameSettings settings);
        abstract public void OnInputReceived(Input input);
        abstract public void Dispose();
    }

    public class GameSettings {
        public Vector2Int resolution;
    }
}

