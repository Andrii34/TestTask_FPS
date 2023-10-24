
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyParameters", menuName = "Scriptable obgect/EnemyParamaters")]
public class EnemyParameters : ScriptableObject
{
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    
    public int damage;
}
