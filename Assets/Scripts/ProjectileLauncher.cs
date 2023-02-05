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

    private Transform player;
    private CharacterController playerController;
    public bool aimAtPLayer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
    }
    public override void PerformAttack(Vector3? direction)
    {
        if (isAttacking) return; 
        isAttacking = true;
        GameObject ball = Instantiate(projectile, transform.position, Quaternion.FromToRotation(transform.position, player.position));

        Vector3 playerVelocity = Vector3.zero;
        if (aimAtPLayer) playerVelocity = playerController.velocity;
        var velocity = (direction.Value - transform.position).normalized * launchVelocity + playerVelocity;
        velocity.y = 0;
        ball.GetComponent<Rigidbody>().velocity = velocity;
        StartCoroutine(nameof(FinishAttack));
    }

    private IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }
}
