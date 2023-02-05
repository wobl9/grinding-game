using UnityEngine;

public class ProgressBarObject : MonoBehaviour
{

    public HealthSystem healthSytem;
    private Transform bar;

    public void Setup(HealthSystem healthSytem)
    {
        bar = transform.Find("ProgressContainer");
        this.healthSytem = healthSytem;
        healthSytem.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(object Sender, System.EventArgs args)
    {
        bar.localScale = new Vector3(healthSytem.GetPercentHealth(), 1);
    }
}
