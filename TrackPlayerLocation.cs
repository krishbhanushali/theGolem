using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrackPlayerLocation : MonoBehaviour {

    // Use this for initialization
    public Text roomField;
    private int room;

    void Start()
    {
        room = 2;
        SetRoomText();
    }
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        string roomString = roomField.text;
        string[] parts = roomString.Split(':');
        int number = System.Int32.Parse(parts[1].Trim());
        if (other.gameObject.tag == "Player")
        {
            if(number == 2)
            {
                room = 1;
            }
            else
            {
                room = 2;
            }
            SetRoomText();
        }
    }
    void SetRoomText()
    {
        roomField.text = "Room : " + room;
    }
}
