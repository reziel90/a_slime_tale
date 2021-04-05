using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    CapsuleCollider2D _collider;
    [SerializeField] EnemySO enemyDatas;
    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealthManager _dm = other.gameObject.GetComponent<HealthManager>();
            _dm.DoDamage(enemyDatas.damage);
        }
    }

}
