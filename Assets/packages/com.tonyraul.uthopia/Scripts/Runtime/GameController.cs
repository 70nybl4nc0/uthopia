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




        public abstract void OnEpisodeBegin();
        abstract public void OnInputReceived(InputData input);

        protected void Win()
        {
            Camera.main.backgroundColor = Color.green;
            Invoke(nameof(RestartCamera), 0.1f);
            onWin.Invoke();
        }
        protected void Lose()
        {
            Camera.main.backgroundColor = Color.red;
            Invoke(nameof(RestartCamera), 0.1f);
            onLose.Invoke();
        }


        void RestartCamera() {
            Camera.main.backgroundColor = Color.black;
        }


        
    }

    public class GameSettings
    {
        public Vector2Int resolution;
    }


    public static class Vector2X {
        public static Vector2 InsideSquareBounds(this Vector3 vector, float dimention) {
            return new Vector2(Mathf.Clamp(vector.x, -dimention, dimention), Mathf.Clamp(vector.y, -dimention, dimention));
        }


    }
}

