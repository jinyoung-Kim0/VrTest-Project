using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    private LineRenderer layser;
    private Material material;
    void Start()
    {
        layser = this.gameObject.AddComponent<LineRenderer>();
        material = new Material(Shader.Find("Standard"));
        layser.material = material;
        layser.positionCount = 2;
        layser.startWidth = 0.01f;
        layser.endWidth = 0.01f;
        layser.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        layser.enabled = true;
        layser.SetPosition(0, transform.position);
        if (Input.GetKeyDown(KeyCode.G))
        {
            
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward * 2f,out hitInfo))
            {   
                if (hitInfo.collider.gameObject.layer == 6)
                {
                    material.color = Color.red;
                }
                else
                {
                    material.color = Color.blue;
                }
            }
            else
            {
                
            }
        }

        layser.SetPosition(1, transform.position + transform.forward * 2f);
        if (Input.GetKeyUp(KeyCode.G))
        {
            layser.enabled = false;
        }
    }
}
