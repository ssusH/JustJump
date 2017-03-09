using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float Speed = 10f;
    public PlayerControl Player;
    public GameManger GM;
    public GameObject dieParticle;
    public EnemyManger EM;
    public AudioClip AttackAudio;
	// Use this for initialization
	void Start () {
        if (!GameObject.Find("fox"))
        {
            Destroy(this.gameObject);
        }
        Player = GameObject.Find("fox").GetComponent<PlayerControl>();
        GM = GameObject.Find("GameManger").GetComponent<GameManger>();
        EM = GameObject.Find("EnemyManger").GetComponent<EnemyManger>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.life <= 0)
        {
            CreateParticle();
            Destroy(this.gameObject);
        }
        if (EM.AllEnemyDead)
            Destroy(this.gameObject);
        if(!EM.AllStop)
        {
            transform.Translate(-Speed * Time.deltaTime, 0f, 0f);
            if (!GameObject.Find("fox"))
            {
                Destroy(this.gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            transform.Rotate (0f, 180f, 0f);
        if (collision.gameObject.tag == "Player"&& !Player.Unbeatable)
        {
            Player.life--;
            if (GM.Sound)
                AudioSource.PlayClipAtPoint(AttackAudio, GameObject.Find("Main Camera").transform.position);
            Player.Unbeatable = true;
        }
    }



    public void CreateParticle()
    {
        
        if (dieParticle)
        {
            GameObject obj = Instantiate(dieParticle, gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(obj, 0.5f);
        }
    }
}
