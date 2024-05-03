using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] public float maxHealth;
    public float currentHealth;

    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    [SerializeField] float atkSpeed;
    public float addAtkSpeed;
    private float shootTimer;

    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI gameWinText;
    [SerializeField] TextMeshProUGUI atkSpeedText;

    [Header("Canvas")]
    [SerializeField] private GameObject canvaStats;
    [SerializeField] private GameObject canvaUpgradeButtons;

    [Header("Anima��o fogo")]
    [SerializeField] private GameObject firePower;

    [Header("Projectile Audio")]
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioSource audioSourceShot;


    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealth();
        UpgradeAtkSpeed(0f);
        canvaStats.SetActive(true);
        canvaUpgradeButtons.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealth();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        healthText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        canvaStats.SetActive(false);
        canvaUpgradeButtons.SetActive(false);
        Time.timeScale = 0;
        //Debug.Log("Player morreu!");
    }

    public void Win()
    {
        healthText.gameObject.SetActive(false);
        gameWinText.gameObject.SetActive(true);
        canvaStats.SetActive(false);
        canvaUpgradeButtons.SetActive(false);
        Time.timeScale = 0;
    }

    private void Update()
    {
        // Decrementa o timer a cada quadro
        shootTimer -= Time.deltaTime;

        // Se o timer chegar a zero ou menos, atira e reseta o timer
        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = 1f / atkSpeed; // Define o tempo entre os disparos com base na velocidade de ataque
        }

        {
            // Ajustar a escala do gameObject firePower com base na vari�vel attackSpeed
            float scaleFactor = CalculateScaleFactor();
            Vector3 newScale = Vector3.one * scaleFactor;
            firePower.transform.localScale = newScale;
        }
    }

    float CalculateScaleFactor()
    {
        // L�gica para calcular o fator de escala com base na vari�vel attackSpeed
        // Aqui, voc� pode implementar sua l�gica espec�fica para ajustar a escala
        // com base na velocidade de ataque atual do seu jogo
        // Este � um exemplo simples para demonstra��o
        float scaleFactor = 1f + atkSpeed * 0.2f; // Ajuste conforme necess�rio
        return scaleFactor;
    }

    private void Shoot()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            GameObject nearestEnemy = null;
            float nearestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
            {
                Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * projectileSpeed;
                }

                if (shotSound != null && audioSourceShot != null)
                {
                    audioSourceShot.clip = shotSound;
                    audioSourceShot.Play();
                }
            }
        }
    }

    private void UpdateHealth()
    {
        //Debug.Log(currentHealth);
        healthText.text = "Health: " + currentHealth.ToString();
        //Debug.Log("update health");
    }


    public void UpgradeAtkSpeed(float addAtkSpeed)
    {
        atkSpeed = Mathf.Clamp(atkSpeed += addAtkSpeed, 1f, 5f);
        //atkSpeed += addAtkSpeed;
        atkSpeedText.text = "ATK SPEED: " + atkSpeed.ToString("F1");
        
    }
}
