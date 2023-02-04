using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    public Weapon meleeWeapon;

    public override void PerformAttack()
    {
        meleeWeapon.PerformAttack();
    }

}
