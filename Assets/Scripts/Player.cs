using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Player : MonoBehaviour
{
    public static Player Instance;
   [SerializeField] private List<Monster> playerList = new List<Monster>();
    private GameInput _gameInput;

    private void Awake() {
        _gameInput = gameObject.GetComponent<GameInput>();
    }

    void Start()
    {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Update() {
        transform.Translate(_gameInput.GetMovementVector() * 2* Time.deltaTime);
    }

    private void MouseTracking() {
        var mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDirection = mouseWorld - this.gameObject.transform.up;
        var mouseAngle = Vector2.Angle(this.gameObject.transform.up, mouseDirection.normalized);
        var Dot = Vector2.Dot(this.gameObject.transform.up, mouseDirection.normalized);
        var rotationAxis = Vector3.Cross(this.gameObject.transform.up, mouseDirection.normalized);

        float clockwise = 1;

        if (rotationAxis.z < 0) {
            clockwise = -1;
        }
        else
            clockwise = 1;
        Debug.Log(Dot);

        if (Dot <= 0.8f)
            transform.Rotate(0, 0, mouseAngle * 1 * clockwise * Time.deltaTime);
    }

    public void AddPet(Monster monster) {
        monster.OnDeathEvent += Monster_OnDeathEvent;
        playerList.Add(monster);
        //log.AddEvent($"Player purchased a new Pet: {pet}");
    }

    private void Monster_OnDeathEvent(object sender, Monster.MonsterChosenEventArgs e) {
        e.chosenPet.OnDeathEvent -= Monster_OnDeathEvent;
        playerList.Remove(e.chosenPet);
    }

    public List<Monster> GetPetList() {
        return playerList;
    }
}
