using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Player Attack")]
public class SpellConfig : ScriptableObject
{
    public string id;
    public float coldown;
    public float speed;
    public float damage;
    public int level = 1;
    public bool isDestroyOnCollision;
    public CastStrategy castStrategy;
    public GameObject prefab;

}

public enum CastStrategy
{
    ZERO_COOLDOWN,
    PRESS_BUTTON,
    ANY
}
