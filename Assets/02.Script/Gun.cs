using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Magazine   magazine;             
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject ejectionLocation;
    [SerializeField] private GameObject standardSliderLocation;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject magazineLocationStart;
    [SerializeField] private GameObject magazineLocationParent;

    private const int TARGET_LAYER = 7;
    private const int SLIDER_SPEED = 50;

    private LineRenderer line;

    private bool isRemoved = false;
    private bool isReady = true;            

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
    /// [Jinyoung Kim]
    /// 
    /// Fire Animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoFireEffect()
    {
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Standard"));
            line.material.color = Color.red;
            line.startWidth = 0.01f;
            line.endWidth = 0.01f;
            line.positionCount = 2;
        }
        else
        {
            line.enabled = true;
        }

        line.SetPosition(0, muzzle.transform.position);
        line.SetPosition(1, muzzle.transform.position + muzzle.transform.forward * 10f);

        yield return new WaitForSeconds(0.1f);
        line.enabled = false;
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

        StartCoroutine(CoFireEffect());

        Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward * 20f);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, 20f, LayerMask.GetMask("Target")))
        {
            if (hitinfo.collider.gameObject.TryGetComponent(out Target target))
            {
                target.Hit();
                Debug.Log("ИэСп");
            }
            else
            {
                Debug.Log("xx");
            }
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

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// SetMagazine
    /// </summary>
    /// <param name="_magazine"></param>
    public void SetMagazine(Magazine _magazine)
    {
        _magazine.transform.SetParent(magazineLocationParent.transform);
        _magazine.transform.localPosition = magazineLocationStart.transform.localPosition;
        _magazine.transform.localRotation = magazineLocationStart.transform.localRotation;
        StartCoroutine(CoSetMagazineAnim(_magazine));
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// SetMagazineAnimation
    /// </summary>
    /// <param name="_magazine"></param>
    /// <param name="_isInsert"></param>
    /// <returns></returns>
    private IEnumerator CoSetMagazineAnim(Magazine _magazine, bool _isInsert = true)
    {
        float progress = 0;
        Vector3 startPos = _isInsert ? _magazine.transform.localPosition : Vector3.zero;
        Vector3 endPos = _isInsert ? Vector3.zero : magazineLocationStart.transform.localPosition;
        Vector3 currentPos = startPos;

        while (progress <= 1)
        {
            progress += Time.deltaTime * 10f;
            currentPos = Vector3.Lerp(startPos, endPos, progress);
            _magazine.transform.localPosition = currentPos;
            yield return null;
        }

        _magazine.transform.localPosition = endPos;

        if (_isInsert)
        {
            magazine = _magazine;
        }
        else
        {
            _magazine.FreeMagazine();
            magazine = null;
            isRemoved = false;
        }

    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// Remove Magazine
    /// </summary>
    public void RemoveMagazine()
    {
        if (isRemoved || magazine == null)
        {
            return;
        }
        isRemoved = true;
        StartCoroutine(CoSetMagazineAnim(magazine, false));
    }

}
