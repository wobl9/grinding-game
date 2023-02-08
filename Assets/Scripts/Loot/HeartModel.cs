using UnityEngine;

[CreateAssetMenu(fileName = "Heart Model", menuName = "MyGameObject/Heart Model")]
public class HeartModel : ScriptableObject
{
    public string id;
    public int healAmount;
    public GameObject prefab;
}
