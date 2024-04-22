using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // O prefab do inimigo que você quer instanciar
    public Transform[] spawnPoints; // Array de spawn points onde os inimigos serão instanciados
    public Transform playerTransform; // Transform do jogador
    public float enemySpeed = 5f; // Velocidade dos inimigos

    public int numberOfEnemies = 5; // Número de inimigos a serem instanciados

    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        MoveEnemiesTowardsPlayer();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Escolhe um spawn point aleatório
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instancia o inimigo no spawn point escolhido
            GameObject enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
            // Passa a referência do transform do jogador para os inimigos
            enemy.GetComponent<Enemy>().playerTransform = playerTransform;
        }
    }

    void MoveEnemiesTowardsPlayer()
    {
        // Move cada inimigo em direção ao transform do jogador
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Vector3 direction = (playerTransform.position - enemy.transform.position).normalized;
            enemy.transform.Translate(direction * enemySpeed * Time.deltaTime);
        }
    }
}
