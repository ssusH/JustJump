using UnityEngine;
using System.Collections;

public class Enemy_Skeleton : MonoBehaviour
{

    public Animator ani;
    public float shotCD = 5f;
    public float bowCD = 3f;
    public float aniTime = 6f;


    public bool Shotting = false;
    bool canShot = true;
    public float Speed = 10f;
    public PlayerControl Player;
    public GameManger GM;
    public EnemyManger EM;
    public GameObject dieParticle;
    public GameObject bowArrow;
    public GameObject bow;
    public Vector3 scale = new Vector3(1.2f,1.2f,1f);
    Vector3 Myposition;
    public AudioClip AttackAudio;
    // Use this for initialization
    void Start()
    {
        if (!GameObject.Find("fox"))
        {
            Destroy(this.gameObject);
        }
        Player = GameObject.Find("fox").GetComponent<PlayerControl>();
        GM = GameObject.Find("GameManger").GetComponent<GameManger>();
        EM = GameObject.Find("EnemyManger").GetComponent<EnemyManger>();
        //bowArrow = GameObject.Find("bowArrow");
        ani = GetComponent<Animator>();
        Myposition = transform.position;
        Myposition.y += 0.3f;
        transform.position = Myposition;

    }

    void Update()
    {
        if(Player.life<=0)
        {
            CreateParticle();
            Destroy(this.gameObject);
        }
        if (EM.AllEnemyDead)
            Destroy(this.gameObject);
        if(!EM.AllStop)
        {
            if (!Shotting)
            {
                bowCD -= Time.deltaTime;
                transform.Translate(-Speed * Time.deltaTime, 0f, 0f);
                shotCD -= Time.deltaTime;
                if (bowCD <= 0)
                {
                    bowArrow.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                aniTime -= Time.deltaTime;
                if (canShot)
                    StartCoroutine(bowshot());

                if (aniTime <= 0)
                {
                    Shotting = false;
                    canShot = true;

                    bowCD = 1f;
                    aniTime = 2f;
                }
            }
            if (shotCD <= 0 && !facePlayer())
            {
                shotCD = 2f;
            }

            if (shotCD <= 0 && facePlayer())
            {
                Shotting = true;
                ani.SetTrigger("isShot");
                shotCD = 2f;
            }


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
            Speed *= -1;
            scale.x *= -1;
            transform.localScale = scale;
        }
        if (collision.gameObject.tag == "Player" && !Player.Unbeatable)
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

    public bool facePlayer()
    {
        float face =  GameObject.Find("fox").transform.position.x - transform.position.x;
        if(transform.localScale.x>0 )
        {
            if (face > 0)
                return false;
            else
                return true;
        }
        else
        {
            if (face > 0)
                return true;
            else
                return false ;
        }
    }
    IEnumerator bowshot()
    {
        for (float timer = 0; timer < 1.3f; timer += Time.deltaTime)
        {
            canShot = false;
            yield return 0;
        }
        Instantiate(bow, bowArrow.transform.position, Quaternion.identity);
        bowArrow.GetComponent<SpriteRenderer>().enabled = false;
    }
}