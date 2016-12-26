using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController1 : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    public GameObject[] enemies;
    public int amount;
    private Vector3 spawnPoint;
    public int count;
    public int i;
    private GameObject spawnPosition;
    // Update is called once per frame
    void Update()
    {
            enemies = new GameObject[10];
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            amount = enemies.Length;
            spawnPosition = GameObject.Find("destination");
            if (amount < 8)
            {
                InvokeRepeating("spawnEnemy", 10, 15f);
            }
            if (amount > 9)
            {
                Destroy(enemyPrefab.gameObject);
            }
        
    }
    void spawnEnemy()
    {
        spawnPoint.x = Random.Range(-10, 15);
        spawnPoint.y = 0.2f;
        spawnPoint.z = Random.Range(-10, 15);
        count = Random.Range(1, 2);
        float positionForX = 1.0f;
        float positionForY = 1.0f;
        for (i = 0; i <= count; i++)
        {
            positionForX = positionForX * Random.Range(1,4);
            positionForY = positionForY * Random.Range(1, 4);
            enemies[i] = Instantiate(enemyPrefab) as GameObject;
            enemies[i].GetComponent<NavMeshAgent>().enabled = false;
            enemies[i].transform.position = new Vector3(spawnPosition.transform.position.x+positionForX, spawnPosition.transform.position.y, spawnPosition.transform.position.z+positionForY);
            float angle = Random.Range(0, 180);
            enemies[i].transform.Rotate(0, angle, 0);
            enemies[i].GetComponent<NavMeshAgent>().enabled = true;
        }
        CancelInvoke();
    }
}