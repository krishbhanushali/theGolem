using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class nav : MonoBehaviour {

    // Use this for initialization
    
    private NavMeshAgent agent;
    private GameObject player;
    private NavMeshHit navHit;
    private float wanderRange = 50 ;
    private Vector3 wanderTarget;
    private float checkRate;
    private float nextCheck;
    public bool alive = true;
    [SerializeField]
    private GameObject fireballPrefab;
    [SerializeField]
    private GameObject actualFireballPrefab;

    private GameObject _fireball;
    private GameObject actualFireball;

    [SerializeField]
    private GameObject head;
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        checkRate = UnityEngine.Random.Range(0.3f,0.4f);
        
    }
	
    public void setDead()
    {
        alive = false;
    }

    public bool isAlive()
    {
        return alive;
    }
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        
        if (distance < 10.0f)
        {
            
            attack();
        }
        else if (distance < 4.0f)
        {
            stayIdle();
        }
        else if (distance > 10.0f)
        {

            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                checkIfIShouldWander();
            }
        }
    }

    private void stayIdle()
    {
        agent.GetComponent<Animation>().CrossFade("creature1Idle", 1.0f);
    }

    private void attack()
    {
        transform.LookAt(player.transform);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            agent.GetComponent<Animation>().CrossFade("creature1roar", 1.0f);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            agent.GetComponent<Animation>().CrossFade("attack_2",1.0f);
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (_fireball == null)
            {
                Managers.Audio.PlaySound(Resources.Load("Music/creatureRoar") as AudioClip);

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
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Managers.Audio.PlaySound(Resources.Load("Music/boss") as AudioClip);
            if (_fireball == null)
            {
                Managers.Audio.PlaySound(Resources.Load("Music/creatureRoar") as AudioClip);
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

    public static Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        Vector3 randDirection = origin + UnityEngine.Random.insideUnitSphere * dist;
        NavMeshHit navHit;
        if(NavMesh.SamplePosition(randDirection, out navHit, 1.0f , NavMesh.AllAreas))
        {
            return navHit.position;
        }
        return origin;
    }

    void checkIfIShouldWander()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            agent.GetComponent<Animation>().CrossFade("creature1walkforward", 1.0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            agent.GetComponent<Animation>().CrossFade("walk", 1.0f);
        }
        
        if (distance < 10.0f)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if(RandomWanderTarget(this.transform.position,wanderRange,out wanderTarget))
            {
                agent.SetDestination(wanderTarget);
            }
        }
    }

    bool RandomWanderTarget(Vector3 centre,float range, out Vector3 result)
    {
        Vector3 randDirection = centre + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randDirection, out navHit, 1.0f, NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = centre;
            return false; 
        }
        
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}
