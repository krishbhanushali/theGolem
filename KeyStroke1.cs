using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyStroke1: MonoBehaviour
{
    [SerializeField]
    private GameObject goPopup;
    [SerializeField]
    private GameObject pausePopup;
    [SerializeField]
    private GameObject mainMenuAlertPopup;
    [SerializeField]
    private GameObject exitPopup;
    [SerializeField]
    private GameObject settingsPopup;
    [SerializeField]
    private GameObject helpPopup;
    [SerializeField]
    private GameObject quitMenu;

    // Use this for initialization
    // Update is called once per frame
    
    void Start()
    {
        pausePopup.SetActive(false);
        mainMenuAlertPopup.SetActive(false);
        exitPopup.SetActive(false);
        helpPopup.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            Managers.Audio.PlaySound(Resources.Load("Music/yes") as AudioClip);
            if (goPopup.activeInHierarchy == false)
            {
                goPopup.SetActive(true);
            }
            else
                goPopup.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.Audio.PlaySound(Resources.Load("Music/257357__brnck__button-click") as AudioClip);

            if (pausePopup.activeInHierarchy == false)
            {
                pausePopup.SetActive(true);
            }
            else
                pausePopup.SetActive(false);
        }

        if (Input.GetKeyDown("h"))
        {
            Managers.Audio.PlaySound(Resources.Load("Music/yes") as AudioClip);
            if (helpPopup.activeInHierarchy == true)
            {
                helpPopup.SetActive(false);
            }
            else
            {
                helpPopup.SetActive(true);
            }

        }


        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            LoadGame();
        }

        if (Input.GetKeyDown("q"))
        {
            Managers.Audio.PlaySound(Resources.Load("Music/yes") as AudioClip);
            quitMenu.SetActive(true);
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
    }

    public void CloseHelpPopup()
    {
        helpPopup.SetActive(false);
    }
    public void LoadGame()
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        transform.position = new Vector3(x, y, z);
    }
}
