using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class PausePopup : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}