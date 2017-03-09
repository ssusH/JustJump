using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    float Horizontal;
    public Rigidbody2D Player;
    Vector2 tmp = Vector2.one;

    public bool Jumpping = false;
    public bool OnGround = true;
    public bool attackEnemy = true;
    public bool PlayerStop = true ;
    public bool Unbeatable = false;

    public bool UIleft;
    public bool UIright;

    public float UnbeatableTime = 3f;
    public float Speed = 5f;
    public GameObject shit;
    public GameObject[] Heart;
    private Animator animation;
    //public float JumpHigh = 10f;
    public Transform GroundCheck;
    public Transform bobo;
    public int life = 3;
    int combo = 0;
    int comboAddLift = 0;
    public float rayLenth = 2.5f;

    public Text Life;
    public Text Combo;

    Vector3 PlayerPosition = Vector3.zero;
    public Vector2 jumpHight = new Vector2(0f, 10f);
    public Vector2 jumpVelocity = new Vector2(0f, 10f);
    public GameManger GM;
    public EnemyManger EM;
    public PropManger PM;
    public AudioClip JumpAudio;
    public AudioClip HitAudio;
    public AudioClip AppleAudio;
	// Use this for initialization
	void Start () {
        Player = GetComponent<Rigidbody2D>();
        GroundCheck = transform.Find("GroundCheck");
        GM = GameObject.Find("GameManger").GetComponent<GameManger>();
        EM = GameObject.Find("EnemyManger").GetComponent<EnemyManger>();
        PM = GameObject.Find("PropManger").GetComponent<PropManger>();
        animation = GetComponent<Animator>();
        bobo = transform.Find("bobo");
	}
	
	// Update is called once per frame
    void Update()
    {

        for (int i = 0; i < 3;i++)
        {
            Heart[i].active = false;
        }
        for (int j = 0; j < life; j++)
            Heart[j].active = true ;
            if (combo != 0)
                Combo.text = combo.ToString() + "Hit";
            else
                Combo.text = " ";

        if (life <= 0)
        {
            combo = 0;
            Combo.text = " ";
            life = 0;
            EM.stop();
            PM.canUse = false;
            UnbeatableTime = 0f;
            gameObject.active = false;
        }
        OnGround = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("BackGround"));
        if(OnGround)
        {
            combo = 0;
            comboAddLift = 0;
        }
        PlayerMove();
        if (UIleft)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }
        if (UIright)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetAxis("Vertical") > 0 && OnGround )
        {
            PlayerJump();
            if (GM.Sound)
                AudioSource.PlayClipAtPoint(JumpAudio, GameObject.Find("Main Camera").transform.position);
        }

        if(Unbeatable)
        {
            UnbeatablePlayer();
        }

        if(Input.GetKeyDown (KeyCode.J))
        {
            Instantiate(shit, GroundCheck.position, Quaternion.identity);
        }
        if(comboAddLift==10&&combo !=0&&life<3)
        {
            if (GM.Sound)
                AudioSource.PlayClipAtPoint(AppleAudio, GameObject.Find("Main Camera").transform.position);
            life++;
            comboAddLift = 0;
        }
    }

        
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLenth, 1 << LayerMask.NameToLayer("Enemy"));

        if (hit)
        {
            if (GM.Sound)
                AudioSource.PlayClipAtPoint(HitAudio, GameObject.Find("Main Camera").transform.position );
            if (hit.collider.gameObject.transform.name == "Enemy_Male" ||hit.collider.gameObject.transform.name == "Enemy_Male(Clone)")
            {
                hit.collider.gameObject.GetComponent<Enemy>().CreateParticle();
                Destroy(hit.collider.gameObject);
                GM.score += 50;
                EM.EnemySum--;
                EM.MaleKill++;
            }     
            if (hit.collider.gameObject.transform.name == "Enemy_Skeleton" || hit.collider.gameObject.transform.name == "Enemy_Skeleton(Clone)")
            {
                hit.collider.gameObject.GetComponent<Enemy_Skeleton>().CreateParticle();
                Destroy(hit.collider.gameObject);
                GM.score += 80;
                EM.SpecialEnemySum--;
                EM.SkeletonKill++;
            }    
            if (hit.collider.gameObject.transform.name == "Enemy_Monster" || hit.collider.gameObject.transform.name == "Enemy_Monster(Clone)")
            {
                hit.collider.gameObject.GetComponent<Enemy_Monster>().CreateParticle();
                Destroy(hit.collider.gameObject);
                GM.score += 120;
                EM.SpecialEnemySum--;
                EM.MonsterKill++;
            }
            combo++;
            comboAddLift++;
            gameObject.GetComponent<Rigidbody2D>().velocity = jumpVelocity;
        }
    }

    void PlayerJump()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = jumpHight;
    }

    void PlayerMove()
    {
        Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal > 0)
        {
            animation.SetBool("isRun", true);
            if (tmp.x > 0)
            {
                tmp.x *= -1;
                Player.gameObject.transform.localScale = tmp;
            }
        }
        else if (Horizontal < 0)
        {
            animation.SetBool("isRun", true);
            if (tmp.x < 0)
            {
                tmp.x *= -1;
                Player.gameObject.transform.localScale = tmp;
            }
        }
        else
        {
            animation.SetBool("isRun", false);
        }
        transform.Translate(Horizontal * Speed * Time.deltaTime, 0, 0);
        

    }

    void UnbeatablePlayer()
    {
        UnbeatableTime -= Time.deltaTime;
        bobo.localScale = new Vector3 (0.8f,0.6f,0f)*(UnbeatableTime / 3f); 
        if(UnbeatableTime <= 0)
        {
            //transform.tag = "Player";
            bobo.localScale = new Vector3(0.8f, 0.6f, 0f) * (UnbeatableTime / 3f); 
            UnbeatableTime = 3f;
            Unbeatable = false;
        }         
    }
            
    public void UIJump()
    {
        if (OnGround)
        {
            PlayerJump();
            if (GM.Sound)
                AudioSource.PlayClipAtPoint(JumpAudio, GameObject.Find("Main Camera").transform.position);
        }
    }

    public void UILeft()
    {
        UIleft = true;
    }
    public void UIRight()
    {
        UIright = true;
    }
    public void UILeftOff()
    {
        UIleft = false;
    }
    public void UIRightOff()
    {
        UIright = false;
    }

}
