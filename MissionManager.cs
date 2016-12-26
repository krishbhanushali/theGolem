using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int curLevel { get; private set; }
    public int maxLevel { get; private set; }
    public void Startup()
    {
        Debug.Log("Mission manager starting...");
        UpdateData(0, 1);
        status = ManagerStatus.Started;
    }
    public void UpdateData(int curLevel, int maxLevel)
    {
        this.curLevel = curLevel;
        this.maxLevel = maxLevel;
    }
    public void ReachObjective()
    {
        // could have logic to handle multiple objectives
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    }
    public void GoToNext()
    {
        if (curLevel < maxLevel)
        {
            curLevel++;
            string name = "Level" + curLevel;
            Debug.Log("Loading " + name);
            //Application.LoadLevel(name);
            SceneManager.LoadScene(curLevel);
        }
        else
        {
            Debug.Log("Last level");
            Messenger.Broadcast(GameEvent.GAME_COMPLETE);
        }
    }
    public void RestartCurrent()
    {
        string name = "Level" + curLevel;
        Debug.Log("Loading " + name);
        //Application.LoadLevel(name);
        SceneManager.LoadScene(curLevel);
    }
}