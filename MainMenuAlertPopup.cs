using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MainMenuAlertPopup : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}