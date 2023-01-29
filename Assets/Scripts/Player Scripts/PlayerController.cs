using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float speed = 1f;
    [SerializeField] private Projectile fireball;

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        transform.position += movement * speed * Time.deltaTime;
    }
}
