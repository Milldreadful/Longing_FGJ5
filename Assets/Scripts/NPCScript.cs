using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public float changeDestinationInterval = 2.0f; // Time between changing destination

    private float timer;
    public float npcRadius = 50f;
    public GameObject sadNobbin;
    public GameObject happyNobbin;
    public bool isHappy = true;
    public float happinessTime;
    public float happinessCounter;

    public GameManager gm;
    public AudioSource ghostHugAudio;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomDestination();

        if(isHappy)
        {
            happinessTime = Random.Range(30f, 80f);
            sadNobbin.SetActive(false);
            happyNobbin.SetActive(true);
        }
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

        if (isHappy)

            happinessCounter += Time.deltaTime;
            
                if (happinessTime < happinessCounter)
                {
                    gm.AddSadness();
                    isHappy = false;
                    sadNobbin.SetActive(true);
                    happyNobbin.SetActive(false);
                    happinessCounter = 0;
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isHappy)
        {
            ghostHugAudio.Play();
            sadNobbin.SetActive(false);
            happyNobbin.SetActive(true);
            isHappy = true;
            gm.AddHappiness();

            happinessTime = Random.Range(50f, 1200f);
        }
    }
}
