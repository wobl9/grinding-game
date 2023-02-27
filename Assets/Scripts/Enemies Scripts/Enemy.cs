using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{

    public const string TAG = "Enemy";

    [SerializeField] protected EnemyModel model;

    private const float DAMAGE_DELAY = 0.3f;
    private float lastHitToPlayer = 0f;

    private Rigidbody2D rigidBody;
    private HealthSystem healthSytem;
    private Transform playerTransform;
    private Player player;

    public void Heal(int amount)
    {
        healthSytem.Heal(amount);
    }

    public void Damage(int amount)
    {
        healthSytem.Damage(amount);
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.tag = TAG;
        player = FindObjectOfType<Player>();
        playerTransform = player.GetComponent<Transform>();
        healthSytem = new HealthSystem(model.hp, model.hp);
        healthSytem.OnDeath += OnDeath;
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void OnDeath(object Sender, System.EventArgs args)
    {
        player.GainExperience(model.exp);
        Destroy(this.gameObject);
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
        if (Time.time > lastHitToPlayer + DAMAGE_DELAY)
        {
            player.Damage(model.exp);
            lastHitToPlayer = Time.time;
        }
    }

    private void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, model.speed * Time.deltaTime);
    }
}
