using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{

    enum EnemyBehaviour{Chase, Attack, Recover, Stunned};

    EnemyBehaviour curState;
    public GameObject playerObj;
    [Header("Movement")]
    public float maxSpeed;
    float speed;
    Rigidbody2D rb;
    float scaleX;

    [Header("Attack")]
    public GameObject enemyBullet;
    public Animator anim;
    float yspeed;
    public Transform bulletPos;
    public GameObject coinObj;
    [SerializeField] GameObject deathParticle;

    [Header("Stats")]
    public int health;
    public float stunTime;
    public float recoverTime;
    public int attackRange;
    [SerializeField] int type;
    [SerializeField] int jumpForce;
    public LayerMask groundLayer;
    public Transform rayStartTransform;
    [SerializeField] AudioClip enemyHitSound;
    [SerializeField] AudioClip enemyAttackSound;
    [SerializeField] AudioClip enemyDeathSound;
    
    
    //public int damage;
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        curState = EnemyBehaviour.Chase;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        scaleX = transform.localScale.x;
        yspeed = maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckState();
        //Debug.Log(playerObj);

    }

    private void CheckState()
    {
        if(curState == EnemyBehaviour.Chase)
        {
            MoveTowardsPlayer(); 
        
        }
        if(curState == EnemyBehaviour.Attack)
        {
            Attack();
        }
        if(curState == EnemyBehaviour.Recover)
        {
            //rb.velocity = Vector2.zero;
        }

    }
    private void MoveTowardsPlayer()
    {

        if(transform.position.y > playerObj.transform.position.y)
        {
            yspeed = -maxSpeed;
        }
        if(transform.position.y < playerObj.transform.position.y)
        {
            yspeed = maxSpeed;
        }
        if(transform.position.x > playerObj.transform.position.x)
        {
            speed = -maxSpeed;
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
            if(type == 1)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayStartTransform.position, Vector2.left, 2f, groundLayer);

                if(hit.collider != null)
                {
                    rb.AddForce(Vector2.up * jumpForce);
                    transform.Translate(Vector2.up * Time.deltaTime);
                    //Debug.Log("we are hitting");
                }
            }

        }
        if(transform.position.x < playerObj.transform.position.x)
        {
            speed = maxSpeed;
            transform.localScale = new Vector3((-1)*scaleX, transform.localScale.y, transform.localScale.z);
            if(type == 1)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayStartTransform.position, Vector2.left, 2f, groundLayer);

                if(hit.collider != null)
                {
                    rb.AddForce(Vector2.up * jumpForce);
                    Debug.Log("we are hitting");
                }
            }
        }
        if(Vector3.Distance(transform.position, playerObj.transform.position) > attackRange && (type ==1 || type == 3))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (Vector3.Distance(transform.position, playerObj.transform.position) > attackRange && type ==2)
        {
            rb.velocity = new Vector2(speed, yspeed);
        }
        else {
            //rb.velocity = Vector2.zero;
            curState = EnemyBehaviour.Attack;
        }
        
    }
    private void Attack()
    {
        //Debug.Log("Im ATTACKING");
        anim.SetTrigger("Attack");
        if(type == 3)
        {
            //bulletPos.LookAt(playerObj.transform.position);
            //Instantiate(enemyBullet, bulletPos.position, bulletPos.rotation);
        }
        AudioManager.Instance.Play(enemyAttackSound);
        curState = EnemyBehaviour.Recover;
        //CancelInvoke();
        Invoke(nameof(SetChase), recoverTime);
    }
    public void ChangeHealth(int damage)
    {
        health-=damage;
        curState = EnemyBehaviour.Stunned;
        anim.SetTrigger("Hit");
        //CancelInvoke();
        Invoke(nameof(Stun), stunTime);
        CheckHealth();
        AudioManager.Instance.Play(enemyHitSound);
    }

    private void CheckHealth()
    {
        if(health <= 0)
        {
            transform.DOScale(this.transform.localScale * .25f, .75f);
            speed = 0;
            AudioManager.Instance.Play(enemyDeathSound);
            Invoke("KillThis", .75f);
        }
    }
    private void SetChase()
    {
        curState = EnemyBehaviour.Chase;
    }
    private void Stun()
    {
        curState = EnemyBehaviour.Chase;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.gameObject.tag);

        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().ChangeHealth();
        }
    }
    private void KillThis()
    {
        Instantiate(coinObj, this.transform.position, Quaternion.identity);
        Instantiate(deathParticle, this.transform.position, Quaternion.identity);
        
        Destroy(this.gameObject);
    }
}
