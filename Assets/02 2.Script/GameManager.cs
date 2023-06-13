using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Character character;
    private Stack<Bullet> bulletPool;
    private GameObject bulletParents;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        bulletPool = new Stack<Bullet>();
        bulletParents = new GameObject("BulletPool");
        bulletParents.transform.SetParent(transform);
    }


    #region [Bullet] ObjectPool
    /// <summary>
    /// 
    /// [Jinyoung Kim] 
    /// 
    /// Push the Bullet in the BulletPool
    /// 
    /// </summary>
    /// <param name="_bullet"></param>
    public void PushPoolBullet(Bullet _bullet)
    {
        _bullet.transform.SetParent(bulletParents.transform);
        _bullet.gameObject.SetActive(false);
        bulletPool.Push(_bullet);
    }
    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Take from BulletPool
    /// 
    /// </summary>
    /// <param name="_bullet"></param>
    public Bullet GetBullet()
    {
        Bullet bullet;
        if (bulletPool.Count == 0)
        {
            Bullet prefab = Resources.Load<Bullet>("Bullet");
            bullet = Instantiate(prefab);
        }
        else
        {
            bullet = bulletPool.Pop();
            bullet.gameObject.SetActive(true);
        }
        bullet.transform.localScale = new Vector3(1f, 1f, 1f);
        return bullet;
    }
    #endregion

    private void Start()
    {
        //InputManager.instance.SettingCharacter(character);
    }
}
