using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{

    [SerializeField] private Gun gun;
    
    public enum HandType
    {
        Hand,
        Gun,
        NONE = 0
    }

    
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

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            gun.Fire();
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            gun.ReMoveMagazine();
        }
    }
}
