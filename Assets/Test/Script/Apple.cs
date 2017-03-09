using UnityEngine;
using System.Collections;
public class Apple : MonoBehaviour {
    public PlayerControl Play;
    public float Speed = -5f;
    public Vector3 SpeedY = Vector3.zero;
    public PropManger PM;
    float lifetime = 5f;
    GameManger GM;
    public AudioClip AppleAudio;
	// Use this for initialization
	void Start () {
        SpeedY.y = Speed;
        GM = GameObject.Find("GameManger").GetComponent<GameManger>();
        Play = GameObject.Find("fox").GetComponent<PlayerControl>();
        PM = GameObject.Find("PropManger").GetComponent<PropManger>();
	}
	
	// Update is called once per frame
	void Update () {
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
            if(Play.life<3)
                Play.life++;
            if (GM.Sound)
                AudioSource.PlayClipAtPoint(AppleAudio, GameObject.Find("Main Camera").transform.position);
            Destroy(this.gameObject);
        }
    }
}
