using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReactiveTarget : MonoBehaviour
{
    public GameObject enemy;
    public GameObject boss;
    private GameObject potion;
    [SerializeField]
    private GameObject potionPrefab;
    int bulletsHit = 0;
    int numberOfBossKilled = 0;
    int bulletsHitToEnemy = 0;
    public void ReactToHit()
    {
        nav behaviour = GetComponent<nav>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (behaviour.isAlive())
            {
                StartCoroutine(Die());
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            bulletsHitToEnemy++;
            if (bulletsHitToEnemy >= 2)
            {
                if (behaviour.isAlive())
                {
                    StartCoroutine(Die());
                }
            }
        }
     }

    public void BossReactToHit()
    {
        nav behaviour = GetComponent<nav>();
        if (bulletsHit > 5)
        {
            StartCoroutine(Die());
            numberOfBossKilled++;
            if(numberOfBossKilled == 2)
            {
                Messenger.Broadcast(GameEvent.ALL_BOSS_KILLED);
            }
        }
        else
            bulletsHit++;
    }

    private IEnumerator Die()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            this.gameObject.GetComponent<nav>().setDead();
            Messenger.Broadcast(GameEvent.ENEMY_HIT);
            Managers.Player.IncreaseScore();
            Managers.Audio.PlaySound(Resources.Load("Music/creatureDying") as AudioClip);
            enemy.GetComponent<Animation>().CrossFade("creature1Die", 3.0f);
            //  yield return new WaitForSeconds(1.0f); 

            Vector3 position = this.gameObject.transform.position;
            Quaternion rotation = this.gameObject.transform.rotation;
            Destroy(this.gameObject, 1.0f);

            yield return new WaitForSeconds(0.3f);
            potion = Instantiate(potionPrefab, position, rotation) as GameObject;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            this.gameObject.GetComponent<nav>().setDead();
            Messenger.Broadcast(GameEvent.ENEMY_HIT);
            Managers.Player.IncreaseScore();
            Managers.Audio.PlaySound(Resources.Load("Music/creatureDying") as AudioClip);
            enemy.GetComponent<Animation>().CrossFade("death", 3.0f);
            //  yield return new WaitForSeconds(1.0f); 

            Vector3 position = this.gameObject.transform.position;
            Quaternion rotation = this.gameObject.transform.rotation;
            Destroy(this.gameObject, 1.0f);

            yield return new WaitForSeconds(0.3f);
            potion = Instantiate(potionPrefab, position, rotation) as GameObject;
        }
    }
}
