using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class MCQPopup : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void onOptionChoose()
    {
        string selectedOption = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        Messenger<string>.Broadcast(GameEvent.OPTION_SELECTED, selectedOption);
    }
}