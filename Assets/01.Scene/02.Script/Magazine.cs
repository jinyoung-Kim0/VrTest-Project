using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour 
{
    private const int MAX_BULLET_COUNT = 9;
    private InteractObject interactObject;
    private Stack<Bullet> magazineStack;
    private Vector3 movePivot = new Vector3(0, 0.01f, 0.0015f);

    [SerializeField] private GameObject magazineParent;
    [SerializeField] private GameObject magazineCenter;
    
    
    private void Start()
    {
        magazineStack = new Stack<Bullet>();
        interactObject = GetComponent<InteractObject>();
        
    }
    public void FreeMagazine()
    {
        interactObject.enabled = true;
        interactObject.SetFreeObject();
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

        while (magazineStack.Count < _count)
        {
            Bullet newbullet = GameManager.instance.GetBullet();
            SetBullet(newbullet);
        }

        while (magazineMoveCount != 0)
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Gun" )
        {   
            interactObject.grabHand.grabObject = null;
            Gun gun = other.GetComponent<Gun>();
            gun.SetMagazine(this);
        }
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChargeBullet();
        }
    }
}
