using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private Character character;
    private const float SPEED = 2f;
    private bool screenSwitch = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Move();
        InputButton();
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Connect Chracter 
    /// </summary>
    /// <param name="_character"></param>
    public void SettingCharacter(Character _character)
    {
        character = _character;
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// ClearCharacter
    /// </summary>
    /// <param name="_character"></param>
    public void ClearCharacter(Character _character)
    {
        character = null;
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// Moving Character
    /// </summary>
    private void Move()
    {
        if (character == null)
            return;
        Vector2 joystickAxis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick, OVRInput.Controller.LTouch);
        character.transform.position += (joystickAxis.x * transform.right +joystickAxis.y*transform.forward) * Time.deltaTime * SPEED;
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// Input
    /// </summary>
    private void InputButton()
    {
        if(character == null)
        {
            return;
        }    


        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            character.leftHand.ButtonPrimaryHandTrigger();
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            character.leftHand.ButtonUpPrimaryHandTrigger();
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            character.leftHand.ButtonPrimaryIndexTrigger();
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            character.leftHand.ButtonUpPrimaryIndexTrigger();
        }        

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            character.rightHand.Fire();
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            character.rightHand.RemoveMagazine();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            character.rightHand.Reload();
        }


        if(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x >0.5 && screenSwitch)
        {
            screenSwitch = false;
            StartCoroutine(CoScreenSwitch());
            character.transform.Rotate(Vector3.up, 90f);
        }
        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x < -0.5&&screenSwitch)
        {
            screenSwitch = false;
            StartCoroutine(CoScreenSwitch());
            character.transform.Rotate(Vector3.down, 90f);
        }
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// SWaiting for screen change
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoScreenSwitch()
    {   
        yield return new WaitForSeconds(1f);
        screenSwitch = true;
    }
}
