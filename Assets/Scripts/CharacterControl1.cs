using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl1 : MonoBehaviour
{
    public CharacterController controller;
    public GameObject projectile;
    public Transform barrel;
    public Transform target;

    public float turnSpeed;
    public float runSpeed;
    public float jumpForce;
    public Vector3 move;
    public bool isBlocking;

    public float gravity;
    public float maxGravity;
    public bool doubleJump;

    public LayerMask ground;
    public bool isGrounded;
    public float groundLevel;

    public Animator anim;
    public float x, z;
    public bool facingRight;
    public float angle;
    public float facingDir;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void AimLock()
    {
        barrel.LookAt(target);
    }

    private void AngleCheck()
    {
        Vector3 dir = (transform.position - target.transform.position).normalized;

        angle = Vector3.Angle(-dir, transform.forward);

        if(angle > 90)
        {
            Flip();
        }

        Debug.DrawRay(transform.position, -dir, Color.red);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        AimLock();

        if (Physics.Raycast(transform.position, -transform.up, groundLevel, ground))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Move();
        Attack();

        AngleCheck();
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject shot = Instantiate(projectile, barrel.transform.position, barrel.transform.rotation);
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }

    private void Move()
    {
        z = Input.GetAxis("Horizontal");

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("Z", z);
        anim.SetFloat("FacingDir", facingDir);
        anim.SetBool("facingRight", facingRight);
        //anim.SetFloat("X", x);

        if (z > 0 && facingRight)
        {
            facingDir = -1;
        }
        else if (z < 0 && facingRight)
        {
            facingDir = -1;
        }
        if (z > 0 && !facingRight)
        {
            facingDir = -1;
        }
        else if (z < 0 && !facingRight)
        {
            facingDir = -1;
        }

        if (z == 0)
        {
            move = new Vector3(0f, move.y, 0f);
        }

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
        {
            if (!isGrounded && doubleJump)
            {
                move.y = jumpForce;    
                anim.SetTrigger("Flip");
                doubleJump = false;
            }
            else if (isGrounded)
            {
                move.y = jumpForce;
                anim.SetTrigger("Jump");
                    doubleJump = true;
            }

        }

        if (isGrounded)
        {                       
            gravity = maxGravity;
            //anim.ResetTrigger("Jump");
            anim.ResetTrigger("Flip");
        }
        else
        {
            gravity = maxGravity / 2f;
        }

        move = new Vector3(0f, move.y, z * runSpeed);
        move.y = move.y - (gravity);

        if(facingRight)
            controller.Move(transform.forward * move.z * Time.deltaTime);
        else
            controller.Move(-transform.forward * move.z * Time.deltaTime);

        controller.Move(move.y * transform.up * Time.deltaTime);

        //transform.Rotate(0f, x * turnSpeed, 0f);        

        if (move.y < 0 && isGrounded)
        {
            move.y = 0f;
        }
    }
}
