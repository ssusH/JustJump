using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

    Transform UIHolder;
    
    public GameObject dirtPrefab;
    public GameObject dirt_GrassPrefab;
    public GameObject GameOver_UI;
    public GameObject GameControl_UI;
    public PlayerControl Player;
    public int x = 17, y = 5;

    // Update is called once per frame
    void Start ()
    {
        Player = GameObject.Find("fox").GetComponent<PlayerControl>();
        GameOver_UI = GameObject.Find("GameOver_UI");
        GameOver_UI.active = false;
        dirt();
    }
    void Update()
    {
        if(Player.life <=0)
        {
            GameOver_UI.active = true;
            GameControl_UI.active = false;
            //GameObject.Find("Button_stop").active = false;
        }
    }

    public void dirt()
    {
        UIHolder = new GameObject("ui").transform;
        for (int i = 0;i<x;i++)
        {
            for(int j  = 0;j<y;j++)
            {
                GameObject UI_back;
                if(j ==y-1)
                    UI_back = Instantiate(dirt_GrassPrefab, new Vector3(i * 1.28f, j * 1.28f, 0), Quaternion.identity) as GameObject;
                else
                    UI_back = Instantiate(dirtPrefab, new Vector3(i * 1.28f, j * 1.28f, 0), Quaternion.identity) as GameObject;
                UI_back.transform.SetParent(UIHolder);

            }
        }
    }

    public void star ()
    {
       Destroy( GameObject.Find("ui").transform.gameObject);
    }
    public void InGameQuit()
    {
        Destroy(GameObject.Find("Board").transform.gameObject);
        Destroy(GameObject.Find("TheTree").transform.gameObject);
        Destroy(GameObject.Find("Plant").transform.gameObject);
    }


}
