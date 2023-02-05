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

    public Transform startPos;
    public float delay = 0f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
    }
    public override void PerformAttack(Vector3? direction)
    {
        if (isAttacking) return; 
        isAttacking = true;

        StartCoroutine(FinishAttack(direction));
    }

    private IEnumerator FinishAttack(Vector3? direction)
    {
        if (delay > 0f)
        {
            yield return new WaitForSeconds(delay);
        }

        var movingPos = startPos == null ? transform.position : startPos.position;
        GameObject ball = Instantiate(projectile, movingPos, Quaternion.FromToRotation(movingPos, player.position));

        Vector3 playerVelocity = Vector3.zero;
        if (aimAtPLayer) playerVelocity = playerController.velocity;
        var velocity = (direction.Value - movingPos).normalized * launchVelocity + playerVelocity;
        velocity.y = 0;
        ball.GetComponent<Rigidbody>().velocity = velocity;
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }
}
