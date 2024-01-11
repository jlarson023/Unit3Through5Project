using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Player")]
    public Rigidbody rb;
    [Header ("Turning")]
    public float turnSpeed;
    public float horizontalInput;
    [Header ("Moving")]
    public float verticalInput;
    public float moveSpeed;
    [Header("Jumping")]
    public float jumpForce;
    public bool isOnGround = true;
    [Header ("Shooting")]
    public GameObject projectile;
    public GameObject bulletSpawn;
    [Header("Animation")]
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOVE
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * moveSpeed * verticalInput * Time.deltaTime);
        animator.SetFloat("VerticalInput", Mathf.Abs(verticalInput));
        
        //TURN
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        
        //SHOOT
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Throw");
            
            Instantiate(projectile, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            
        }

        //JUMP
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            animator.SetBool("isOnGround", isOnGround);
        }
    }

    //Check if player is on the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isOnGround = true;
            animator.SetBool("isOnGround", isOnGround);
        }
    }



}
