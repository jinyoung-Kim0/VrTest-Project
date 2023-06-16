using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeftHand : MonoBehaviour
{
    [SerializeField] private GameObject lineStartPosition;

    private LineRenderer laser;
    private Material material;
    private const int CURVE_POINT_COUNT = 20;
    private const int LINE_POINT_COUNT = 2;
    private const float LINE_LENGTH = 5f;
    private Collider target;
    private bool pullSwitch;
    private Quaternion touchStartQuaternion;

    void Start()
    {
        LaserInitialize();
    }

    #region Laser

    /// <summary>
    /// 
    /// [Jinyoung Kim] 
    /// 
    /// Layser Initialize
    /// </summary>
    private void LaserInitialize()
    {
        laser = this.gameObject.AddComponent<LineRenderer>();
        material = new Material(Shader.Find("Standard"));
        laser.material = material;
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
        LaserReset();
        pullSwitch = false;
        laser.enabled = false;
        
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// LaserInfo reset
    /// </summary>
    private void LaserReset()
    {
        target = null;
        pullSwitch = false;
        material.color = Color.blue;
        laser.positionCount = LINE_POINT_COUNT;
        laser.enabled = false;
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Set in a straight line
    /// </summary>
    private void SetStraightLine()
    {   
        laser.positionCount = LINE_POINT_COUNT;
        laser.SetPosition(0, lineStartPosition.transform.position);
        Vector3 targetPos = target != null ? target.transform.position : lineStartPosition.transform.position + lineStartPosition.transform.forward* LINE_LENGTH;
        laser.SetPosition(1, targetPos);
        laser.enabled = true;
        pullSwitch = false;
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Set in a curve line
    /// </summary>
    private void SetCurveLine(float _angle)
    {
        Vector3 startPos, startPos2, targetPos, targetPos2, point;
        float index, startPosPivot, targetPosPivot;
        startPosPivot = 0.01f;
        targetPosPivot = 0.1f;

        laser.positionCount = CURVE_POINT_COUNT;

        for (int i = 0; i < CURVE_POINT_COUNT; i++)
        {
            index = i / (float)CURVE_POINT_COUNT;
            startPos = lineStartPosition.transform.position;
            startPos2 = lineStartPosition.transform.position + Vector3.up * startPosPivot;
            targetPos = target.transform.position;
            targetPos2 = target.transform.position + Vector3.up * _angle * targetPosPivot;
            point = BezierMath.BezierPoint(startPos, startPos2, targetPos2, targetPos, index);
            laser.SetPosition(i, point);
        }
        laser.enabled = true;
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Find the target when the target is NULL
    /// </summary>
    private void LaserFindTarget()
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(lineStartPosition.transform.position, lineStartPosition.transform.forward * LINE_LENGTH);
        if (Physics.Raycast(ray, out hitInfo, 10f, LayerMask.GetMask("Grabberable")))
        {
            target = hitInfo.collider;
            touchStartQuaternion = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
            material.color = Color.red;          
        }
        else
        {
            SetStraightLine();
        }
    }
    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// LaserInput
    /// </summary>
    private void LaserInput()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (target == null)
            {
                LaserFindTarget();
            }
            else
            {
                Quaternion dir = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
                float angle = Quaternion.Angle(dir, touchStartQuaternion);

                if (angle > 10 && touchStartQuaternion.x > dir.x)
                {
                    if (angle > 30)
                    {
                        angle = 30;
                    }
                    SetCurveLine(angle);
                    pullSwitch = true;
                }
                else
                {
                    SetStraightLine();
                }

            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (target != null && pullSwitch)
            {
                InteractObject temp = target.GetComponent<InteractObject>();
                temp.MoveObject(GetVelocity(target.transform.position, lineStartPosition.transform.position, 60f));
            }
            LaserReset();
        }
    }

    #endregion

    #region Grab

    private InteractObject grabObject;
    [SerializeField] private GameObject grabCenter;

    private void InputGrab()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            if(grabObject == null)
            {
                Collider[] nearObject = Physics.OverlapBox(grabCenter.transform.position, new Vector3(1f, 1f, 1f));

                for (int i = 0; i < nearObject.Length; i++)
                {
                    InteractObject interactObject = nearObject[i].GetComponent<InteractObject>();
                    if (interactObject != null)
                    {
                        grabObject = interactObject;
                        grabObject.SetGrab(grabCenter.transform);
                    }
                }
            }
        }
        if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {   
            if(grabObject != null)
            {
                grabObject.SetFreeObject();
                grabObject = null;
            }
            
        }
    }

    #endregion

    private Vector3 GetVelocity(Vector3 _targetPos, Vector3 _currentPos, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(_currentPos.x, 0, _currentPos.z);
        Vector3 planarPosition = new Vector3(_targetPos.x, 0, _targetPos.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = _targetPos.y - _currentPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (_currentPos.x > _targetPos.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }
    private void Update()
    {
        GetComponent<LeftHand>().
        LaserInput();
        InputGrab();
    }
}
