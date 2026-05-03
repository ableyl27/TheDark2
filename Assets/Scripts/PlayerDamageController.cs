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

    [SerializeField] private AudioClip takeDamage;
    [SerializeField] private AudioSource audioSource;

    [Header("Healing")]
    [SerializeField] private float healRate = 5f;
    [SerializeField] private float healInterval = 1f;
    [SerializeField] private float healDelay = 3f;

    public DamageFlashUI damageFlashUI;

    private Coroutine healCoroutine;

    public PauseMenu pauseMenu;

    

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
        //Debug.Log("Enemy Collision");
        playerHealth -= damage;
        damageFlashUI.TriggerDamageFlash();
        Debug.Log("Health: " + playerHealth);
        audioSource.PlayOneShot(takeDamage);
        healthBar.size = playerHealth/100f;
        if (playerHealth <= 0)
         {
            Die();
            return;
         }

        if (healCoroutine != null)
            StopCoroutine(healCoroutine);
        healCoroutine = StartCoroutine(HealOverTime());
    
            
    
    }

    private IEnumerator HealOverTime()
    {
        yield return new WaitForSeconds(healDelay);

        while (playerHealth < 100f && !isDead)
    {
        playerHealth = Mathf.Min(playerHealth + healRate, 100f);
        healthBar.size = playerHealth / 100f;
        yield return new WaitForSeconds(healInterval);
    }
}

    void Die()
    {
        pauseMenu.SetGameOver();
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
