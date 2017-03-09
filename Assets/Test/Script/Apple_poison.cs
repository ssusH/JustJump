using UnityEngine;
using System.Collections;

public class Apple_poison : MonoBehaviour
{

    public PlayerControl Play;
    public float Speed = -5f;
    public Vector3 SpeedY = Vector3.zero;
    float lifetime = 5f;
    public PropManger PM;
    GameManger GM;
    public AudioClip AppleAudio;
    // Use this for initialization
    void Start()
    {
        PM = GameObject.Find("PropManger").GetComponent<PropManger>();
        GM = GameObject.Find("GameManger").GetComponent<GameManger>();
        SpeedY.y = Speed;
        Play = GameObject.Find("fox").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PM.AllPorpDead)
            Destroy(this.gameObject);
        if(!PM.ALLstop)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
                Destroy(this.gameObject);
            gameObject.transform.position += SpeedY;
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        if (collision.transform.tag == "Player")
        {
            Play.life--;
            if(GM.Sound)
                AudioSource.PlayClipAtPoint(AppleAudio, GameObject.Find("Main Camera").transform.position);
            Destroy(this.gameObject);
        }
    }


}