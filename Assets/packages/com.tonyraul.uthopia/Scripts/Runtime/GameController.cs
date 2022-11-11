using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Uthopia
{
    public abstract class GameController : MonoBehaviour
    {
        private static GameController _instance;
        public static GameController instance {
            get {
                if (!_instance)
                {
                    _instance =  FindObjectOfType<GameController>(true);
                }
                return _instance;
            }
        }

        public GameSettings settings { private set; get; }
        public int seed { private set; get; }

        public UnityEvent onWin;
        public UnityEvent onLose;

        public virtual void Initialize(int seed, GameSettings settings = null)
        {
            onWin.RemoveAllListeners();
            onLose.RemoveAllListeners();
            this.settings = settings;
            this.seed = seed;
        }

        public virtual void OnEpisodeBegin() {
            Camera.main.backgroundColor = Color.black;
        }

        abstract public void OnInputReceived(InputData input);

        protected void Win()
        {
            onWin.Invoke();
            Camera.main.backgroundColor = Color.green;
        }
        protected void Lose()
        {
            onLose.Invoke();
            Camera.main.backgroundColor = Color.red;
        }


        
    }

    public class GameSettings
    {
        public Vector2Int resolution;
    }
}

