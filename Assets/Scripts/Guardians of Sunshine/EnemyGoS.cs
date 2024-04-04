using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoS : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int totalHealth = 2;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        Debug.Log("Taking " + damageTaken + " points of damage");
        currentHealth -= damageTaken;
        if (currentHealth < 1)
            Destroy(this.gameObject);
        Debug.Log("Health Remaining: " + currentHealth);
    }
}
