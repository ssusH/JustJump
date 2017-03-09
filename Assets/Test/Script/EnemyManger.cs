using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManger : MonoBehaviour {

    public Transform CreateLeft;
    public Transform CreateRight;
    public GameObject EnemyPrefab;
    public GameObject[] SpecialEnemyPrefab;
    GameManger GM;
    public PlayerControl Player;
    public bool AllStop = false;

    public bool LeftCDing = false;
    public bool RightCDing = false;
    public bool SpecialCDing = false;
    public bool canMakeEnemy = true;
    public bool AllEnemyDead = false;

    public int MonsterKill = 0;
    public int SkeletonKill = 0;
    public int MaleKill = 0;
    public int sumMonsterKill = 0;
    public int sumSkeletonKill = 0;
    public int sumMaleKill = 0;
    public Text monsterKill;
    public Text skeletonKill;
    public Text maleKill;

    public int baseSpecialEnemy = 3;
    public int baseEnemy = 8;
    public int EnemySum = 0;
    public int SpecialEnemySum = 0;


    int leftCD = 0;
    int rightCD = 0;
    int specialCD = 0;
    int specialNum = 0;
    int LeftOrRight = 0;
	// Use this for initialization
    public void Awake()
    {
        sumMaleKill = PlayerPrefs.GetInt("MaleSum");
        sumMonsterKill = PlayerPrefs.GetInt("MonsterSum");
        sumSkeletonKill = PlayerPrefs.GetInt("SkeletonSum");
    }

	void Start () {
        GM = GameObject.Find("GameManger").GetComponent<GameManger>();
        Player = GameObject.Find("fox").GetComponent<PlayerControl>();
	}

    public void begin()
    {
        EnemySum = 0;
        SpecialEnemySum = 0;
        leftCD = 0;
        rightCD = 0;
        specialCD = 0;
        LeftCDing = false;
        RightCDing = false;
        AllStop = false;
        SpecialCDing = false;
        AllEnemyDead = false;
        canMakeEnemy = true;
    }
    public void QuitInGame()
    {
        AllEnemyDead = true;
        Player.life = 3;
        Player.UnbeatableTime = 0;
        Player.gameObject.transform.position = new Vector3(8*1.28f,1.28f,0);
        AddKillSum();
        ResetKillSum();
        GM.ResetBestScore();
        
    }
    public void stop()
    {
        LeftCDing = true;
        RightCDing = true;
        SpecialCDing = true;
    }
    public void gameStop()
    {
        AllStop = true;
        canMakeEnemy = false;
    }
    public void ResetKillSum()
    {
        MonsterKill = 0;
        SkeletonKill = 0;
        MaleKill = 0;
    }

    public void AddKillSum()
    {
        sumMonsterKill += MonsterKill;
        sumSkeletonKill += SkeletonKill;
        sumMaleKill += MaleKill;
    }


	// Update is called once per frame
	void Update () {
        if (Player.life <= 0)
        {
            gameStop();
            stop();
            monsterKill.text = MonsterKill.ToString();
            skeletonKill.text = SkeletonKill.ToString();
            maleKill.text = MaleKill.ToString();

        }
        if (!GameObject.Find("fox"))
        {
            LeftCDing = true;
            RightCDing = true;
        }

        leftCD = Random.Range(2, 4);
        rightCD = Random.Range(2, 4);
        specialCD = Random.Range(3, 5);
        LeftOrRight = Random.Range(1, 4);
        specialNum = Random.Range(0, SpecialEnemyPrefab.Length);
        if (!LeftCDing && EnemySum < baseEnemy+GM.score/600)
        {
            StartCoroutine(LeftCD(leftCD));
            if (canMakeEnemy)
            {
                Instantiate(EnemyPrefab, new Vector3(1.28f, 2f, 0), CreateLeft.rotation);
                EnemySum++;
            }
        }
        if (!RightCDing && EnemySum < baseEnemy + GM.score / 600)
        {
            StartCoroutine(RightCD(rightCD));
            if (canMakeEnemy)
            {
                Instantiate(EnemyPrefab, new Vector3(15 * 1.28f, 2f, 0), CreateRight.rotation);
                EnemySum++;
            }
        }

        if(!SpecialCDing && SpecialEnemySum <baseSpecialEnemy + GM.score/1000)
        {   
            StartCoroutine(SpecialCD(specialCD));
            if (LeftOrRight > 2 && canMakeEnemy)
            {
                Instantiate(SpecialEnemyPrefab[specialNum], new Vector3(1.28f, 2f, 0), Quaternion.identity);
                SpecialEnemySum++;
            }
            else if (LeftOrRight <= 2 && canMakeEnemy)
            {
                Instantiate(SpecialEnemyPrefab[specialNum], new Vector3(15 * 1.28f, 2f, 0), Quaternion.identity);
                SpecialEnemySum++;
            }    
        }


	}

    IEnumerator LeftCD(float CDTime)
    {
        for (float timer = 0; timer < CDTime; timer += Time.deltaTime)
        {
            LeftCDing = true;
            yield return 0;
        }
        LeftCDing = false;
    }
    IEnumerator RightCD(float CDTime)
    {
        for (float timer = 0; timer < CDTime; timer += Time.deltaTime)
        {
            RightCDing = true;
            yield return 0;
        }
        RightCDing = false;
    }

    IEnumerator SpecialCD(float CDTime)
    {
        for (float timer = 0; timer < CDTime; timer += Time.deltaTime)
        {
            SpecialCDing = true;
            yield return 0;
        }
        SpecialCDing = false;
    }
}
