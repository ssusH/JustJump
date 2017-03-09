using UnityEngine;
using System.Collections;

public class Enemy_Hedgehog : MonoBehaviour {

    public float Speed = 10f;
    public PlayerControl Player;
    public GameManger GM;
    public int life = 1; 
    public GameObject dieParticle;
    public EnemyManger EM;
    Vector3 Myposition;
    public AudioClip AttackAudio;
    // Use this for initialization
    void Start()
    {
        life = 1;
        if (!GameObject.Find("fox"))
        {
            Destroy(this.gameObject);
        }
        Player = GameObject.Find("fox").GetComponent<PlayerControl>();
        GM = GameObject.Find("GameManger").GetComponent<GameManger>();
        EM = GameObject.Find("EnemyManger").GetComponent<EnemyManger>();
        Myposition = transform.position;
        Myposition.y -= 1.16f;

        transform.position = Myposition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.life <= 0)
        {
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
        {   
            if (life <= 0)
            {
                Destroy(this.gameObject);
                EM.SpecialEnemySum--;
            }
                
            life--;   
            transform.Rotate(0f, 180f, 0f);
        }   
        if (collision.gameObject.tag == "Player" && !Player.Unbeatable)
        {
            Player.life--;
            if (GM.Sound)
                AudioSource.PlayClipAtPoint(AttackAudio, GameObject.Find("Main Camera").transform.position);
            Player.Unbeatable = true;
        }
    }
}
