using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target; // Kirna

    [Header("Rangos")]
    public float visionRange = 10f;
    public float walkRange = 8f;

    private Vector3 walkPoint;
    private bool walkPointSet;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform; 
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= visionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(target.position);
    }

    void Patrol()
    {
        if (!walkPointSet) FindWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        if (Vector3.Distance(transform.position, walkPoint) < 1f)
            walkPointSet = false;
    }

    void FindWalkPoint()
    {
        float randomX = Random.Range(-walkRange, walkRange);
        float randomZ = Random.Range(-walkRange, walkRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        walkPointSet = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}