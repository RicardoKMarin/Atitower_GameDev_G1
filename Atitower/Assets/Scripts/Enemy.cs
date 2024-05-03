using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform; // Transform do jogador

    public float maxHealth = 20;
    private float currentHealth;

    [SerializeField] public float damage;

    [Header("DAMAGE_TIMER")]
    [SerializeField] float dmgTick;
    private float dmgTimer;
    private bool dmgAllow;

    private void Start()
    {
        currentHealth = maxHealth;
        dmgAllow = true;
    }

    void Update()
    {
        // Decrementa o timer a cada quadro
        dmgTimer -= Time.deltaTime;

        // Se o timer chegar a zero ou menos, atira e reseta o timer
        if (dmgTimer <= 0)
        {
            dmgAllow = true;
            dmgTimer = 1f / dmgTick; // Define o tempo entre os disparos com base na velocidade de ataque
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Lógica de morte do jogador, como reiniciar a cena ou exibir uma tela de game over
        SpawnManager smanager = FindObjectOfType<SpawnManager>();
        smanager.EnemyCounter(-1);
        Destroy(this.gameObject);
    }


    /*private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && dmgAllow)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            dmgAllow = false;
        }
    }*/

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && dmgAllow)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            dmgAllow = false;
        }
    }
}
