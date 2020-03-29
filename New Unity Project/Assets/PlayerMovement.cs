using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    
    [Header("Basic Movement")]
    public float speed = 10f;
    public float gravity = -10f;
    public float jumpHeight = 3f;
    private float currentJumpHeight;
    private float jumps = 0;

    [Header("Dashing")]
    public bool canDash = true;
    public float dashingTime;
    public float dashSpeed;
    public float dashJump;
    public float timeBTWdashes;

    [Header("GroundChecks")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;


    void Start()
    {
        currentJumpHeight = jumpHeight;
    }

    void Update()
    {
        //BASIC MOVEMENT
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //JUMPING
        if (Input.GetButtonDown("Jump"))
        {
            if (jumps > 0 && isGrounded)
            {
                jumps = 0;
            }
            if (jumps == 0)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                    jumps++;
                }
            else if (jumps == 1)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                    jumps++;
                }

        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //DASHING
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }


    Collider m_ObjectCollider;
    Renderer m_ObjectRenderer;
    void OnTriggerEnter(Collider other)
    {
        //POWER UP INVISIBILITY
        GameObject go = GameObject.Find("PowerUpInvisible");
        PowerUpScript scr = go.GetComponent<PowerUpScript>();

        GameObject goo = GameObject.Find("Gate");
        m_ObjectCollider = goo.GetComponent<Collider>();
        m_ObjectRenderer = goo.GetComponent<Renderer>();
        bool hasPowerUpInvisible = scr.hasPowerUpInvisible;

        if (other.gameObject.tag == "Gate" && hasPowerUpInvisible == true)
        {
               m_ObjectCollider.enabled = false;
               m_ObjectRenderer.enabled = false;
        }
        else if (hasPowerUpInvisible == false)
        {
               m_ObjectCollider.enabled = true;
               m_ObjectRenderer.enabled = true;
        }
    }

    //SPECIAL EFFECT PADS
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "JumpPad":
                jumpHeight = 5f;
                break;
            case "Ground":
                jumpHeight = currentJumpHeight;
                break;
        }
    }


    void DashAbility()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }
    IEnumerator Dash()
    {
        canDash = false;
        speed = dashSpeed;
        jumpHeight = dashJump;
        yield return new WaitForSeconds(dashingTime);
        speed = 10f;
        jumpHeight = 3f;
        yield return new WaitForSeconds(timeBTWdashes);
        canDash = true;
    }

}
