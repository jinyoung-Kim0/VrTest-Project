using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private Character character;
    [SerializeField] private Rigidbody rigidbody = null;
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
        Vector2 joystickAxis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick, OVRInput.Controller.LTouch);
        transform.position += (joystickAxis.x * transform.right +joystickAxis.y*transform.forward) * Time.deltaTime * SPEED;
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
        Move();
        if (character == null)
            return;
      
        Button();
    }
}
