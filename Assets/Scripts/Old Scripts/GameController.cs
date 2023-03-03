using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [Header("Enemy Settings")] public GameObject[] enemies;
    public GameObject[] spawnPoints;
    public GameObject[] targetPoints;
    public float enemySpawnTime = 2f;

   
    [Header("Health Settings")]
    public Image healthBar;
    public float playerHealth = 100f;

    [Header("UI Settings")] public GameObject GameOverCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
            
        StartCoroutine(EnemySpawn());
        
    }

    private IEnumerator EnemySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnTime);

            var enemy = Random.Range(0, enemies.Length);
            var spawnPoint = Random.Range(0, spawnPoints.Length);
            var targetPoint = Random.Range(0, targetPoints.Length);


            var target = Instantiate(enemies[enemy], spawnPoints[spawnPoint].transform.position, Quaternion.identity);

            target.GetComponent<EnemyController>().FindTarget(targetPoints[targetPoint]);
        }
    }

    void Update()
    {
    }
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;

        healthBar.fillAmount = playerHealth / 100;

        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("You Are Death"); // TODO: GAME OVER Screen popup
        Cursor.lockState = CursorLockMode.None;
        GameOverCanvas.SetActive(true);
        // Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
}