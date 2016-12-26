using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{
    void Awake()
    {
        Messenger<int>.AddListener(GameEvent.HEALTH_PACK,
        heal);
    }
    void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.HEALTH_PACK,
         heal);
    }
    private int _health;
    [SerializeField]
    public Slider healthBar;

    void Start()
    {
        _health = 100;
        healthBar.value = _health;
    }

    public void Hurt(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
        }
        healthBar.value = _health;
        Managers.Player.decreaseHealth(damage);
    }

    public void heal(int increase)
    {
        _health += increase;
        healthBar.value = _health;
        Managers.Player.IncreaseHealth(increase);
    }

    public void die()
    {
        if (_health == 0)
        {
           // Managers.Mission.LoadGame();
        }
    }
}
