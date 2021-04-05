using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "a_slime_tale/EnemySO", order = 0)]
public class EnemySO : ScriptableObject
{
    [SerializeField] public float damage;
}