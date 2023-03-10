using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _target;

    
    public float enemyHealth;
    public float enemyDamage;
    


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_target.transform.position);
    }

    public void FindTarget(GameObject target)
    {
        _target = target;
    }

    public void GetDamage(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Attacked to Player");
            
            var mainController = GameObject.FindWithTag("GameController");
            
            mainController.GetComponent<GameController>().TakeDamage(enemyDamage);
         
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}