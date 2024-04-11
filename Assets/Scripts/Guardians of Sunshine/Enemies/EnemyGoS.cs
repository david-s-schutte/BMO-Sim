using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoS : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int totalHealth = 2;
    int currentHealth;
    void Start()
    {
        currentHealth = totalHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth < 1)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().AddScore(totalHealth * 500);
            Destroy(this.gameObject);
        }
    }
}
