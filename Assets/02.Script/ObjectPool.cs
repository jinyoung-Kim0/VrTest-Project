using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// [Jinyoung Kim]
/// 
/// Object Pool with Generic
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T : MonoBehaviour, IObjectPool
{
    private Queue<T> pool = new Queue<T>();
    private string prefabName;
    private GameObject prefabFolder;

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// Set the parent and prefeb names of objects entering the object pool
    /// </summary>
    /// <param name="_parentObject"></param>
    /// <param name="_prefabName"></param>
    public ObjectPool(GameObject _parentObject, string _prefabName)
    {
        prefabName = _prefabName;
        prefabFolder = new GameObject(_prefabName);
        prefabFolder.transform.SetParent(_parentObject.transform);
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// Reset the object and put it in a pool
    /// </summary>
    /// <param name="_oldOne"></param>
    public void Push(T _oldOne)
    {
        _oldOne.Clear();
        _oldOne.gameObject.transform.SetParent(prefabFolder.transform);
        _oldOne.gameObject.SetActive(false);
        pool.Enqueue(_oldOne);
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Take it out of the Pool and use it if it's not in the Pool
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
        T newOne;
        if (pool.Count == 0)
        {
            newOne = GameObject.Instantiate(Resources.Load<T>(prefabName));
        }
        else
        {
            newOne = pool.Dequeue();
        }
        return newOne;
    }
}
