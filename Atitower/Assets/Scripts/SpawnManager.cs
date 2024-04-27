using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform[] spawnPoints; 
    public Transform playerTransform; 
    public float enemySpeed = 5f; 
    [SerializeField] private float SpawnInterval;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private int enemiesCount;

    //[SerializeField] private int waveNum;

    void Start()
    {
        StartCoroutine(SpawnEnemies(numberOfEnemies, SpawnInterval));
        enemiesCount = numberOfEnemies;
    }

    void Update()
    {
        MoveEnemiesTowardsPlayer();
    }

    private IEnumerator SpawnEnemies(int numberOfEnemies, float SpawnInterval)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Escolhe um spawn point aleatório
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instancia o inimigo no spawn point escolhido
            GameObject enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
            // Passa a referência do transform do jogador para os inimigos
            enemy.GetComponent<Enemy>().playerTransform = playerTransform;
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    public void EnemyCounter(int decreaseCount)
    {
        enemiesCount += decreaseCount;
        if (enemiesCount == 0)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                playerController.Win();
            }
            else
            {
                Debug.LogError("PlayerController não encontrado na cena!");
            }
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
