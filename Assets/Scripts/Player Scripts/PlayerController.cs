using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] ControlManager controlManager;

    [Header("M�scaras")]
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

    public Transform playerPosition;

    [Header("Controle dos tiros (Mascara base)")]
    public BulletScript bullet;
    public Transform bulletPoint;
    public float timeToFire;
    public float timeToWait;

    [Header("Controle do escudo de corrente")]
    public GameObject chainShield;
    public float shieldDuration;
    public float timeToShield = 0f;

    [Header("Sfx Player")]
    public int sfxWalkIndex;
    public AudioSource[] walkSfx;


    private void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        {
            timeToShield = 0;
        }

        

        if (canMove)
        {
            //movimento horizontal
            Move();

            onGround = Physics2D.OverlapCircle(foot.position, .2f, ground);

            //movimento vertical
            if (controlManager.tryToJump && (onGround || (canDoubleJump && mBase.activeSelf)))
            {
                Jump();

                mBaseAnim.SetTrigger("jump");
            }

            //acionando o disparo
            if (controlManager.tryToAttack && timeToFire <= 0f && onGround)
            {
                Bullet();
                timeToFire += timeToWait;

            }
            else if (timeToFire > 0)
            {
                timeToFire -= Time.deltaTime;
            }

            //habilidade do escudo
            if (controlManager.tryToSpecialAttack && timeToShield <= 0f &&  masks.chainShield)
            {
                ActiveShield();
                timeToShield += timeToAbility;
            }
            else if(timeToShield > 0)
            {
                timeToShield -= Time.deltaTime;
            }
            

        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (mBase.activeSelf)
        {
            mBaseAnim.SetBool("onGround", onGround);
            mBaseAnim.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "MetalGorund") 
        {
            sfxWalkIndex = 0;
        }
        else if (other.gameObject.tag == "GrassGround")
        {
            sfxWalkIndex = 1;
        }
        else if(other.gameObject.tag == "Ground")
        {
            sfxWalkIndex = 2;
        }
    }

    public void SoundWalk()
    {
        walkSfx[sfxWalkIndex].Play();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(controlManager.moveDirection.x * moveSpeed, rb.linearVelocity.y);

        //Flip
        if ((rb.linearVelocity.x > 0 && transform.localScale.x < 0) || (rb.linearVelocity.x < 0 && transform.localScale.x > 0))
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

            canDoubleJump = false;

            // AudioManager.Instance.PlaySfxAdjusted(9);
        }
        mBaseAnim.SetBool("onGorund", onGround);

        Vector2 _velocity = rb.linearVelocity;
        _velocity.y = 0f;
        rb.linearVelocity = _velocity;
        rb.AddForce(Vector2.up * jumpForce);
    }

    void Bullet()
    {
        //bullet
        if (mBase.activeSelf)
        {
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation).direction = new Vector2(transform.localScale.x, 0f);

            //anima��o do disparo
            mBaseAnim.SetTrigger("atirando");

            //AudioManager.Instance.PlaySfxAdjusted(14);
        }
    }

    


    public void ActiveShield()
    {
        chainShield.SetActive(true);
        PlayerHealthController.instance.invencible = true;
        Invoke("DesactiveShield", 2f);
    }

    public void DesactiveShield()
    {

        PlayerHealthController.instance.invencible = false;
        chainShield.SetActive(false);
    }

}
