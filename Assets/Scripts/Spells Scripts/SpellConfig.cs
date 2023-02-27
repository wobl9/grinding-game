using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "MyGameObject/Player Attack")]
public class Spell : ScriptableObject
{
    public string id;
    public float cooldown;
    public float speed;
    public float damage;
    public int level;
    public bool isDestroyOnCollision;
    public CastStrategy castStrategy;
    public GameObject prefab;
    public string description;

}

public enum CastStrategy
{
    ZERO_COOLDOWN,
    PRESS_BUTTON,
    ANY
}
