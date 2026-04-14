using UnityEngine;

public class EnemyBehaviors : MonoBehaviour
{
    public enum EnemyState
    {
        Patrolling, Chasing
    }

    [Header("Patrol Settings")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTime = 2f;

    [Header("References")]
    [SerializeField] private EnemyMovementController movement;
    [SerializeField] private EnemyPlayerDetection detection;

    private  EnemyState currentState;

    private int currentPatrolIndex = 0;
    private float waitTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = EnemyState.Patrolling;

        detection.OnPlayerDetected += StartChasing;
        detection.OnPlayerLost += StopChasing;

        MoveToNextPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
            HandlePatrolling();
            break;

            case EnemyState.Chasing:
            HandleChasing();
            break;
        }
    }

    void HandlePatrolling()
    {
        if (movement.HasReachedTarget())
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= waitTime)
            {
                MoveToNextPatrolPoint();
                waitTimer = 0f;
            }
        }
    }

    void HandleChasing()
    {
        Vector3 playerPosition = detection.GetPlayerPosition();
        movement.SetTargetPosition(playerPosition);
    }

    void MoveToNextPatrolPoint()
    {
        if(patrolPoints.Length == 0)
        {
            return;
        }

        Vector3 target = patrolPoints[currentPatrolIndex].position;
        movement.SetTargetPosition(target);

        currentPatrolIndex++;

        if (currentPatrolIndex >= patrolPoints.Length)
            currentPatrolIndex = 0;
    }

    void StartChasing()
    {
        currentState = EnemyState.Chasing;
    }

    void StopChasing()
    {
        currentState = EnemyState.Patrolling;
        MoveToNextPatrolPoint();
    }
}
