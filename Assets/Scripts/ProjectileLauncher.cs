using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class ProjectileLauncher : Weapon
{
    public GameObject projectile;
    public float launchVelocity = 700f;
    public float attackTime;

    private bool isAttacking;
    private Transform player;
    private CharacterController playerController;
    //private Vector3 offset;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        //offset = transform.position - player.position;
        playerController = GameObject.Find("Player").GetComponent<CharacterController>();
    }
    public override void PerformAttack(Vector3? direction)
    {
        if (isAttacking) return; 
        isAttacking = true;
        GameObject ball = Instantiate(projectile, transform.position, player.rotation);

        var velocity = (direction.Value - transform.position).normalized * launchVelocity + playerController.velocity;
        ball.GetComponent<Rigidbody>().velocity = velocity;
        StartCoroutine(nameof(FinishAttack));
    }

    private IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }
}
