using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    [SerializeField] private Gun gun;

    #region ButtonAction

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// If you have a gun, you can shoot
    /// </summary>
    public void Fire()
    {
        if(gun == null)
        {
            return;
        }
        gun.Fire();
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// If you have a gun, you can load it
    /// </summary>
    public void Reload()
    {
        if(gun == null)
        {
            return;
        }
        gun.ReLoad();
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// If you have a gun, you can throw away the magazine
    /// </summary>
    public void RemoveMagazine()
    {
        if(gun == null)
        {
            return;
        }
        gun.RemoveMagazine();
    }

    #endregion


}
