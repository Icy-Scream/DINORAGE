using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    [SerializeField] private int _damage = 4;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<IDamagable>(out IDamagable damage)) {
            damage.Damage(_damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.TryGetComponent<IDamagable>(out IDamagable damage)) {
            damage.Damage(_damage);
        }
    }
}
