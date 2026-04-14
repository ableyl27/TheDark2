using UnityEngine;
using System;

public class EnemyPlayerDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 8f;
    [SerializeField] private float viewDistance = 10f;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform eyePoint;

    [Header("Timing")]
    [SerializeField] private float checkInterval = 0.5f;

    public event Action OnPlayerDetected;
    public event Action OnPlayerLost;

    private bool playerVisible = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(CheckForPlayer), 0f, checkInterval);
    }

    void CheckForPlayer()
    {
        if(player == null)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRadius)
        {
            LosePlayer();
            return;
        }
        Vector3 direction = (player.position - eyePoint.position).normalized;

        Ray ray = new Ray(eyePoint.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, viewDistance))
        {
            if (hit.transform == player)
            {
                SeePlayer();
                return;
            }
        }
        LosePlayer();
    }

    void SeePlayer()
    {
        if (!playerVisible)
        {
            playerVisible = true;
            OnPlayerDetected?.Invoke();
        }
    }

    void LosePlayer()
    {
        if (playerVisible)
        {
            playerVisible = false;
            OnPlayerLost?.Invoke();
        }
    }

    public Vector3 GetPlayerPosition()
    {
        if(player != null)
        {
            return player.position;
        }
        return transform.position;
    }

  
}
