using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameObjective : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private GOPopup goPopup;
    [SerializeField]
    private Text text;
	void Start () {
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            goPopup.Open();
            text.text = "1. Collect keys more than 1. \n2.Collect ores more than 2. \n3.Collect health packs more than 5. \n4.Equip the Key.";
        }
        else
        {
            goPopup.Open();
            text.text = "1. Kill 2 Boss enemies. \n2. Kill 5 monsters.";
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
