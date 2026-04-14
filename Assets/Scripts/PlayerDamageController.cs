using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageController : MonoBehaviour
{

    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private Scrollbar healthBar;

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy Collision");
        playerHealth -= damage;
        healthBar.size = playerHealth/100f;
        if (playerHealth <= 0)
         {
            Debug.Log("Player died");
         }
            
    
    }
    

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
}
