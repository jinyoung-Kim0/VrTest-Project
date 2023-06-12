using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private Character character;
    private CharacterController characterController;
    private const float SPEED = 2f;
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
        characterController = character.GetComponent<CharacterController>();
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
        float dirX = 0;
        float dirZ = 0;
        Vector2 tumStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        float absX = Mathf.Abs(tumStick.x);
        float absY = Mathf.Abs(tumStick.y);

        if(absX>absY)
        {
            if (tumStick.x > 0)
            {
                dirX = 1;
            }
            else
                dirX = -1;
        }
        else
        {
            if(tumStick.y >0)
            {
                dirZ = 1;
            }
            else
            {
                dirZ = -1;
            }
        }
        Vector3 movDir = new Vector3(dirX * SPEED, 0, dirZ * SPEED);
        transform.Translate(movDir * Time.deltaTime);
    }
    private void Button()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            Debug.Log("RightHandTrigger");
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            Debug.Log("RightHandTrigger");
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("RightIndexTrigger");
        }

        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("LeftIndexTrigger");
        }

        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Debug.Log("RawButton.Y");
        }

        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            Debug.Log("RawButton.X");
        }

        if (OVRInput.GetDown(OVRInput.RawButton.Start))
        {
            Debug.Log("RawButton.Start");
        }

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            Debug.Log("RawButton.B");
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log("RawButton.A");
        }
    }

    private void Update()
    {
        if (character == null)
            return;
        Move();
        Button();
    }
}
