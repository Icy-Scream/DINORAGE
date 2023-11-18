using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMonster : MonoBehaviour, IDamagable
{
    [SerializeField] private Animator _playerAnimation;
    [SerializeField] private int _health = 20;
    private bool _attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_attacking)
        transform.position += Vector3.left * 0.3f * Time.deltaTime;
    }

    public void Damage(int damage) {
        _health -= damage;
        Destruction();
    }

    private void Destruction() {
        if (_health <= 0) {
            Destroy(gameObject);
        }
    }


    private void OnCollisionStay2D(Collision2D collision) {

        if (collision.transform.TryGetComponent<IDamagable>(out IDamagable damage)) {
            Debug.Log("HELLO");
            _attacking = true;
            _playerAnimation.SetBool("Attack", _attacking);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.transform.TryGetComponent<IDamagable>(out IDamagable damage)) {
            _attacking = false;
            _playerAnimation.SetBool("Attack", _attacking);
        }
    }
}
