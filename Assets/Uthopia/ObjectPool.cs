using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<ObjectPool>(true);
            }
            return _instance;
        }
    }

    Dictionary<string, List<GameObject>> m_pool = new Dictionary<string, List<GameObject>>();


    public static T Get<T>(string id, T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
    {
        var pool = instance.m_pool;
        if (!pool.ContainsKey(id))
        {
            pool[id] = new List<GameObject>();
        }

        if (pool[id].Count == 0)
        {
            pool[id].Add(Instantiate<T>(prefab, position, rotation, parent).gameObject);
        }

        var obj = pool[id][0].GetComponent<T>();
        obj.gameObject.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        pool[id].RemoveAt(0);

        return obj;
    }

    public static void Set(string id, GameObject obj)
    {
        var pool = instance.m_pool;
        if (!pool.ContainsKey(id))
        {
            pool[id] = new List<GameObject>();
        }

        obj.transform.parent = null;
        obj.SetActive(false);
        pool[id].Add(obj);

    }


}
