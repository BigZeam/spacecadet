using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("UI")]
    public Slider ammoSlider;
    public Text ammoText;
    public Text moneyText;
    public GameObject[] UIhearts;

    [Header("Stats")]
    public int health;
    public int type;
    int money;

    
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
        
        rb2d = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        gunSr.sprite = gunSlot1.gunSprite;
        reloadSpeed = gunSlot1.fireRate * 4;
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        Jump();
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Flip();
        rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
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
                jumpsLeft--;
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
    }
    public void SetAmmoUI()
    {
        ammoSlider.value = ammo;
        string ammoString = ammo.ToString() + " : " + gunSlot1.ammoCount;
        ammoText.text = ammoString;
    }

    public void ChangeHealth()
    {
        
        health--;
        Debug.Log(health);
        if(health <= 0)
        {
            Debug.Log("Lose");
        }
        SetHealthUI();
    }

    public int GetHealth()
    {
        return health;
    }
    public Gun GetGunSlot1()
    {
        return gunSlot1;
    }
    public void IncHealth()
    {
        if(health < 3 && health > 0)
        {
            health++;
        }
        for(int i = 0; i<UIhearts.Length; i++)
        {
            if(health > i)
            {
                UIhearts[i].SetActive(true);
            }
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
                UIhearts[i].SetActive(true);
            }
            else 
            {
                UIhearts[i].SetActive(false);
            }
        }
    }
    public void SetNewGunImg()
    {
        gunSr.sprite = gunSlot1.gunSprite;
    }
}
