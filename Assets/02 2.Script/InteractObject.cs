using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void SetOfftest(bool _isinteract)
    {
        rigidBody.isKinematic = _isinteract;
    }
    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// pull an object    List: PathPosList
    /// </summary>
    /// <param name="_pathList"></param>
    public void PullObject(Vector3 _targetPos, float _angle)
    {
        rigidBody.velocity = GetVelocity(transform.position, _targetPos, _angle);
    }
    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Projectile Motion
    /// </summary>
    /// <param name="currentPos"></param>
    /// <param name="targetPos"></param>
    /// <param name="initialAngle"></param>
    /// <returns></returns>
    Vector3 GetVelocity(Vector3 currentPos, Vector3 targetPos, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(targetPos.x, 0, targetPos.z);
        Vector3 planarPosition = new Vector3(currentPos.x, 0, currentPos.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = currentPos.y - targetPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (targetPos.x > currentPos.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }
}
