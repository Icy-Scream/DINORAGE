using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    [SerializeField] private Animator _monsterAnim;

    public void AttackAnim(bool _attacking) {
        _monsterAnim.SetBool("Attack", _attacking);
    }

    public void AttackTrigger() {
        _monsterAnim.SetTrigger("AttackTrigger"); 
    }
}
