using UnityEngine;
public class ExpObject : Collectable
{
    [SerializeField] ExpModel model;
    public override string Id => model.id;

    public override bool IsSelfDestroyable => true;

    protected override void InvokeEffect()
    {
        base.InvokeEffect();
        player.GainExperience(model.expAmount);
    }
}
