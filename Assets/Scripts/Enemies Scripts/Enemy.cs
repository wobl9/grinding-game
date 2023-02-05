using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{

    public const string TAG = "Enemy";
    private const float DAMAGE_DELAY = 0.3f;

    protected int hp;
    protected int speed;
    protected int size;
    protected int exp;
    protected Transform playerTransform;
    protected PlayerLevelManager playerLevelManager;
    protected PlayerController playerController;
    public EnemyScriptableObject enemyScriptableObject;
    private Rigidbody2D rigidBody;
    private float lastHitToPlayer = 0f;

    public void ApplyDamage(int damage)
    {
        hp -= damage;
        KillIfNoHp(hp);
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.tag = TAG;
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = playerController.GetComponent<Transform>();
        playerLevelManager = FindObjectOfType<PlayerLevelManager>();   
    }

    private void Start()
    {
        hp = enemyScriptableObject.hp;
        speed = enemyScriptableObject.speed;
        size = enemyScriptableObject.size;
        exp = enemyScriptableObject.exp;
    }

    private void Update()
    {
        MoveToPlayer();
    }

    protected void DestroySelf(float delay = 0)
    {
        Destroy(this.gameObject, delay);
    }

    private void KillIfNoHp(int hp)
    {
        if (hp <= 0)
        {
            playerLevelManager.GainExpirience(exp);
            DestroySelf();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
        }
    }

    private void DamagePlayer()
    {
        if(Time.time > lastHitToPlayer + DAMAGE_DELAY)
        {
            playerController.Damage(exp);
            lastHitToPlayer = Time.time;
        }
    }

    private void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

    }
}
