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
    private List<Magazine> tableMagazine;
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

    private void TableMagazineInitialize()
    {
        tableMagazine = new List<Magazine>(9);
        Vector3 preset = new Vector3(1f,0,0);
        Magazine firstOne = Resources.Load<Magazine>("Magazine");

        for (int i = 0; i < tableMagazine.Count; i++)
        {
            tableMagazine[i] = Instantiate(firstOne);
        }
        //firstOne.transform.position = new Vector3(5, 2, 1);
        //for (int i = 1; i < tableMagazine.Count; i++)
        //{
        //    Vector3 pos = tableMagazine[i - 1].transform.position;
        //    if(i % 4 == 0)
        //    {
        //        pos.z--;
        //    }
        //    else
        //    {
        //        pos.x++;
        //    }
        //    tableMagazine[i].transform.position = pos;
        //}
        
    }

    private void ResetTableMagazine()
    {
        Vector3 startPos = new Vector3(5, 2, 1);
        int count = 0;
        for (int i = 0; i < tableMagazine.Count; i++)
        {
            if(tableMagazine[i].IsFree())
            {
                tableMagazine[i].transform.position = startPos;
                /// 여기서 부터 
            }
        }
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
        targetPool.Push(_hitTarget);
    }

}
