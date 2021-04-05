using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float health;
    private void Start()
    {
        health = maxHealth;
    }
    public void DoDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);
    }
}
