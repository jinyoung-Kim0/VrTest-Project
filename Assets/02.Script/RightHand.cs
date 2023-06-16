using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    public enum HandType
    {
        Hand,
        Gun,
        NONE = 0
    }

    private Gun gun;
    private HandType currentType;

    public void ChangeHandType(HandType _handType)
    {
        switch(currentType)
        {
            case HandType.Gun:
                {
                    gun.gameObject.SetActive(false);
                }
                break;
            case HandType.Hand:
                {
                    if(gun == null)
                    {
                        Debug.Log("RightHand :: ChangeHandType Gun is Null");
                        return;
                    }
                    gun.gameObject.SetActive(true);
                }
                break;
            default:
                Debug.Log("RightHand :: HandType is NONE");
                return;
        }

        currentType = _handType;
    }

    public void InputHand()
    {

    }
    public void InputGun()
    {

    }

}
