using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Character character;
    public ObjectPool<Bullet> bulletPool;
    public ObjectPool<Target> targetPool;

    [SerializeField] private TextMeshPro scoreBoard;

    private Vector3 magazineBoxPos;
    private int score;

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
        score = 0;
        bulletPool = new ObjectPool<Bullet>(this.gameObject, "Bullet");
        targetPool = new ObjectPool<Target>(this.gameObject, "Target");
        magazineBoxPos = new Vector3(6, 1.5f, 0);
    }

    private void Start()
    {
        InputManager.instance.SettingCharacter(character);
    }

    /// <summary>
    /// [Jinyoung Kim]
    /// 
    /// Score Points
    /// </summary>
    /// <param name="_hitTarget"></param>
    public void GetScore(Target _hitTarget)
    {
        ++score;
        scoreBoard.text = score.ToString();
    }

}
