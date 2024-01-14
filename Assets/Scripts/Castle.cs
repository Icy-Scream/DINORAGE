using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle : MonoBehaviour,IDamagable
{
    [SerializeField] private int _health;
    [SerializeField] private Slider _healthBar;

    private void Start() {
        _healthBar.value = _health;
    }

    public void Damage(int damage) {
        _health -= damage;
        _healthBar.value = _health;
        Destruction();
    }

    private void Destruction() { 
        if(_health <= 0) { 
            Destroy(gameObject);
        }
    }

    public void LoadCastleScene() {
        SceneManager.LoadScene(1);
    }
}
