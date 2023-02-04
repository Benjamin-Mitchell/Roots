using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private Animator animator;
    public bool isAttacking = false;
    private float attackTime;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public override void PerformAttack()
    {
        if (isAttacking) return;
        isAttacking = true;
        animator.SetTrigger("Attack");
        StartCoroutine("FinishAttack");
    }

    private IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(attackTime / 2);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackTime / 2);
        isAttacking = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateAnimClipTimes();

    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "SwordAttack":
                    attackTime = clip.length;
                    break;
            }
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag != "Enemy" || !isAttacking) return;
    //    other.GetComponent<Enemy>().TakeDamage(damage);
    //}

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
