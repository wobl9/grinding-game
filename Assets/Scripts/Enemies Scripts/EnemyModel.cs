using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "MyGameObject/Enemy")]
public class EnemyModel : ScriptableObject
{
    public new string name;
    public int speed;
    public int exp;
    public int hp;
    public int size;
}
