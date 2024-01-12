using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Player")]
    public Rigidbody rb;
    public bool isDead = false;
    public bool canThrow = true;
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
    [Header("GameManager")]
    public GameManager gameManager;
    [Header("Powerup")]
    public bool hasPowerup = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOVE
        verticalInput = Input.GetAxis("Vertical");
        if (!isDead) 
        {
            transform.Translate(Vector3.forward * moveSpeed * verticalInput * Time.deltaTime);
            animator.SetFloat("VerticalInput", Mathf.Abs(verticalInput));
        }
        
        //TURN
        horizontalInput = Input.GetAxis("Horizontal");
        if (!isDead)
        {
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        }
        
        //SHOOT
        if(Input.GetKeyDown(KeyCode.Mouse0) && canThrow)
        {
            animator.SetTrigger("Throw");
            
            Instantiate(projectile, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            
        }

        //JUMP
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !isDead)
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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
            canThrow = false;
            animator.SetBool("isDead", isDead);
            gameManager.gameOverText.gameObject.SetActive(true);
            gameManager.restartGameButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            gameManager.powerupText.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownTimer());
        }
    }

    IEnumerator PowerupCountdownTimer()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        gameManager.powerupText.gameObject.SetActive(false);
    }

}
