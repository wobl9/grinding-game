using UnityEngine;
public class ExpObject : Collectable
{
    [SerializeField] ExpModel model;
    public override string Id => model.id;
    protected override void InvokeEffect()
    {
        base.InvokeEffect();
        FindObjectOfType<PlayerLevelManager>().GainExpirience(model.expAmount);
    }
}
