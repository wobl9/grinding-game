using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public abstract string Id
    {
        get;
    }
    protected virtual void InvokeEffect()
    {
        Debug.Log($"effect of collectable {Id} was invoked");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        InvokeEffect();
        Destroy(this.gameObject);
    }

}
