using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour {


    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    [SerializeField]
    private GameObject levelPopup;
    // Use this for initialization
    void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
        levelPopup.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void OpenLevelPopup()
    {
        levelPopup.SetActive(true);
    }
    public void CloseLevelPopup()
    {
        levelPopup.SetActive(false);
    }
    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }
    
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
