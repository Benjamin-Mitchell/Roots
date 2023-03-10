using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage; 
    public bool isAttacking = false;
    public abstract void PerformAttack(Vector3? direction = null);

}
