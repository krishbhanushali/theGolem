using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;


    [SerializeField]
    private GameObject fireballPrefab;
    [SerializeField]
    private GameObject actualFireballPrefab;
    private GameObject actualFireball;
    private GameObject _fireball;
    private GameObject Player;
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    private bool _alive;
    float tempSpeed = 3.0f;
    NavMeshAgent agent;

    [SerializeField]
    private GameObject head;

    public GameObject enemy;

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.ENEMY_SPEED_CHANGED,
        ChangeSpeed);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.ENEMY_SPEED_CHANGED,
         ChangeSpeed);
    }

    void Start()
    {
        _alive = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        tempSpeed = speed;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //tempSpeed = 3.0f;
        if (_alive)
        {

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            if (distance < 10.0f)
            {
                agent.SetDestination(Player.transform.position);
                tempSpeed = 0f;
                transform.LookAt(Player.transform);
                transform.Translate(0, 0, tempSpeed * Time.deltaTime);
            }
            else
            {
                //speed = tempSpeed;
                transform.Translate(0, 0, speed * Time.deltaTime);
                if (speed < 9.0f)
                {
                    enemy.GetComponent<Animation>().CrossFade("creature1walkforward", 1.0f);
                }
                else
                {
                    enemy.GetComponent<Animation>().CrossFade("creature1run", 0.3f);
                }
            }
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (distance < 10.0f)
                    {
                        agent.SetDestination(hitObject.transform.position);
                        enemy.GetComponent<Animation>().CrossFade("creature1roar", 1.0f);
                        if (_fireball == null)
                        {
                            _fireball = Instantiate(fireballPrefab) as GameObject;
                            _fireball.transform.position = head.transform.TransformPoint(Vector3.forward * 2f);
                            _fireball.transform.rotation = head.transform.rotation;
                        }
                        if (actualFireball == null)
                        {
                            actualFireball = Instantiate(actualFireballPrefab) as GameObject;
                            actualFireball.transform.position = head.transform.TransformPoint(Vector3.forward * 2f);
                            actualFireball.transform.rotation = head.transform.rotation;
                        }
                    }
                }
            }
            else if (hit.distance < obstacleRange)
            {
                if (distance < 10f)
                {
                    transform.LookAt(Player.transform);
                    enemy.GetComponent<Animation>().CrossFade("creature1roar", 0.3f);
                }
                else
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                    enemy.GetComponent<Animation>().CrossFade("creature1walkforward", 1.0f);
                }

            }
        }
    }
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    public void ChangeSpeed(float value)
    {
        speed = value;
    }
}
