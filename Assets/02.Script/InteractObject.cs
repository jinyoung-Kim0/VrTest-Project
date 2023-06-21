using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public enum InteractObjectState
    {
        Grab,
        OtherAction,
        FREE = 0
    }
    private Rigidbody rigidbody;
    public InteractObjectState state;
    public LeftHand currentHand;

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
    public void SetGrab(LeftHand _grabber)
    {
        rigidbody.isKinematic = true;
        currentHand = _grabber;
        transform.SetParent(_grabber.grabCenter.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Interaction while grabbing
    /// </summary>
    public void MoveGrabObject()
    {
        currentHand.MoveGrabObject();
        currentHand = null;
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// put Object
    /// </summary>
    public void SetFreeObject()
    {
        rigidbody.isKinematic = false;
        state = InteractObjectState.FREE;
        transform.SetParent(null);
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// Add velocity 
    /// </summary>
    /// <param name="_velocity"></param>
    public void MoveObject(Vector3 _velocity)
    {
        rigidbody.velocity = _velocity;
    }

}
