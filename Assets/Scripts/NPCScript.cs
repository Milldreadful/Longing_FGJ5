using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{
    /*public float movementSpeed = 3f;
    public float changeDirectionInterval = 2f;

    private Vector3 targetPos;
    private float timer;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeDestaination();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            timer += Time.deltaTime;

            if(timer >= changeDirectionInterval)
            {
                RandomizeDestaination();
                timer = 0;
            }
        }
    }

    void RandomizeDestaination()
    {
        float newX = Random.Range(-50f, 50f);
        float newZ = Random.Range(-50f, 50f);
        targetPos = new Vector3(newX,transform.position.y, newZ);
    }*/

    private NavMeshAgent navMeshAgent;
    public float changeDestinationInterval = 2.0f; // Time between changing destination

    private float timer;
    public int maxHappiness = 20;
    public int currentHappiness;
    public float npcRadius = 50f;
    public GameObject sadNobbin;
    public GameObject happyNobbin;
    public bool isHappy = true;

    public HappinessBarScript happinessBarScript;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        happinessBarScript.UpdateHappiness(maxHappiness, currentHappiness);
        SetRandomDestination();
    }

    void Update()
    {
        // Check if the NPC has reached the current destination
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            timer += Time.deltaTime;
            if (timer >= changeDestinationInterval)
            {
                SetRandomDestination();
                timer = 0;
            }
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomPoint = GetRandomPointInNavMesh();
        navMeshAgent.SetDestination(randomPoint);
    }

    Vector3 GetRandomPointInNavMesh()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(Random.insideUnitSphere * npcRadius, out hit, 10, NavMesh.AllAreas);
        return hit.position;
    }

    private void OnCollisionExit(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("Player") && isHappy)
        {
            sadNobbin.SetActive(true);
            happyNobbin.SetActive(false);
            isHappy = false;
            currentHappiness -= 5;
        }*/

        if (collision.gameObject.CompareTag("Player") && !isHappy)
        {
            sadNobbin.SetActive(false);
            happyNobbin.SetActive(true);
            isHappy = true;
            currentHappiness -= 5;
            happinessBarScript.UpdateHappiness(maxHappiness, currentHappiness);
        }

    }
}
