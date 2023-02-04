using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int damage;
    public int maxHealth;
    public abstract void TakeDamage(int amount);
    public abstract void PerformAttack();
    public abstract void Die();
}
