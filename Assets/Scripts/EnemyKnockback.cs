using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 20f;
    [SerializeField] private float knockbackDuration = 0.5f;
    [SerializeField] private float knockbackHeight = 0.3f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovementController player = other.gameObject.GetComponent<PlayerMovementController>();

            if (player != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                direction.y = knockbackHeight;
                player.ApplyKnockback(direction, knockbackForce, knockbackDuration);
            }
        }

    }
    
}
