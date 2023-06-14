using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Magazine magazine;
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject ejectionLocation;
    [SerializeField] private GameObject magazineLocation;
    [SerializeField] private GameObject standardSliderLocation;
    private const int SLIDER_SPEED = 50;

    private bool isReady = true;            // 발사 준비

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Fire Gun
    /// </summary>
    public void Fire()
    {
        if (!isReady)
        {
            Debug.Log("Not Ready");
            return;
        }
        isReady = false;
        StartCoroutine(CoFire());
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// ReLoad
    /// </summary>
    public void ReLoad()
    {
        if (magazine == null || magazine.IsEmpty())
        {
            Debug.Log("Gun :: Reload => magazine is Null or No Bullet ");
            return;
        }
        StartCoroutine(CoMoveSlider(true));
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// remove a magazine
    /// </summary>
    public void RemoveMagazine()
    {
        if (magazine == null)
        {
            Debug.Log("Gun :: RemoveMagazine => Magazine is Null");
        }
        magazine.SetMagazine();
        magazine = null;
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// ReplaceMagazine
    /// </summary>
    /// <param name="_magazine"></param>
    public void ReplaceMagazine(Magazine _magazine)
    {
        RemoveMagazine();
        magazine = _magazine;
        _magazine.SetMagazine(this.gameObject);
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Fire Coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoFire()
    {
        yield return StartCoroutine(CoMoveSlider(false));

        if (magazine == null || magazine.IsEmpty())
        {
            yield break;
        }

        EjectionBullet(magazine.FireBullet());

        if (magazine.IsEmpty())
        {
            yield break;
        }

        yield return CoMoveSlider(true);
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// Bullet Effect
    /// </summary>
    /// <param name="_bullet"></param>
    private void EjectionBullet(Bullet _bullet)
    {
        _bullet.transform.position = ejectionLocation.transform.position;
        _bullet.FireBullet();
    }

    /// <summary>
    /// 
    /// [Jinyoung Kim]
    /// 
    /// SliderMove Coroutine
    /// </summary>
    /// <param name="_isBack"></param>
    /// <returns></returns>
    private IEnumerator CoMoveSlider(bool _isBack)
    {
        Vector3 targetPos = slider.transform.localPosition;
        targetPos.z = _isBack ? standardSliderLocation.transform.localPosition.z : 0;
        float timer = 0f;
        while (slider.transform.localPosition.z != targetPos.z)
        {
            yield return null;
            timer += Time.deltaTime * SLIDER_SPEED;
            slider.transform.localPosition = Vector3.Lerp(slider.transform.localPosition, targetPos, timer);
        }
        if (!_isBack)
        {
            isReady = true;
        }
    }

    // Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReLoad();
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            RemoveMagazine();
        }
    }
}
