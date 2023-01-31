using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{

    public const string TAG = "Enemy";

    protected int hp;
    protected int speed;
    protected int size;
    protected int exp;
    protected Transform player;
    protected PlayerLevelManager playerLevelManager;
    public EnemyScriptableObject enemyScriptableObject;
    private Rigidbody2D rigidBody;

    public void ApplyDamage(int damage)
    {
        hp -= damage;
        KillIfNoHp(hp);
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        playerLevelManager = FindObjectOfType<PlayerLevelManager>();
        transform.tag = TAG;
        
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
            playerLevelManager.OnExpirienceGained(exp);
            DestroySelf();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 bounceBackVector = (transform.position - collision.gameObject.transform.position).normalized * 2f;
            rigidBody.AddForce(bounceBackVector);
        }
    }

    private void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

    }
}
