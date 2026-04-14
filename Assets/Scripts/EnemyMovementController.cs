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
        if (agent.pathPending)
            return false;

        return agent.remainingDistance <= agent.stoppingDistance;
    }
    // private NavMeshAgent agent;
    // [Header("Movement Settings")]
    // [SerializeField] private float moveSpeed = 3f;
    // [SerializeField] private float stoppingDistance = 0.5f;

    // [Header("Target")]
    // [SerializeField] private Vector3 targetPosition;

    // private Rigidbody rigidbody; 
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     rigidbody = GetComponent<Rigidbody>();
    //     agent = 
    // }


    // void FixedUpdate()
    // {
    //     MoveTowardsTarget();
    // }

    // void MoveTowardsTarget()
    // {
    //     Vector3 direction = targetPosition - transform.position;

    //     float distance = direction.magnitude;

    //     if (distance <= stoppingDistance)
    //     {
    //         return;
    //     }

    //     direction = direction.normalized;

    //     Vector3 movePosition = rigidbody.position + direction * moveSpeed * Time.fixedDeltaTime;
    //     rigidbody.MovePosition(movePosition);

    //     if (direction != Vector3.zero)
    //     {
    //         Quaternion rotation = Quaternion.LookRotation(direction);
    //         rigidbody.MoveRotation(rotation);
    //     }
    // }
        
    //     public void SetTargetPosition(Vector3 newTarget){
    //         targetPosition = newTarget;
    //     }

    //     public bool HasReachedTarget()
    // {
    //     float distance = Vector3.Distance(transform.position, targetPosition);

    //     return distance <= stoppingDistance;
    // }


    }


