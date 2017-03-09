using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour {

    private Transform boardHolder;
    private Transform TheTree;
    public bool Sound = true;

    public GameObject myself;
    public GameObject GroundPrefab;
    public GameObject WallPrefab;
    public GameObject WallColliderPrefab;
    public GameObject BackGroundPrefab;
    public GameObject[] GrassPrefab;
    public GameObject[] MushroomPrefab;
    public GameObject[] RockPrefab;
    public GameObject[] WheatPrefab;
    public GameObject[] TreePrefab;

    public float score = 0;
    public float BestScore = 0;

    public Text Score;
    public Text bestScore;
    public Text Msum;
    public Text Ssum;
    public Text MaleSum;

    public EnemyManger EM;
    public PropManger PM;

	// Use this for initialization
    public void Awake()
    {
        BestScore = PlayerPrefs.GetFloat("MaxScore");
    }
	void Start () 
    {
        myself = GameObject.Find("fox");
        EM = GameObject.Find("EnemyManger").GetComponent<EnemyManger>();
        PM = GameObject.Find("PropManger").GetComponent<PropManger>();
        score = 0;
        //BuildFloor();
        //BuildScenery();

	}
	
	// Update is called once per frame
    void Update()
    {
        bestScore.text = BestScore.ToString();
        Score.text = "Score:" + score.ToString();
        Msum.text = EM.sumMonsterKill.ToString();
        Ssum.text = EM.sumSkeletonKill.ToString();
        MaleSum.text = EM.sumMaleKill.ToString();
        
    }

    public void BuildFloor()
    {
        boardHolder = new GameObject("Board").transform;  //新建一个名为Board（地面）的游戏物体组件
        //GameObject BackGround = Instantiate(BackGroundPrefab, new Vector3(10.24f,1.28f, 0f), Quaternion.identity)as GameObject;
        //BackGround.transform.SetParent(boardHolder);
        GameObject wallColliderPrefab = Instantiate(WallColliderPrefab, new Vector3(0, 6f, 0), Quaternion.identity) as GameObject;
        wallColliderPrefab.transform.SetParent(boardHolder);
        wallColliderPrefab = Instantiate(WallColliderPrefab, new Vector3(16*1.28f, 6f, 0), Quaternion.identity) as GameObject;
        wallColliderPrefab.transform.SetParent(boardHolder);

        TheTree = new GameObject("TheTree").transform;
        //生成地形
        for (int i = 0; i < 17; i++)
        {
            GameObject Ground = Instantiate(GroundPrefab, new Vector3(i * 1.28f, 0f, 0f), Quaternion.identity)as GameObject;
            Ground.transform.SetParent(boardHolder); //将刚刚实例化的物体设置为地形变量的子物体
        }
        for (int j = 0; j < 10; j++)
        {
            GameObject WallLeft = Instantiate(WallPrefab, new Vector3(0f, j * 1.28f, 0f), Quaternion.identity)as GameObject;
            WallLeft.transform.SetParent(boardHolder); //将刚刚实例化的物体设置为地形变量的子物体
            GameObject WallRight = Instantiate(WallPrefab, new Vector3(16 * 1.28f, j * 1.28f, 0f), Quaternion.identity)as GameObject;
            WallRight.transform.SetParent(boardHolder); //将刚刚实例化的物体设置为地形变量的子物体
        }
        int randomPosition = Random.Range(2,14);
        int randomTreeHight = Random.Range(1, 3);
        int randomTreeBig = Random.Range(1, 2);
        GameObject thetree = Instantiate(TreePrefab[0], new Vector3(randomPosition * 1.28f, 1.28f, 0f), Quaternion.identity)as GameObject;
        thetree.transform.SetParent(TheTree);
        for(int k = 2;k<=randomTreeHight+1;k++)
        {
            thetree = Instantiate(TreePrefab[1], new Vector3(randomPosition * 1.28f, k * 1.28f, 0f), Quaternion.identity) as GameObject;
            thetree.transform.SetParent(TheTree);
        }

        //生成树

       thetree = Instantiate(TreePrefab[2], new Vector3((randomPosition + 1) * 1.28f, (2 + randomTreeHight) * 1.28f, 0f), Quaternion.identity) as GameObject;
       thetree.transform.SetParent(TheTree);
       thetree = Instantiate(TreePrefab[2], new Vector3((randomPosition) * 1.28f, (2 + randomTreeHight) * 1.28f, 0f), Quaternion.identity) as GameObject;
       thetree.transform.SetParent(TheTree);
       thetree = Instantiate(TreePrefab[2], new Vector3((randomPosition - 1) * 1.28f, (2 + randomTreeHight) * 1.28f, 0f), Quaternion.identity) as GameObject;
       thetree.transform.SetParent(TheTree);
        for (int l = 3 + randomTreeHight; l <= 2 * (2 + randomTreeHight)-1; l++)
        {
            thetree = Instantiate(TreePrefab[2], new Vector3(randomPosition * 1.28f, l * 1.28f, 0f), Quaternion.identity) as GameObject;
            thetree.transform.SetParent(TheTree);
            randomTreeBig = Random.Range(2, 3);
            for (int p = 1; p <= randomTreeBig; p++)
            {
                thetree = Instantiate(TreePrefab[2], new Vector3((randomPosition + p) * 1.28f, l * 1.28f, 0f), Quaternion.identity) as GameObject;
                thetree.transform.SetParent(TheTree);
                thetree = Instantiate(TreePrefab[2], new Vector3((randomPosition - p) * 1.28f, l * 1.28f, 0f), Quaternion.identity) as GameObject;
                thetree.transform.SetParent(TheTree);
            }
        }
        thetree = Instantiate(TreePrefab[2], new Vector3((randomPosition + 1) * 1.28f, 2 * (2 + randomTreeHight) * 1.28f, 0f), Quaternion.identity) as GameObject;
        thetree.transform.SetParent(TheTree);
        thetree = Instantiate(TreePrefab[2], new Vector3((randomPosition) * 1.28f, 2 * (2 + randomTreeHight) * 1.28f, 0f), Quaternion.identity) as GameObject;
        thetree.transform.SetParent(TheTree);
        thetree = Instantiate(TreePrefab[2], new Vector3((randomPosition - 1) * 1.28f, 2 * (2 + randomTreeHight) * 1.28f, 0f), Quaternion.identity) as GameObject;
        thetree.transform.SetParent(TheTree);



        
    }
    public void BuildPlant(GameObject[] Plant)
    {
        
        int objectCount = Random.Range(5, 8);  //物体数量 ，生成一个在给定范围内的物体数量值
        for(int i = 0;i<objectCount;i++)
        {
            
            float xPosition = Random.Range(1.28f,15*1.28f);
            Vector3 randomPosition = new Vector3 (xPosition,1.28f,0);
            GameObject tileChoice = Plant[Random.Range(0, Plant.Length)];
            GameObject PlantPrefab = Instantiate(tileChoice, randomPosition, Quaternion.identity)as GameObject;
            PlantPrefab.transform.SetParent(boardHolder);
        }
    }

    public void BuildScenery()
    {
        boardHolder = new GameObject("Plant").transform;
        BuildPlant(GrassPrefab);
        BuildPlant(MushroomPrefab);
        BuildPlant(RockPrefab);
        BuildPlant(WheatPrefab);
    }

    public void star()
    {
        BuildFloor();
        BuildScenery();
        score = 0;
        EM.begin();
        PM.canUse = true;
        PM.ALLstop = false;
        PM.AllPorpDead = false;
        myself.GetComponent<PlayerControl>().enabled = true;
        myself.active = true;
        myself.GetComponent<Rigidbody2D>().isKinematic = false;
        myself.GetComponent<PlayerControl>().life = 3;

        myself.gameObject.transform.position = new Vector3(8 * 1.28f, 2f, 0);
        //Instantiate(myself, new Vector3(8 * 1.28f, 1.28f, 0), Quaternion.identity);

    }
    public void retry()
    {
        ResetBestScore();
        score = 0;
        EM.begin();
        EM.AddKillSum();
        EM.ResetKillSum();
        PM.canUse = true;
        myself.GetComponent<PlayerControl>().enabled = true ;
        PM.ALLstop = false;
        PM.AllPorpDead = false;
        myself.active = true;

        myself.GetComponent<Rigidbody2D>().isKinematic = false;
        myself.GetComponent<PlayerControl>().life = 3;
        //myself.GetComponent<PlayerControl>().UnbeatableTime = 0f;
        myself.gameObject.transform.position = new Vector3(8 * 1.28f, 2f, 0);
        //Instantiate(myself, new Vector3(8 * 1.28f, 1.28f, 0), Quaternion.identity);

    }
    public void stop()
    {
        EM.gameStop();
        myself.GetComponent<Rigidbody2D>().isKinematic = true;
        myself.GetComponent<PlayerControl>().enabled = false;
        PM.canUse = false ;
        PM.ALLstop = true;
        

    }
    public void gameContinue()
    {
        EM.AllStop = false;
        EM.canMakeEnemy = true;
        EM.AllEnemyDead = false;
        myself.GetComponent<PlayerControl>().enabled = true;
        myself.GetComponent<Rigidbody2D>().isKinematic = false;
        PM.canUse = false;
        PM.ALLstop = false;
        
    }

    public void SoundOff()
    {
        Sound = false;
        GameObject.Find("option UI").transform.FindChild("Button_Soff").transform.FindChild("Text").GetComponent<Text>().color = Color.red;
        GameObject.Find("option UI").transform.FindChild("Button_Son").transform.FindChild("Text").GetComponent<Text>().color = Color.black;

    }

    public void SoundOn()
    {
        Sound = true;
        GameObject.Find("option UI").transform.FindChild("Button_Soff").transform.FindChild("Text").GetComponent<Text>().color = Color.black;
        GameObject.Find("option UI").transform.FindChild("Button_Son").transform.FindChild("Text").GetComponent<Text>().color = Color.red;
    }
    public void MusicOff()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0;
        GameObject.Find("option UI").transform.FindChild("Button_Moff").transform.FindChild("Text").GetComponent<Text>().color = Color.red;
        GameObject.Find("option UI").transform.FindChild("Button_Mon").transform.FindChild("Text").GetComponent<Text>().color = Color.black;

    }

    public void MusicOn()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0.5f;
        GameObject.Find("option UI").transform.FindChild("Button_Moff").transform.FindChild("Text").GetComponent<Text>().color = Color.black;
        GameObject.Find("option UI").transform.FindChild("Button_Mon").transform.FindChild("Text").GetComponent<Text>().color = Color.red;
    }

    public void ResetBestScore()
    {
        if (score > BestScore)
            BestScore = score;
    }

    public void Quit()
    {
        PlayerPrefs.SetFloat("MaxScore", BestScore);
        PlayerPrefs.SetInt("MaleSum", EM.sumMaleKill);
        PlayerPrefs.SetInt("MonsterSum", EM.sumMonsterKill);
        PlayerPrefs.SetInt("SkeletonSum", EM.sumSkeletonKill);
        Application.Quit(); 
    }
}
