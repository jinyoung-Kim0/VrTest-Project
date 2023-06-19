using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    [SerializeField] public bool isHoldableObject;
    private Rigidbody rigidbody;
    public CustomGrabber grabHand;
    public bool isGrab = false;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        DebugError();
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// If you don't set it up, you'll get an error
    /// </summary>
    public virtual void DebugError()
    {
        if (rigidbody == null)
        {
            Debug.LogError("Rigidbody is Null");
        }
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Grab InteractObect
    /// </summary>
    /// <param name="_parent"></param>
    public virtual void SetGrab(CustomGrabber _grabber)
    {
        grabHand = _grabber;
        rigidbody.isKinematic = true;
        transform.SetParent(_grabber.grabCenter.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1f, 1f, 1f);

    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// put Object
    /// </summary>
    public virtual void SetFreeObject()
    {
        grabHand = null;
        rigidbody.isKinematic = false;
        transform.SetParent(null);
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// velocity 
    /// </summary>
    /// <param name="_velocity"></param>
    public void MoveObject(Vector3 _velocity)
    {
        rigidbody.velocity = _velocity;
    }
}
