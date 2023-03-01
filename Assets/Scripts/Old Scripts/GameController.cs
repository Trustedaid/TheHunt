using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [Header("Enemy Settings")] public GameObject[] enemies;
    public GameObject[] spawnPoints;
    public GameObject[] targetPoints;
    public float enemySpawnTime = 2f;

    [Header("Health Settings")] 
    public Image HealthBar;
    private float _playerHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.fillAmount = 0.75f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Attacked to Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}