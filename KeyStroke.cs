using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class KeyStroke : MonoBehaviour
{
    [SerializeField]
    private GOPopup goPopup;
    [SerializeField]
    private PausePopup pausePopup;
    [SerializeField]
    private MainMenuAlertPopup mainMenuAlertPopup;
    [SerializeField]
    private ExitPopup exitPopup;
    [SerializeField]
    private SettingsPopup settingsPopup;
    [SerializeField]
    private GameObject helpPopup;
    [SerializeField]
    private GameObject savePopup;
    [SerializeField]
    private GameObject player;

    // Use this for initialization
    // Update is called once per frame

    bool pausePopupStatus;
    bool gameObjectivePopupStatus;
    string _inventory;
    [SerializeField]
    private GameObject quitMenu;

    void Start()
    {
        pausePopup.Close();
        mainMenuAlertPopup.Close();
        exitPopup.Close();
        pausePopupStatus = false;
        gameObjectivePopupStatus = true;
        helpPopup.SetActive(false);
        savePopup.SetActive(false);
        _inventory = "";
    }
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            Managers.Audio.PlaySound(Resources.Load("Music/yes") as AudioClip);
            gameObjectivePopupStatus = !goPopup.isActiveAndEnabled;
            if (!gameObjectivePopupStatus)
            {
                goPopup.Close();
            }
            if (gameObjectivePopupStatus)
            {
                goPopup.Open();

            }
          
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.Audio.PlaySound(Resources.Load("Music/257357__brnck__button-click") as AudioClip);
            pausePopupStatus = !pausePopup.isActiveAndEnabled;
            if (!pausePopupStatus)
            {
                pausePopup.Close();
            }
            if(pausePopupStatus)
            {
                pausePopup.Open();
                
            }   
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
            OpenSavePopup();
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            LoadGame();
        }

        if (Input.GetKeyDown("q"))
        {
            Managers.Audio.PlaySound(Resources.Load("Music/yes") as AudioClip);
            OpenSavePopup();
            quitMenu.SetActive(true);
        }
    }

    public void OpenSavePopup()
    {
        savePopup.SetActive(true);
    }
    public void CloseSavePopup()
    {
        savePopup.SetActive(false);
    }

    public void CloseHelpPopup()
    {
        helpPopup.SetActive(false);
    }
    public void SaveGame()
    {
         PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
         PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
         PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);

         PlayerPrefs.SetInt("Health", Managers.Player.health);
         PlayerPrefs.SetInt("Score", Managers.Player.score);

         List<string> items = Managers.Inventory.GetItemList();
         foreach (string item in items)
         {
             int count = Managers.Inventory.GetItemCount(item);
             _inventory += item + "#" + count + ",";
         }
         _inventory = _inventory.Substring(0, _inventory.Length - 1);
         PlayerPrefs.SetString("inventory", _inventory);

         string equippedKey = Managers.Inventory.GetEquippedKey();
         PlayerPrefs.SetString("EquippedKey", equippedKey);

    }

    public void LoadGame()
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        player.transform.position = new Vector3(x, y, z);
        Managers.Player.SetHealth(PlayerPrefs.GetInt("Health"));
        Managers.Player.SetScore(PlayerPrefs.GetInt("Score"));

        string _inventory = PlayerPrefs.GetString("inventory");
        string[] itemsWithCount = _inventory.Split(',');
        foreach (string itemAndItsCount in itemsWithCount)
        {
            string[] itemAndItsCountSeperated = itemAndItsCount.Split('#');
            string item = itemAndItsCountSeperated[0];
            int count = int.Parse(itemAndItsCountSeperated[1]);
            Managers.Inventory.AddItemWithCount(item, count);
        }

        string equippedKey = PlayerPrefs.GetString("EquippedKey");
        Managers.Inventory.EquipItem(equippedKey);
        
    }


}
