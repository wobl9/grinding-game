using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public abstract SpellConfig config { get; }
    public string id { get => config.id; }
    public float cooldown { get => config.coldown; }
    public float damage { get => config.damage; }
    public float speed { get => config.speed; }
    public CastStrategy castStrategy { get => config.castStrategy; }

    public GameObject prefab { get => config.prefab; }

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
        Debug.Log("collision occures 1");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("ApplyDamage", config.damage);
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
