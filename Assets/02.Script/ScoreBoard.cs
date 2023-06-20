using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// The scoreboard looks at the main camera
    /// </summary>
    private void Update()
    {
        if(GameManager.instance.character != null)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
