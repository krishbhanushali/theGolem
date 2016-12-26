

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }
   
   
    public int minHealth { get; private set; }
    public int score { get; private set; }
    //private NetworkService _network;

    public void Startup()
    { //(NetworkService service) {
        Debug.Log("Player manager starting...");

        //_network = service;
        health = 50;
        maxHealth = 100;
        score = 0;
        UpdateData(50, 100);

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        status = ManagerStatus.Started;
    }

    public void UpdateData(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }

    public void ChangeHealth(int value)
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }
        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
    }

    public void Respawn()
    {
        UpdateData(50, 100);
    }

    public void IncreaseHealth(int value)
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }
        Debug.Log("Health: " + health + "/" + maxHealth);
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }
    public void decreaseHealth(int value)
    {
        health -= value;
        if (health < minHealth)
        {
            health = minHealth;
        }
    }

    public void IncreaseScore()
    {
        score += 1;
        Debug.Log("Score in Player Manager :- " + score);
    }
}
