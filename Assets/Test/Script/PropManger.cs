using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;


public class PropManger : MonoBehaviour {
    public GameObject[] PropPerfab;

    public float BaseTime = 5f;
    int PropNum = 0;
    public bool ALLstop = false;
    public PlayerControl Player;
    public bool canUse = false;
    public bool AllPorpDead = false;
    bool produce = false;
    float CDtime = 0;
    public float Flag = 0;

	// Use this for initialization
	void Start () {
        Flag = Random.Range(0, 1f);
        Player = GameObject.Find("fox").GetComponent<PlayerControl>();
        ALLstop = false;
	}
	

	// Update is called once per frame
	void Update () {

        if (Player.life <= 0)
        {
            AllPropDead();
        }
        if(canUse)
        {
            CDtime += Time.deltaTime;
            if (produce)
            {
                Flag = Random.Range(0, 1f);
                PropNum = Random.Range(0, PropPerfab.Length);
                RandomCreateProp(PropPerfab[PropNum], 1.28f, 15 * 1.28f);
                produce = false;
                CDtime = 0f;
            }
            else
            {
                produce = CanProduce(CDtime);
            }
        }
        
	}

    public void  AllPropDead()
    {
        AllPorpDead = true;
    }

    void RandomCreateProp(GameObject Prop,float left,float right)
    {
        Vector3 PropPosition = new Vector3(0f, 12.8f, 0f);
        PropPosition.x = Random.Range(left, right);
        Instantiate(Prop, PropPosition, Quaternion.identity);
        
    }

    public bool CanProduce(float time)
    {
        
        float probability = Mathf.Log(time, BaseTime);
        if (Flag < probability)
        {
            //Debug.Log(Flag);
            //Debug.Log(probability);
            return true;
        }
        else
        {
            //Debug.Log(probability);
            return false;
        }
        
    }
}
