using UnityEngine;
using System.Collections;

public class ChangeView : MonoBehaviour {

    // Use this for initialization
    public string view;
    public GameObject player;
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    FPSInput main_input;
    RelativeMovement tps_input;
    //public GameObject gun;
	void Start () {
        view = "TP";
        main_input = this.
            GetComponent<FPSInput>();
        tps_input = 
            this.GetComponent<RelativeMovement>();
        thirdPersonCamera.gameObject.SetActive(true);
        firstPersonCamera.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.V))
        {
            ViewChange();
        }
	}
    
    void ViewChange()
    {
        if (view.Equals("TP"))
        {
            view = "FP";
        }
        else
        {
            view = "TP";
        }
        if (view == "FP")
        {
            main_input.enabled = true;
            tps_input.enabled = false;
            thirdPersonCamera.gameObject.SetActive(false);
            firstPersonCamera.gameObject.SetActive(true);
        }
        if(view == "TP")
        {
            main_input.enabled = false;
            tps_input.enabled = true;
            thirdPersonCamera.gameObject.SetActive(true);
            firstPersonCamera.gameObject.SetActive(false);
        }
    }
}
