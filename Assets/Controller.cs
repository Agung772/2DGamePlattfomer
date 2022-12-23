using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    bool isGround;
    float radius = 2f;
    float force = 300;
    public Transform coinsParent;
    public Transform groundPoint;
    public LayerMask ground;
    public AudioSource audioSource;
    public AudioClip jump, food, win;
    public Animator animator;
    bool jumped;
    float jumpTime, jumpDelay = 0.5f;

    public int foods;
    public TextMesh score;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle (groundPoint.position, radius, ground);
        if (isGround)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.AddForce(Vector2.up * force * 1.3f);
                jumpTime = jumpDelay;
                jumped = true;
                animator.SetTrigger("Jumped");


            }
        }
        jumpTime -= Time.deltaTime;
        if (jumpTime <= 0 && jumped)
        {
            jumped = false;
            animator.SetTrigger("Landed");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            foods++;
            score.text = "" + foods + "";
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeadZone")
        {
            Application.LoadLevel(0);
        }
        if (collision.tag == "Finish")
        {
            print("Win");
        }
    }
}
