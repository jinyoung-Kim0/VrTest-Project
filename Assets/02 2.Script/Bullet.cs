using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private Rigidbody rigidBody;
    private const float DISCHARGE_SPEED = 2f;

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// FireBulletEffect And Disappearing Effect Start
    /// </summary>
    public void FireBullet()
    {
        head.gameObject.SetActive(false);
        rigidBody.isKinematic = false;
        rigidBody.AddForce(transform.up + transform.right * DISCHARGE_SPEED, ForceMode.Impulse);
        StartCoroutine(CoDisappear());
    }

    /// <summary>
    /// 
    /// [JInyoung Kim]
    /// 
    /// Disappearing Coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoDisappear()
    {
        yield return new WaitForSeconds(2f);
        BulletReset();
        GameManager.instance.PushPoolBullet(this);
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Bullet Reset
    /// </summary>
    private void BulletReset()
    {
        head.gameObject.SetActive(true);
        rigidBody.isKinematic = true;
    }
}
