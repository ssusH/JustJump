using UnityEngine;
using System.Collections;

public class shot : MonoBehaviour {

    public PlayerControl Player;
    public float speed = 1f;
    GameObject Play;
    
    Vector3 Direction = Vector3.zero;
    bool left = false;
    bool right = false;
    float cos = 0;
    float sin = 0;
    float tan = 0;
    GameManger GM;
    public AudioClip shotAudio;
    public AudioClip AttackAudio;
	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManger").GetComponent<GameManger>();
        Player = GameObject.Find("fox").GetComponent<PlayerControl>();
        Play = GameObject.Find("fox");
        cos = (Play.transform.position.x - transform.position.x)/Mathf.Sqrt((Play.transform.position.y - transform.position.y) * (Play.transform.position.y - transform.position.y) + (Play.transform.position.x - transform.position.x) * (Play.transform.position.x - transform.position.x));
        sin = (Play.transform.position.y - transform.position.y) / Mathf.Sqrt((Play.transform.position.y - transform.position.y) * (Play.transform.position.y - transform.position.y) + (Play.transform.position.x - transform.position.x) * (Play.transform.position.x - transform.position.x));
        tan = (Play.transform.position.y - transform.position.y) / (Play.transform.position.x - transform.position.x);
        float Atan = Mathf.Atan(tan) * Mathf.Rad2Deg;
        Direction = transform.position;
        transform.Rotate(new Vector3(0,0,Atan));
        if (Play.transform.position.x - transform.position.x>0)
            right = true;
        else
            left = true;
        if (GM.Sound)
            AudioSource.PlayClipAtPoint(shotAudio, GameObject.Find("Main Camera").transform.position);

	}
	
	// Update is called once per frame
	void Update () {
        if (right)
        {
            Direction.y += Time.deltaTime * speed * sin;
            Direction.x += Time.deltaTime * speed * cos;
        }
        if(left)
        {
            Direction.y -= Time.deltaTime * speed * tan;
            Direction.x -= Time.deltaTime * speed;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        
        transform.position = Direction;
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            Destroy(this.gameObject);
        if (collision.gameObject.tag == "Player" && !Player.Unbeatable)
        {
            Player.life--;
            Destroy(this.gameObject);
            if (GM.Sound)
                AudioSource.PlayClipAtPoint(AttackAudio, GameObject.Find("Main Camera").transform.position);
            Player.Unbeatable = true;
        }
    }

}
