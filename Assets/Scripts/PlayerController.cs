using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Character
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;
    //public Animator anim;
    public float rotateSpeed;

    public float knockBackForce;
    public float knockBackTime;

    public Weapon meleeWeapon;
    public Weapon rangedWeapon;
    public Weapon shield;


    private float knockBackCounter;
    private Quaternion lastRotation;

    private bool hasMoved = false;


    private Camera mainCamera;
    private int currentHealth;

	private void Awake()
	{
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
	// Start is called before the first frame update
	void Start()
    {
        controller = GetComponent<CharacterController>();

        mainCamera = FindObjectOfType<Camera>();
        lastRotation = transform.rotation;
        currentHealth = maxHealth;
    }

    // plane degined by p (p.xyz must be normalized)
    private float plaIntersect(Vector3 ro, Vector3 rd, Vector4 p)
    {
        Vector3 pxyz = new Vector3(p.x, p.y, p.z);
        return -(Vector3.Dot(ro, pxyz) + p.w) / Vector3.Dot(rd, pxyz);
        //return -(dot(ro, p.xyz) + p.w) / dot(rd, p.xyz);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            moveSpeed = 200f;
        }
        else { moveSpeed = 7f; }

        moveDirection = (mainCamera.transform.forward * Input.GetAxis("Vertical")) + (mainCamera.transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;


        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
        {
            PerformAttack();
        }
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            hasMoved = true;
            var rotate = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;
            lastRotation = Quaternion.LookRotation(rotate);
            transform.rotation = lastRotation;
        }
        else if(hasMoved)
        {
            transform.rotation = lastRotation;
            hasMoved = false;
        }
    }

    public override void Knockback(Vector3 direction)
    {
        //knockBackCounter = knockBackTime;
        //moveDirection = direction * knockBackForce;
        //moveDirection.y = knockBackForce;
        //controller.Move(moveDirection * Time.deltaTime);
    }

    public override void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    public override void PerformAttack()
    {
        hasMoved = true;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float t = plaIntersect(ray.origin - new Vector3(0.0f, transform.position.y, 0.0f), ray.direction, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));

        Vector3 hitPoint = ray.origin + ray.direction * t;

        transform.LookAt(hitPoint, Vector3.up);
        lastRotation = transform.rotation;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            meleeWeapon.PerformAttack();
        }
        else
        {
            rangedWeapon.PerformAttack(hitPoint);
        }
    }

    public override void Die()
    {
        //Die
    }
}
