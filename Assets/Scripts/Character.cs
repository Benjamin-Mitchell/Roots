using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public abstract void TakeDamage(int amount);
    public abstract void PerformAttack();
    public abstract void Die();

    public abstract void Knockback(Vector3 direction);
}
