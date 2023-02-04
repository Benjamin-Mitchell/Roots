using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    private float knockBackCounter;
    private Quaternion lastRotation;

    private bool hasMoved = false;


    private Camera mainCamera;

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
    }


    private void Update()
    {
        moveDirection = (mainCamera.transform.forward * Input.GetAxis("Vertical")) + (mainCamera.transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;


        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            hasMoved = true;
            var mousePos = Input.mousePosition;
            var mousePosNormalised = new Vector3(mousePos.x - (Screen.width / 2), mousePos.y - (Screen.height / 2), mousePos.z).normalized;
            var angle = GetAngle(mousePosNormalised) + 45;

            transform.eulerAngles = new Vector3(0, angle, 0);
            lastRotation = transform.rotation;
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

    float GetAngle(Vector3 a)
    {
        var angle = Mathf.Atan(a.x / a.y) * Mathf.Rad2Deg;
        if (a.y < 0) angle += 180;
        return angle;
    }
    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }

}
