using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractObject : MonoBehaviour
{
    [SerializeField] GameObject rootParent;
    Rigidbody rigidbody;    
    bool isGrab = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public bool IsGrab()
    {
        return isGrab;
    }

    public void SetGrab(Transform _parent)
    {
        isGrab = true;
        rigidbody.isKinematic = true;
        rootParent.transform.SetParent(_parent);
        rootParent.transform.rotation = Quaternion.identity;
        rootParent.transform.localPosition = Vector3.zero;
    }

    public void SetFreeObject()
    {
        isGrab = false;
        rigidbody.isKinematic = false;
        rootParent.transform.SetParent(null);
    }

    public void PullObject(Vector3 _velocity)
    {
        rigidbody.velocity = _velocity;
    }
}
