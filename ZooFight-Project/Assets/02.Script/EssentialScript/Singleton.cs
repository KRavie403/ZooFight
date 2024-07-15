using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _inst = null;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType<T>();
                if (_inst == null)
                {
                    GameObject obj = new GameObject(typeof(T).ToString());
                    _inst = obj.AddComponent<T>();
                }
            }
            return _inst;
        }
    }

    protected virtual void Awake()
    {
        if (_inst == null)
        {
            _inst = this as T;
            DontDestroyOnLoadOnRoot(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void DontDestroyOnLoadOnRoot(GameObject root)
    {
        if (root.transform.parent != null)
        {
            root.transform.SetParent(null);
        }
        DontDestroyOnLoad(root);
    }
}
