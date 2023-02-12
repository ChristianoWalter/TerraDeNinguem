using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Máscaras")]
    public GameObject mBase;
    public Animator mBaseAnim;

    [Header("Movimento do Player")]
    public Rigidbody2D rb;

    public float moveSpeed;
    public float jumpForce;

    public Transform foot;
    private bool onGround;
    public LayerMask ground;

    private bool canDoubleJump;

    public bool canMove;

    [Header("Controle dos tiros (Mascara base)")]
    public BulletScript bullet;
    public Transform bulletPoint;



    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //movimento horizontal
            Move();

            onGround = Physics2D.OverlapCircle(foot.position, .2f, ground);

            //movimento vertical
            if (Input.GetButtonDown("Jump") && (onGround || (canDoubleJump && mBase.activeSelf)))
            {
                Jump();
            }

            //acionando o disparo
            if (Input.GetButtonDown("Fire1"))
            {
                Bullet();
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (mBase.activeSelf)
        {
            mBaseAnim.SetBool("onGround", onGround);
            mBaseAnim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        }
    }
    
    void Move()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

        //Flip
        if ((rb.velocity.x > 0 && transform.localScale.x < 0) || (rb.velocity.x < 0 && transform.localScale.x > 0))
        {
            Vector2 _localScale = transform.localScale;
            _localScale.x *= -1f;
            transform.localScale = _localScale;
        }
    } 

    void Jump()
    {
        if (onGround)
        {
            canDoubleJump = true;

            // AudioManager.instance.PlaySfxAdjusted(12);
        }
        else
        {
            //anim.SetTrigger("puloDuplo");

            canDoubleJump = false;

            // AudioManager.instance.PlaySfxAdjusted(9);
        }
        Vector2 _velocity = rb.velocity;
        _velocity.y = 0f;
        rb.velocity = _velocity;
        rb.AddForce(Vector2.up * 700);
    }

    void Bullet()
    {
        //bullet
        if (mBase.activeSelf)
        {
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation).direction = new Vector2(transform.localScale.x, 0f);

            //animação do disparo
            mBaseAnim.SetTrigger("atirando");

            //AudioManager.instance.PlaySfxAdjusted(14);
        }
    }

}
