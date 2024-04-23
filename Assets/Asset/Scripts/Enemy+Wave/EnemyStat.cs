using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyStat : ScriptableObject
{
    public float startSpeed;
    public float startHP;
    public int drop;
    public float physArmor;
    public float lasArmor;
    public int live;
    public float rate;
    public GameObject deathEffect;
}
