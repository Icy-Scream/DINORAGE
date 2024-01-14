using UnityEngine;

public class TargetSystem : MonoBehaviour {
    [SerializeField] private LayerMask _targetMask;
    private Monster _currentMonster;
    private void Start () { 
        _currentMonster = GetComponent<Monster>();
   
    }

    private void Update () {
        DetectionCircle();
    }

    public void GetTargetMonster(Monster currentMonster,GameObject[] monsters) {
        foreach (GameObject targetObj in monsters) {
            var distance1 = Vector3.Distance(transform.position, targetObj.transform.position);
            if (distance1 < (currentMonster.GetIntelligence() / 100) + 2.5f) {
                if(currentMonster.AttackTarget == null) 
                    currentMonster.GetAttackVector(targetObj.gameObject);
            }
        }

    }

    private void DetectionCircle() {
       Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 3, _targetMask);
        if (hit.Length > 0) {
          
            GameObject[] damageable = new GameObject[hit.Length];
           
            for (int i = 0; i < hit.Length; i++) {
                IDamagable enemyMonster = hit[i].gameObject.GetComponent<IDamagable>();
                if (enemyMonster != null) {
                    damageable[i] = hit[i].gameObject;
                }
            }

            GetTargetMonster(_currentMonster, damageable);
        } 
    }
}
