using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Centipede : Pet, IDamagable {
    [SerializeField] private Slider _hungerSlider;
    [SerializeField] private Slider _happinessSlider;
    [SerializeField] private GameObject _uiIcon;
    [SerializeField] private Animator animator;
    private int _hitDetection = 9;
    private bool _attacking = false;
    private bool changeDirection = false;
    private float _direction = 1f;
    private PlayerLog _playerLog;
    public Centipede(int maxHappiness, int maxHunger, int startingHappiness, int startingHunger, float timerLength, int power, int agility, int health, int intelligence, int stamina, float weight) : base(maxHappiness, maxHunger, startingHappiness, startingHunger, timerLength, power, agility, health, intelligence, stamina, weight) {

    }

    private void Start() {
        _happinessSlider.maxValue = base.MaxHappiness;
        _hungerSlider.maxValue = base.MaxHunger;
        name = "Centipede";
        var petList = GameObject.FindGameObjectWithTag("MAINUI");
        _uiIcon.transform.parent = petList.transform;

        StartCoroutine(TimerRoutine());
        _playerLog = FindObjectOfType<PlayerLog>().GetComponent<PlayerLog>();
        Debug.Log(_currentHappiness + "HAPPPY");
    }

    public override void Update() {
        if(!_attacking)
        Movement();
        _happinessSlider.value = _currentHappiness;
        _hungerSlider.value = _currentHunger;
    }

    private void Movement() {
        this.transform.position += Vector3.right * (_agility/100f) * Time.deltaTime;
    }


    private void OnDestroy() {
        Destroy(_uiIcon);
    }

    IEnumerator MovementRoutine() {
        changeDirection = true;
        if (_direction == 1f)
            _direction = -1f;
        else
            _direction = 1f;
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        changeDirection = false;
    }

    private void DepleteStats() {
        _currentHunger++;
        _currentHappiness--;
    }

    public override void DoInteractions(IInteractions interactions) {
        switch (interactions) {
            case PetInteractions:
                _currentHunger++;
                _currentHappiness++;
                break;

        };
    }

    private IEnumerator TimerRoutine() {
        while (true) {
            yield return new WaitForSecondsRealtime(10f);
            DepleteStats();
            _playerLog.AddEvent($"{name} WAS NOT FEED IN A WHILE HAPPINESS -- AND HUNGER ++");
        }
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
        if (collision.transform.TryGetComponent<IDamagable>(out IDamagable damage) && _hitDetection == LayerMask.GetMask("Villian")) {
            Debug.Log(collision.gameObject.layer);
            if(collision.gameObject.layer == 9) {
                _attacking = true;
                animator.SetBool("Attack", _attacking);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.transform.TryGetComponent<IDamagable>(out IDamagable damage) && _hitDetection == LayerMask.GetMask("Villian")) {
            if (collision.gameObject.layer == 9) {
                _attacking = false;
                animator.SetBool("Attack", _attacking);
            }
        }
    }
}
