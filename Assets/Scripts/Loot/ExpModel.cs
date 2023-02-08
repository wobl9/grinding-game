using UnityEngine;

[CreateAssetMenu(fileName = "Expirience Model", menuName = "MyGameObject/Expirience Model")]
public class ExpModel : ScriptableObject
{
    public string id;
    public int expAmount;
    public GameObject prefab;
}
