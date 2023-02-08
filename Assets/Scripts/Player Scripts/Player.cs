using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    [SerializeField] protected ProgressBarObject healthBar;
    public float speed = 2f;

    private HealthSystem healthSytem = new(100, 100);
    private LevelSystem levelSystem = new();


    private void Start()
    {
        healthSytem.OnDeath += OnDeath;
        healthBar.Setup(healthSytem);
    }

    private void Update()
    {
        MovePlayer();
    }

    public void Heal(int amount)
    {
        healthSytem.Heal(amount);
    }

    public void Damage(int amount)
    {
        healthSytem.Damage(amount);
    }

    public void GainExperience(int amount)
    {
        levelSystem.Equals(amount);
    }

    private void MovePlayer()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        transform.position += movement * speed * Time.deltaTime;
    }

    private void OnDeath(object Sender, System.EventArgs args)
    {
        Debug.Log("no hp. player died");
    }

}
