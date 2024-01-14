using UnityEngine;

public class Stab : MonoBehaviour
{
    [SerializeField] private int _damage = 4;
    [SerializeField] private GameObject _monster;
    [SerializeField] public float _timer;
    [SerializeField] private float DPS;
    [SerializeField] private LayerMask _target;
    [SerializeField] public bool _isAttacking = false;
    Monster monsterObject;

    private void Start() {
        monsterObject = _monster.GetComponent<Monster>();
    }

    public void RayAttack() {
        Vector3 direction = (monsterObject.AttackTarget.transform.position - transform.position).normalized;
        var hitRay = Physics2D.Raycast(transform.position, direction, 0.8f,_target);

        if (hitRay.collider != null) {
            if (hitRay.transform.TryGetComponent<IDamagable>(out var enemy)) {
                Attack(enemy);
            }
        }
    }

    private void Attack(IDamagable enemy) {
        bool attackTimer = Time.time > (_timer + DPS);
        if (attackTimer) {
            _isAttacking = true;
            enemy.Damage(1);
            _timer = Time.time; 
        }
        else
            _isAttacking = false;
    }
}
