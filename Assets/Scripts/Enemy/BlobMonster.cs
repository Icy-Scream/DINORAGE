using UnityEngine;

public class BlobMonster : Monster, IDamagable
{
    [SerializeField] Stab basicAttack;
    private void Init() {
        MaxHp = 100;
        _attackRadius = _intelligence / 100f + 0.3f;
        _currentHappiness = MaxHappiness;
        _currentHunger = MaxHunger;
        goodorEvil = GoodorEvil.Evil;
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        AIStateMachine();
        if (basicAttack._isAttacking)
            _monsterAnimator.AttackTrigger();
    }

    public override void AIStateMachine() {
        switch (_currentState) {
            case AISTATE.Wandering:
                Vector3 direction = (Vector3.zero - transform.position).normalized;
              Vector3 velocity = direction * (_agility / 100f * Time.deltaTime);
               transform.position += velocity;
               
                if (AttackTarget != null)
                    _currentState = AISTATE.Chasing;
                break;
            case AISTATE.Chasing:
                if (AttackTarget == null)
                    _currentState = AISTATE.Wandering;
                else {
                    Movement();
                    if (GetDistance() < _attackRadius) {
                        _currentState = AISTATE.Attacking;
                        basicAttack._timer = Time.time;
                    }
                }
                break;
            case AISTATE.Attacking:

                if (AttackTarget != null) {
                    if (GetDistance() > _attackRadius && AttackTarget != null) {
                        basicAttack._isAttacking = false;
                        _currentState = AISTATE.Chasing;
                    }

                    else if (AttackTarget != null) {
                        basicAttack.RayAttack();
                    }
                    else {
                        basicAttack._isAttacking = false;
                        _currentState = AISTATE.Wandering;
                    }
                }
                else if (AttackTarget == null) {
                    basicAttack._isAttacking = false;
                    _currentState = AISTATE.Wandering;
                }
                break;
        }


    }

    private void Movement() {
        Vector3 direction = (AttackTarget.transform.position - transform.position);
        Vector3 velocity = direction.normalized * (_agility / 100f) * Time.deltaTime;
        if (direction.x < 0)
            _sprite.flipX = false;
        else
            _sprite.flipX = true;

        transform.position += velocity;
    }

    private float GetDistance() {
        if (AttackTarget == null) {
            _currentState = AISTATE.Wandering;
            return 0;
        }
        else
            return (AttackTarget.transform.position - transform.position).magnitude;
    }


    public void Damage(int damage) {
        _hp -= damage;
        Death();
    }
}
