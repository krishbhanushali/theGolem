using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PistolAnimationPlay : MonoBehaviour {


    public GameObject gun;
    public Animation _animation;
    	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            _animation.CrossFade("1stPistolShoot");
            gun.GetComponent<Animation>().CrossFade("1stPistolShoot");
        }
	}
}
