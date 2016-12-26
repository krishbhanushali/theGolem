using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text scoreLabel;
    [SerializeField]
    private SettingsPopup settingsPopup;
    [SerializeField]
    private RayShooter rayshooter;
    [SerializeField]
    private Text roomLabel;

    [SerializeField]
    private MCQPopup mcqPopup;
    private int score;
    private int room;

    [SerializeField]
    private ErrorInventoryPopup errorPopup;

    [SerializeField]
    private Text questionLabel;
    [SerializeField]
    private Button optionA;
    [SerializeField]
    private Button optionB;
    [SerializeField]
    private Button optionC;
    private int teleportFrom=0;

    GameObject player;

    [SerializeField]
    private WrongAnswerPopup wap;

    [SerializeField]
    private Text error;

    ArrayList questions = new ArrayList();
    int questionNo;
    void Start()
    {
        score = 0;
        room = 1;
        UpdateRoom();
        UpdateScore();
        settingsPopup.Close();
        mcqPopup.Close();
        errorPopup.Close();
        wap.Close();
        questions.Add("What is the capital of United States of America?");
        questions.Add("How much is 12*12?");
        questions.Add("Which is the latest iPhone Model?");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.AddListener(GameEvent.TELEPORT_FROM_1, FromRoom1);
        Messenger.AddListener(GameEvent.TELEPORT_FROM_2, FromRoom2);
        Messenger<int>.AddListener(GameEvent.OPEN_MCQ_POPUP, Open);
        Messenger.AddListener(GameEvent.CLOSE_MCQ_POPUP, Close);
        Messenger.AddListener(GameEvent.ERROR_INVENTORY_CLOSE_POPUP, CloseErrorInventoryPopup);
        Messenger<string>.AddListener(GameEvent.ERROR_INVENTORY, publishErrorForInventory);
        Messenger<string>.AddListener(GameEvent.OPTION_SELECTED, checkAnswer);
    }

    private void CloseErrorInventoryPopup()
    {
        errorPopup.Close();
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.RemoveListener(GameEvent.TELEPORT_FROM_1, FromRoom1);
        Messenger.RemoveListener(GameEvent.TELEPORT_FROM_2, FromRoom2);
        Messenger<int>.RemoveListener(GameEvent.OPEN_MCQ_POPUP, Open);
        Messenger.RemoveListener(GameEvent.CLOSE_MCQ_POPUP, Close);
        Messenger.RemoveListener(GameEvent.ERROR_INVENTORY_CLOSE_POPUP, CloseErrorInventoryPopup);
        Messenger<string>.RemoveListener(GameEvent.ERROR_INVENTORY, publishErrorForInventory);
        Messenger<string>.RemoveListener(GameEvent.OPTION_SELECTED, checkAnswer);
    }

    private void publishErrorForInventory(string error)
    {
        errorPopup.Open();
        string[] errors = error.Split(' ');
        String errorText = "Please complete the following tasks :-";
        foreach(string err in errors)
        {
            if (err.Equals("ore")){
                errorText += "\n # Collect ores more than 2.";
            }
            if (err.Equals("key")){
                errorText += "\n # Collect keys more than 1.";
            }
            if (err.Equals("health"))
            {
                errorText += "\n # Collect health packs more than 5.";
            }
            if (err.Equals("notEquippedKey")){
                errorText += "\n # Equip the key.";
            }
        }
        errorPopup.GetComponentInChildren<Text>().text = errorText;
    }

    public void checkAnswer(string answer)
    {
        int questionNo = 0;
        string question1CorrectAnswer = "Washington DC";
        string question2CorrectAnswer = "144";
        string question3CorrectAnswer = "iPhone 7";
        if (answer.Equals("Los Angeles") || answer.Equals("New York City") || answer.Equals("Washington DC"))
        {
            questionNo = 1;
        }
        if (answer.Equals("100") || answer.Equals("144") || answer.Equals("24"))
        {
            questionNo = 2;
        }
        if (answer.Equals("iPhone 6S") || answer.Equals("iPhone 6") || answer.Equals("iPhone 7"))
        {
            questionNo = 3;
        }
        if (questionNo == 1)
        {
            if(answer.Equals(question1CorrectAnswer))
            {
                if (teleportFrom == 1)
                {
                    Messenger.Broadcast(GameEvent.TELEPORT_FROM_1);
                    Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
                }
                else if(teleportFrom==2)
                {
                    Messenger.Broadcast(GameEvent.TELEPORT_FROM_2);
                    Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
                }
            }
            else
            {
                Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
                wap.Open();
            }
        }
        if (questionNo == 2)
        {
            if (answer.Equals(question2CorrectAnswer))
            {
                if (teleportFrom == 1)
                {
                    Messenger.Broadcast(GameEvent.TELEPORT_FROM_1);
                    Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
                }
                else if (teleportFrom == 2)
                {
                    Messenger.Broadcast(GameEvent.TELEPORT_FROM_2);
                    Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
                }
            }
            else
            {
                Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
                wap.Open();
            }
        }
        if (questionNo == 3)
        {
            if (answer.Equals(question3CorrectAnswer))
            {
                if (teleportFrom == 1)
                {
                    Messenger.Broadcast(GameEvent.TELEPORT_FROM_1);
                    Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
                }
                else if (teleportFrom == 2)
                {
                    Messenger.Broadcast(GameEvent.TELEPORT_FROM_2);
                    Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
                }
            }
            else
            {
                Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
                wap.Open();
            }
        }
    }
    public void Open(int from)
    {
        teleportFrom = from;
        mcqPopup.Open();
        questionNo = UnityEngine.Random.Range(0,3);
        questionLabel.text = "" + questions[questionNo];
        if (questionNo == 0)
        {
            optionA.GetComponentInChildren<Text>().text = "Los Angeles";
            optionB.GetComponentInChildren<Text>().text = "Washington DC";
            optionC.GetComponentInChildren<Text>().text = "New York City";
        }
        if (questionNo == 1)
        {
            optionA.GetComponentInChildren<Text>().text = "100";
            optionB.GetComponentInChildren<Text>().text = "144";
            optionC.GetComponentInChildren<Text>().text = "24";
        }
        if (questionNo == 2)
        {
            optionA.GetComponentInChildren<Text>().text = "iPhone 6S";
            optionB.GetComponentInChildren<Text>().text = "iPhone 6";
            optionC.GetComponentInChildren<Text>().text = "iPhone 7";
        }
    }
    public void Close()
    {
        mcqPopup.Close();
    }
    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    private void FromRoom1()
    {
        room = 2;
        SceneManager.UnloadScene(1);
        SceneManager.LoadScene(2);
        
        //player.gameObject.transform.position = new Vector3(GameObject.Find("Destination2").transform.position.x, GameObject.Find("Destination2").transform.position.y, GameObject.Find("Destination2").transform.position.z);
        UpdateRoom();
    }
    private void FromRoom2()
    {
        room = 1;
        player.transform.position = new Vector3(GameObject.Find("Destination1").transform.position.x, GameObject.Find("Destination1").transform.position.y, GameObject.Find("Destination1").transform.position.z);
        UpdateRoom();
    }
    void UpdateRoom()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            roomLabel.text = "Level : 1";
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            roomLabel.text = "Level : 2";
        }
    }
    void UpdateScore()
    {
        scoreLabel.text = "Score : " + score;
    }
    public void OnOpenSettings()
    {
        settingsPopup.Open();
    }

    private void OnEnemyHit()
    {
        score += 1;
        UpdateScore();
    }
}