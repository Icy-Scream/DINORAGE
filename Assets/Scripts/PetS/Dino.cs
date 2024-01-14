using TMPro;
using UnityEngine;


public class Dino : Monster, IDamagable {
    [SerializeField] TMP_Text AIStateText;
    [SerializeField] Stab basicAttack;
    private void Init() {
        base.Init();
        MaxHp = 100;
        _attackRadius = 0.8f;
        _currentHappiness = MaxHappiness;
        _currentHunger = MaxHunger;
        goodorEvil = GoodorEvil.Good;
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        AIStateText.text = "AI State: " + _currentState.ToString();
        if (basicAttack._isAttacking)
            _monsterAnimator.AttackTrigger();
    }

    private void LateUpdate() {
        AIStateMachine();
    }


    public override void AIStateMachine() {
        switch (_currentState) {
            case AISTATE.Wandering:
                transform.position += Vector3.right * 0.5f * Time.deltaTime;
                if (AttackTarget != null)
                    _currentState = AISTATE.Chasing;
                break;
            case AISTATE.Chasing:
                if (AttackTarget == null)
                    _currentState = AISTATE.Wandering;
                else {
                    Movement();
                    if (GetDistance() < _attackRadius)
                        _currentState = AISTATE.Attacking;
                         basicAttack._timer = Time.time;
                }

                break;

            case AISTATE.Attacking:

                if (AttackTarget != null) {
                    if (GetDistance() > _attackRadius && AttackTarget != null) {
                        basicAttack._isAttacking = false;
                        _currentState = AISTATE.Chasing;
                    }

                    else if (AttackTarget != null)
                        basicAttack.RayAttack();
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
