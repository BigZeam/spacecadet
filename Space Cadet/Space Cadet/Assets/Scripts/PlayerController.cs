using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Jump")]
    public int jumpsAmount;
    int jumpsLeft;
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    bool isGrounded;

    [Header("Gun")]
    public Gun gunSlot1;
    public Transform spawnPos, gunPos, gunSpritePos, gunPosLeft;
    public GameObject bulletObj;
    public SpriteRenderer gunSr;
    public Animator gunAnim;
    float reloadSpeed;
    Vector2 gunDirection;
    float readyForNextShot;
    int ammo;
    bool isReloading;
    [SerializeField] GameObject gunFireParticle;
    bool canShoot;

    [Header("UI")]
    public Slider ammoSlider;
    public Text ammoText;
    public Text moneyText;
    public Image[] UIhearts;
    [SerializeField] Sprite fullHeart, emptyHeart;

    [Header("Stats")]
    public int health;
    public int maxHealth;
    public int type;
    [SerializeField] Animator anim;
    [SerializeField] GameManager gm;
    int money;
    bool canLoseHealth = true;
    [SerializeField] float invulnerabilityTimer;
    [SerializeField] Color hitColor;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] GameObject orbObject;

    [SerializeField] public float orbResetTime;

    [Header("Sounds")]
    [SerializeField] AudioClip shootEffect;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip playerHitSound;
    [SerializeField] AudioClip jumpSound;

    
    //Resources;
    


    float moveInput;
    Rigidbody2D rb2d;
    float scaleX;
    // Start is called before the first frame update
    void Start()
    {
        ammo = gunSlot1.ammoCount;
        ammoSlider.maxValue = ammo;
        SetAmmoUI();
        canShoot = true;
        rb2d = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        gunSr.sprite = gunSlot1.gunSprite;
        reloadSpeed = gunSlot1.fireRate * 4;
        moneyText.text = money.ToString();
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        //sr = this.gameObject.GetComponent<SpriteRenderer>();
        DOTween.Init();
        SetHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gm.isGameOver)
        {
            Jump();
            if(canShoot)
                Shoot();
        }

    }

    private void FixedUpdate()
    {
        if(!gm.isGameOver)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            Move();  
        }
        else 
        {
            sr.sprite = null;
            anim = null;
        }

    }

    public void Move()
    {
        Flip();
        rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        //rb2d.AddForce(new Vector2(moveInput, 0) * moveSpeed);
        anim.SetBool("isMoving", (moveInput != 0));
    }

    public void Flip()
    {
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
        if (moveInput < 0)
        {
            transform.localScale = new Vector3((-1)*scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckIfGrounded();
            if (jumpsLeft > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                //rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpsLeft--;
                AudioManager.Instance.Play(jumpSound);
            }
                               
        }
        
    }

    public void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer);
        ResetJumps();
    }

    public void ResetJumps()
    {
        if (isGrounded)
        {
            jumpsLeft = jumpsAmount;// jumpsAmount =2;
        }
    }

    public void Shoot()
    {

        if(Input.GetKeyDown(KeyCode.R)){
            ReloadGun();
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gunDirection = mousePos - (Vector2)gunPos.position;

        gunPos.transform.right = gunDirection;
        GunFlip(mousePos);
        if(Input.GetMouseButtonDown(0) && ammo > 0 && !isReloading)
        {
            if(Time.time > readyForNextShot)
            {
                ammo--;

               
                SetAmmoUI();
                gunAnim.SetTrigger("Recoil");
                readyForNextShot = Time.time + gunSlot1.fireRate;
                GameObject tempBullet;
                tempBullet = Instantiate(bulletObj, spawnPos.position, spawnPos.rotation);
                Instantiate(gunFireParticle, spawnPos.position, spawnPos.rotation);

                tempBullet.GetComponent<Bullet>().SetGun(gunSlot1);
                if(ammo <=0)
                {
                    ReloadGun();
                }
                tempBullet.GetComponent<Rigidbody2D>().AddForce(tempBullet.transform.right * gunSlot1.bulletSpeed);

                AudioManager.Instance.Play(shootEffect);
            }
       
        }
    }
    private void GunFlip(Vector2 mousePos)
    {

        
        if(mousePos.x < transform.position.x && transform.localScale.x > 0)
        {
            gunSr.flipY = true;
            gunSpritePos.position = gunPos.position;
            gunSpritePos.localScale = new Vector3(scaleX, gunSpritePos.localScale.y, gunSpritePos.localScale.z);
        }
        if(mousePos.x > transform.position.x && transform.localScale.x > 0)
        {
            gunSr.flipY = false;
            gunSpritePos.position = gunPos.position;
            gunSpritePos.localScale = new Vector3(scaleX, gunSpritePos.localScale.y, gunSpritePos.localScale.z);
        }
        if(mousePos.x < transform.position.x && transform.localScale.x < 0)
        {
            gunSr.flipY = true;
            gunSpritePos.position = gunPosLeft.position;
            gunSpritePos.localScale = new Vector3((-1)*scaleX, gunSpritePos.localScale.y, gunSpritePos.localScale.z);
        }
        if(mousePos.x > transform.position.x && transform.localScale.x < 0)
        {
            gunSr.flipY = false;
            gunSpritePos.position = gunPosLeft.position;
            gunSpritePos.localScale = new Vector3((-1)*scaleX, gunSpritePos.localScale.y, gunSpritePos.localScale.z);
        }
    }

    void ReloadGun()
    {
        readyForNextShot = Time.time + reloadSpeed;
        ammo = gunSlot1.ammoCount;
        SetAmmoUI();
        AudioManager.Instance.Play(reloadSound);
    }
    public void SetAmmoUI()
    {
        ammoSlider.value = ammo;
        ammoSlider.maxValue = gunSlot1.ammoCount;
        //string ammoString = ammo.ToString() + " : " + gunSlot1.ammoCount;
        //ammoText.text = ammoString;
    }

    public void ChangeHealth()
    {
        if(canLoseHealth)
        {
            health--;
            //tween health anim
            Sequence invulnerableSequence = DOTween.Sequence();
            invulnerableSequence.Append(sr.DOColor(hitColor, invulnerabilityTimer/2));
            invulnerableSequence.Append(sr.DOColor(new Color(255,255,255,1), invulnerabilityTimer/2));

            AudioManager.Instance.Play(playerHitSound);

            if(health <= 0)
            {
                gm.LoseGame();
            }
            SetHealthUI();
            canLoseHealth = false;
            Invoke(nameof(Invulnerability), invulnerabilityTimer);
        }

    }
    void Invulnerability()
    {
        canLoseHealth = true;
    }
    public void MakeInvulnerable()
    {
        canLoseHealth = false;
        Invoke(nameof(Invulnerability), orbResetTime);
    }
    public int GetHealth()
    {
        return health;
    }
    public Gun GetGunSlot1()
    {
        return gunSlot1;
    }
    public bool IncHealth()
    {
        if(health < maxHealth && health > 0)
        {
            health++;
            SetHealthUI();
            return true;
        }
        return false;
    }
    public bool SetMaxHealth()
    {
        if(maxHealth < 5)
        {
            maxHealth++;
            health++;
            SetHealthUI();
            return true;
        }
        else 
        {
            return false;
        }
    }
    public void IncReloadSpeed()
    {
        reloadSpeed-=.05f;
    }
    public void IncMoveSpeed(float num)
    {
        moveSpeed+=num;
    }
    public void ChangeCoin(int num)
    {
        money += num;
        moneyText.text = money.ToString();

    }
    void SetHealthUI()
    {
        
        for(int i = 0; i<UIhearts.Length; i++)
        {
            if(health > i)
            {
                UIhearts[i].gameObject.SetActive(true);
                UIhearts[i].sprite = fullHeart;
            }
            else 
            {
                if(i<maxHealth)
                {
                    UIhearts[i].gameObject.SetActive(true);
                    UIhearts[i].sprite = emptyHeart;
                }  
                else 
                    UIhearts[i].gameObject.SetActive(false);
            }
        }
    }
    public void ActivateOrb()
    {
        orbObject.SetActive(true);
    }
    public void SetNewGunImg()
    {
        gunSr.sprite = gunSlot1.gunSprite;
    }
    public void SetCanShoot(bool b) {
        canShoot = b;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
