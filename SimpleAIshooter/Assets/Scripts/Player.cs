using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movSpeed = 7f;
    public float jumpForce = 5f;

    private Animator animator;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private float movX;
    private bool isGrounded=true;

    public Healthbar healthBar;
    public float maxHealth = 10;
    private float currentHealth;
    public float damage = 1;
    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        button.SetActive(false); //aici
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        DiePlayer();
        
    }

    void MovePlayer()
    {
         movX = Input.GetAxis("Horizontal");
       // float movY = Input.GetAxis("Vertical");

        if (movX > 0)
        {
            sr.flipX = false;
            animator.SetBool("isWalking", true);
            transform.Translate(new Vector3(movX * movSpeed * Time.deltaTime, 0f, 0f));
        }
        else if (movX < 0)
        {
            sr.flipX = true;
            animator.SetBool("isWalking", true);
            transform.Translate(new Vector3(movX * movSpeed * Time.deltaTime, 0f, 0f));
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

    }

    void Jump()
    {
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded==true)
            {
                rb.velocity = Vector2.up * jumpForce;

            }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.CompareTag("Projectile"))
        {
            TakeDamage(damage);
        }
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void DiePlayer()
    {
        if (currentHealth <=0)
        {
            animator.SetBool("isDead", true);
            StartCoroutine(DelayAction(2));
           //gameObject.SetActive(false);
            
        }
    }

    IEnumerator DelayAction(float delay)
    {
        yield return new WaitForSeconds(delay);
        //gameObject.SetActive(false);
        isAlive = false;

        button.SetActive(true); //aici
    }

    public GameObject button; //aici



}
