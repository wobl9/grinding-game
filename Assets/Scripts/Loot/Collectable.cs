using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public abstract string Id
    {
        get;
    }

    public abstract bool IsSelfDestroyable
    {
        get;
    }


    private const float OBJECT_DESTROY_DELAY = 1;
    private const float DISTANCE_WHERE_OBJECTS_DESTROED = 25;
    private float lastCheckIfObjectsShouldBeDestroyed = 0f;
    protected Player player;
    private Transform playerTransform;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerTransform = GetComponent<Transform>();
    }
    protected virtual void InvokeEffect()
    {
        Debug.Log($"effect of collectable {Id} was invoked");
    }

    private void Update()
    {
        if (IsSelfDestroyable && Time.time > lastCheckIfObjectsShouldBeDestroyed + OBJECT_DESTROY_DELAY)
        {
            DestroyObjectsFarFromPlayer();
            lastCheckIfObjectsShouldBeDestroyed = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        InvokeEffect();
        Destroy(this.gameObject);
    }

    private void DestroyObjectsFarFromPlayer()
    {
        if ((transform.position - playerTransform.position).magnitude > DISTANCE_WHERE_OBJECTS_DESTROED)
        {
            Destroy(this.gameObject);
        }
    }

}
