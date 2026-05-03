using UnityEngine;
using UnityEngine.AI;


public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTargetPosition(Vector3 target)
    {
        if (agent != null)
        {
            agent.SetDestination(target);
        }
    }

    public bool HasReachedTarget()
    {
        if (agent == null)
        return false;

        if (agent.pathPending)
        return false;

        return agent.remainingDistance <= agent.stoppingDistance;
    }
    

    }


