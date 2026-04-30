using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerDamageController : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private Scrollbar healthBar;
    [SerializeField] private GameObject gameOverMenu;

    

    private bool isDead = false;

    void Start()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(false);
        }
    }

    
    public void TakeDamage(int damage)
    {

        if (isDead)
        {
            return;
        }
        Debug.Log("Enemy Collision");
        playerHealth -= damage;
        healthBar.size = playerHealth/100f;
        if (playerHealth <= 0)
         {
            Die();

         }
            
    
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player died");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (gameOverMenu != null)
        OnPlayerDeath?.Invoke();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
}
