using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] GameObject target;

    bool isMove = false;
    public void PullObject()
    {
        if (isMove)
            return;
        isMove = true;
        StartCoroutine(CoPullObject(target.transform.position));
    }
    
    private IEnumerator CoPullObject(Vector3 _playerPos)
    {
        rigidBody.isKinematic = false;
        Vector3 Point1 = transform.position;
        Vector3 Point2 = transform.position + Vector3.up*5;
        Vector3 Point3 = _playerPos + Vector3.up * 5;
        Vector3 Point4 = _playerPos;
        float progress = 0f;

        Vector3 bezierPoint;
        Vector3 direction;
        Vector3 force;
        while(progress <=1)
        {
            yield return null;
            progress += Time.deltaTime*0.2f;
            bezierPoint = BezierMath.BezierPoint(Point1, Point2, Point3, Point4, progress);
            rigidBody.MovePosition(bezierPoint);
        }
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;
        isMove = false;
    }

}
