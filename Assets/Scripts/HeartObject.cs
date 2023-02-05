using UnityEngine;

public class HeartObject : Collectable
{
    [SerializeField] protected HeartModel model;
    public override string Id => model.id;

    protected override void InvokeEffect()
    {
        base.InvokeEffect();
        FindObjectOfType<PlayerController>().Heal(model.healAmount);
    }

}
