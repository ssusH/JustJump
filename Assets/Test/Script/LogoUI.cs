using UnityEngine;
using System.Collections;

public class LogoUI : MonoBehaviour {

    public float waitTime = 2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        waitTime -= Time.deltaTime;
        if(waitTime<=0)
        {
            Application.LoadLevel("JustJump");
        }
	}
}
