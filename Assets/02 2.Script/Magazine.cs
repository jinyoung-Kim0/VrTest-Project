using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    private const int MAX_BULLET_COUNT = 9;
    private Stack<Bullet> magazineStack;
    private Vector3 movePivot = new Vector3(0, 0.01f, 0.0015f);

    [SerializeField] private GameObject magazineParent;
    [SerializeField] private GameObject magazineCenter;
    [SerializeField] private Rigidbody rigidbody;

    private void Start()
    {
        magazineStack = new Stack<Bullet>();
    }
    public void SetMagazine(GameObject _parent=null)
    {
        if(_parent)
        {
            rigidbody.isKinematic = true;
            transform.SetParent(_parent.transform);
            transform.localRotation = Quaternion.identity;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.SetParent(null);
            rigidbody.isKinematic = false;
        }
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// If the number of bullets is 0, return true
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {   
        return magazineStack.Count == 0 ? true : false;
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// put a bullet in a magazine
    /// 
    /// </summary>
    /// <param name="_bullet"></param>
    private void SetBullet(Bullet _bullet)
    {
        _bullet.gameObject.transform.SetParent(magazineParent.transform);
        if (magazineStack.Count == 0)
        {   
            _bullet.transform.localPosition = Vector3.zero;
        }
        else
        {
            Bullet topBullet = magazineStack.Peek();
            Vector3 nextPos = topBullet.transform.localPosition + movePivot;
            _bullet.transform.localPosition = nextPos;
            
        }
        _bullet.transform.localRotation = Quaternion.identity;

        magazineStack.Push(_bullet);
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// to charge as many bullets as one wants [Defalut = Max]
    /// </summary>
    public void ChargeBullet(int _count = MAX_BULLET_COUNT)
    {
        magazineCenter.transform.localPosition = Vector3.zero;
        int magazineMoveCount = MAX_BULLET_COUNT - _count;

        while(magazineStack.Count < _count)
        {
            Bullet newbullet = GameManager.instance.GetBullet();
            SetBullet(newbullet);
        }

        while(magazineMoveCount != 0)
        {
            magazineCenter.transform.localPosition += movePivot;
        }
        Debug.Log("Magazine :: FullReload => Full Reloaded!.");
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Use Bullet and Move to the designated location 
    /// </summary>
    /// <returns></returns>
    public Bullet FireBullet()
    {
        if (magazineStack.Count == 0)
        {
            Debug.Log("Magazine :: FireBullet 'We have no Bullet!' ");
            return null;
        }
        Bullet fireBullet = magazineStack.Pop();
        fireBullet.transform.SetParent(null);
        magazineCenter.transform.localPosition += movePivot;
        return fireBullet;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ChargeBullet();
        }
    }
}
