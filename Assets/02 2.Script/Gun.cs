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
    private bool isReady = true;
    private bool isSliderFront = true;

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
        if(!isSliderFront)
        {
            yield return StartCoroutine(CoMoveSlider(true));
        }

        if (magazine == null || magazine.IsEmpty())
        {
            yield break;
        }   

        Bullet ejectionBullet = magazine.FireBullet();
        ejectionBullet.transform.position = ejectionLocation.transform.position;
        ejectionBullet.FireBullet();

        if(magazine.IsEmpty())
        {
            yield break;
        }

        yield return CoMoveSlider(false);
        isReady = true;
    }

    private IEnumerator CoMoveSlider(bool _isFront)
    {   
        Vector3 tartgetPos = _isFront ? new Vector3(slider.transform.localPosition.x, slider.transform.localPosition.y, 0) : standardSliderLocation.transform.localPosition;
        float timer = 0f;
        while(slider.transform.localPosition.z != tartgetPos.z)
        {
            yield return null;
            timer += Time.deltaTime * SLIDER_SPEED;
            slider.transform.localPosition = Vector3.Lerp(slider.transform.localPosition, tartgetPos, timer);
        }

        if(!_isFront)
        {
            isSliderFront = true;
            isReady = true;
        }
        else
        {
            isSliderFront = false;
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
    }
}
