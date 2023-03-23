using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Máscaras")]
    public GameObject mBase;
    public Animator mBaseAnim;
    private PlayerAbilityTracker masks;

    [Header("BaseStats do Player")]
    public Rigidbody2D rb;

    public float moveSpeed;
    public float jumpForce;

    public Transform foot;
    private bool onGround;
    public LayerMask ground;

    private bool canDoubleJump;

    public bool canMove;

    public float timeToAbility;

    public Transform thePlayer;

    [Header("Controle dos tiros (Mascara base)")]
    public BulletScript bullet;
    public Transform bulletPoint;
    public float timeToFire;
    public float timeToWait;

    [Header("Controle do escudo de corrente")]
    public GameObject chainShield;
    public float shieldDuration;
    public float timeToShield = 0f;



    private void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        canMove = true;
    }


    // Start is called before the first frame update
    void Start()
    {

        masks = GetComponent<PlayerAbilityTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToFire < 0)
        {
            timeToFire = 0;
        }

        if (timeToShield < 0)
            timeToShield = 0;

        

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
            if (Input.GetButtonDown("Fire1") && timeToFire <= 0f && onGround)
            {
                Bullet();
                timeToFire += timeToWait;

            }
            else if (timeToFire > 0)
            {
                timeToFire -= Time.deltaTime;
            }

            //habilidade do escudo
            /*if (Input.GetButtonDown("Fire2") && timeToShield <= 0f &&  masks.chainShield)
            {
                chainShield.SetActive(true);
                timeToShield += timeToAbility;
            }
            else if(timeToShield > 0)
            {
                timeToShield -= Time.deltaTime;
            }           
            else if (Input.GetButtonUp("Fire2"))
            {
                chainShield.SetActive(false);
            }*/
            

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

            // AudioManager.Instance.PlaySfxAdjusted(12);
        }
        else
        {
            //anim.SetTrigger("puloDuplo");

            canDoubleJump = false;

            // AudioManager.Instance.PlaySfxAdjusted(9);
        }
        Vector2 _velocity = rb.velocity;
        _velocity.y = 0f;
        rb.velocity = _velocity;
        rb.AddForce(Vector2.up * jumpForce);
    }

    void Bullet()
    {
        //bullet
        if (mBase.activeSelf)
        {
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation).direction = new Vector2(transform.localScale.x, 0f);

            //animação do disparo
            mBaseAnim.SetTrigger("atirando");

            //AudioManager.Instance.PlaySfxAdjusted(14);
        }
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("PositionX", thePlayer.position.x);
        PlayerPrefs.SetFloat("PositionY", thePlayer.position.y);
        PlayerPrefs.SetFloat("PositionZ", thePlayer.position.z);
    }

    public void NewLocation(Vector2 _location)
    {
        transform.position = _location;
    }

    public void SaveScene()
    {
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
    }

}
