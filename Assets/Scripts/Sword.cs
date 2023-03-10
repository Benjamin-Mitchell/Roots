using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Sword : Weapon
{
    private Animator animator;

    public float attackTime = 1f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public VisualEffect slash;

    public override void PerformAttack(Vector3? direction = null)
    {
        if (isAttacking) return;
        isAttacking = true;
        if(animator != null) animator.SetTrigger("Attack");
        if(slash != null)
            slash.Play();
        StartCoroutine("FinishAttack");
    }

    private IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(attackTime / 4);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (var enemy in hitEnemies)
        {
            var direction = this.GetComponentInParent<Transform>().forward;
            enemy.GetComponent<Character>().Knockback(direction);
            enemy.GetComponent<Character>().TakeDamage(damage);
        }

        yield return new WaitForSeconds((3 * attackTime) / 4);
        isAttacking = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //UpdateAnimClipTimes();

    }

    //public void UpdateAnimClipTimes()
    //{
    //    AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
    //    foreach (AnimationClip clip in clips)
    //    {
    //        switch (clip.name)
    //        {
    //            case "SwordAttack":
    //                attackTime = clip.length;
    //                break;
    //        }
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
