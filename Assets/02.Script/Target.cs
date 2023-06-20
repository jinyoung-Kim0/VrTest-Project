using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IObjectPool
{

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// Back to the original state
    /// </summary>
    public void Clear() { }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// If you get hit, you'll go back to Pool
    /// </summary>
    public void Hit()
    {
        GameManager.instance.GetScore(this);
    }
}
