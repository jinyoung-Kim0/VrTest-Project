using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    private bool isGrab;
    private Rigidbody rigidbody;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        isGrab = false;
    }
    public bool IsGrab()
    {
        return isGrab;
    }

    public void SetGrab(Transform _parent)
    {
        isGrab = true;
        rigidbody.isKinematic = true;
        transform.SetParent(_parent);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void SetFreeObject()
    {
        isGrab = false;
        rigidbody.isKinematic = false;
        transform.SetParent(null);
    }

    public void MoveObject(Vector3 _velocity)
    {
        rigidbody.velocity = _velocity;
    }
}
