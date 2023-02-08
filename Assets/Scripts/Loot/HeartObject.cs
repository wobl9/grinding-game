using UnityEngine;

public class HeartObject : Collectable
{
    [SerializeField] protected HeartModel model;
    public override string Id => model.id;

    public override bool IsSelfDestroyable => false;

    protected override void InvokeEffect()
    {
        base.InvokeEffect();
        FindObjectOfType<Player>().Heal(model.healAmount);
    }

}
