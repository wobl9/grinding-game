using UnityEngine;

public class Projectile : Spell
{

    public Projectile(SpellConfig config)
    {
        this.config = config;
    }

    public override SpellConfig config { get; }

    private const float EXTEND_DIRECTION = 3f;
    private const float LIFE_TIME = 5f;

    private Vector3 moveDirection;

    private void Awake()
    {
        RemoveSelf(LIFE_TIME);
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            moveDirection = CalculacteMoveDirection(transform.position, closestEnemy.transform.position);
        }
        else
        {
            moveDirection = CalculacteMoveDirection(transform.position, CreateRandomVector());
        }

    }

    void Update()
    {
        Vector3 movement = Vector3.MoveTowards(transform.position, moveDirection, config.speed * Time.deltaTime);
        if (Vector3.Distance(movement, transform.position) != 0)
        {
            transform.position = movement;
        }
        else
        {
            RemoveSelf(LIFE_TIME);
        }
    }

    private Vector3 CalculacteMoveDirection(Vector3 startPosition, Vector3 endPosition)
    {
        return (endPosition - startPosition) * EXTEND_DIRECTION;
    }

    //todo почему 15?
    private Vector3 CreateRandomVector()
    {
        return new Vector3(
            transform.position.x + Random.Range(-15f, 15f),
            transform.position.y + Random.Range(-15f, 15f),
            0
            );
    }
}
