using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private GameObject[] _enemy;
    public int count;
    public int count1;
    public int i;
    private GameObject spawnPosition;
    private GameObject spawnPositionForLevel2;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { 
            count = Random.Range(2, 4);
            count1 = count;
            _enemy = new GameObject[20];
            spawnPosition = GameObject.Find("destination");
            spawnPositionForLevel2 = GameObject.Find("SpawnPosition");
            float positionForX = 1.0f;
            float positionForY = 1.0f;
        
            for (i = 0; i < count1; i++)
            {

                positionForX = positionForX * Random.Range(1, 4);
                positionForY = positionForY * Random.Range(1, 4);
                _enemy[i] = Instantiate(enemyPrefab) as GameObject;
                _enemy[i].GetComponent<NavMeshAgent>().enabled = false;
                _enemy[i].transform.position = new Vector3(spawnPosition.transform.position.x+positionForX, spawnPosition.transform.position.y, spawnPosition.transform.position.z+positionForY);
                float angle = Random.Range(0, 360);
                _enemy[i].transform.Rotate(0, angle, 0);
                _enemy[i].GetComponent<NavMeshAgent>().enabled = true;

            }
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            float positionForX = 1.0f;
            float positionForY = 1.0f;
            for(int i = 0; i < 10; i++)
            {
                positionForX = positionForX * Random.Range(1, 4);
                positionForY = positionForY * Random.Range(1, 4);
                _enemy[i] = Instantiate(enemyPrefab) as GameObject;
                _enemy[i].GetComponent<NavMeshAgent>().enabled = false;
                _enemy[i].transform.position = new Vector3(spawnPositionForLevel2.transform.position.x + positionForX, spawnPositionForLevel2.transform.position.y, spawnPositionForLevel2.transform.position.z + positionForY);
                float angle = Random.Range(0, 360);
                _enemy[i].transform.Rotate(0, angle, 0);
                _enemy[i].GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }
}