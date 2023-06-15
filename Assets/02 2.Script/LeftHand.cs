using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    [SerializeField] private GameObject lineStartPosition;
    private LineRenderer layser;
    private Material material;
    private const int CURVE_MAXPOINT_COUNT = 20;
    private const int LINE_POINT_COUNT = 2;
    private Collider target;
    private Quaternion touchStandardQuaternion;
    void Start()
    {   
        LayserInitialize();
    }
    #region Layser

    /// <summary>
    /// 
    /// [Jinyoung Kim] 
    /// 
    /// Layser Initialize
    /// </summary>
    private void LayserInitialize()
    {
        layser = this.gameObject.AddComponent<LineRenderer>();
        material = new Material(Shader.Find("Standard"));
        layser.material = material;
        material.color = Color.blue;
        layser.positionCount = 2;
        layser.startWidth = 0.01f;
        layser.endWidth = 0.01f;
        layser.enabled = false;
    }


    private void LayserInput()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            layser.enabled = true;
            layser.SetPosition(0, lineStartPosition.transform.position);

            if (target==null)
            {
                RaycastHit hitInfo;
                Ray ray = new Ray(lineStartPosition.transform.position, lineStartPosition.transform.forward * 2f);
                if (Physics.Raycast(ray, out hitInfo, 10f, LayerMask.GetMask("Grabberable")))
                {
                    target = hitInfo.collider;
                    touchStandardQuaternion = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
                    layser.SetPosition(1, lineStartPosition.transform.position + target.transform.position);
                    material.color = Color.red;
                    return;
                }
                else
                {   
                    layser.SetPosition(1, lineStartPosition.transform.position + lineStartPosition.transform.forward * 4f);
                }
            }
            else
            {
                Quaternion dir = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
                float temp = Quaternion.Angle(dir, touchStandardQuaternion);
                
                if(temp>10&&  touchStandardQuaternion.x > dir.x)
                {
                    if(temp > 30)
                    {
                        temp = 30;
                    }    
                    layser.positionCount = CURVE_MAXPOINT_COUNT + 1;
                    for (int i = 0; i < CURVE_MAXPOINT_COUNT + 1; i++)
                    {
                        float index = i / (float)CURVE_MAXPOINT_COUNT;
                        Vector3 startPos = lineStartPosition.transform.position;
                        Vector3 startPos2 = lineStartPosition.transform.position + Vector3.up * 0.01f;
                        Vector3 targetPos = target.transform.position;
                        Vector3 targetPos2 = target.transform.position + Vector3.up * temp * 0.1f;
                        Vector3 point = BezierMath.BezierPoint(startPos, startPos2, targetPos2, targetPos, index);
                        layser.SetPosition(i, point);
                    }
                }
                else
                {
                    layser.positionCount = LINE_POINT_COUNT;
                    layser.SetPosition(0, lineStartPosition.transform.position);
                    layser.SetPosition(1, target.transform.position);
                }
            }
        }



        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            target = null;
            material.color = Color.blue;
            layser.positionCount = LINE_POINT_COUNT;
            layser.enabled = false;
        }
    }

    #endregion

    private void Update()
    {
        LayserInput();
    }
}
