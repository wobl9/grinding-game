using UnityEngine;


public abstract class SpellObject : MonoBehaviour
{
    [SerializeField] private Spell model;
    public string id { get => model.id; }
    public float cooldown { get => model.cooldown; }
    public float damage { get => model.damage; }
    public float speed { get => model.speed; }
    public string description { get => model.description; }
    public int level { get => model.level; }
    public CastStrategy castStrategy { get => model.castStrategy; }

    public GameObject prefab { get => model.prefab; }

    protected GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("Damage", model.damage);
            Destroy(gameObject);
        }
    }

    protected void RemoveSelf(float afterTime = 0f)
    {
        Destroy(this.gameObject, afterTime);
    }

    private void OnBecameInvisible()
    {
        RemoveSelf();
    }

}
