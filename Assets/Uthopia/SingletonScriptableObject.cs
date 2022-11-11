using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (!_instance)
            {

                var results = Resources.FindObjectsOfTypeAll<T>();
                Debug.Assert(results.Length != 0, $"There is no instance of {typeof(T).Name}");
                Debug.Assert(results.Length < 2, $"There is more than one instance of {typeof(T).Name}");

                _instance = results[0];
            }

            return _instance;
        }
    }
}
