using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Monster> enemies;
    [SerializeField] private Player player;
    [SerializeField] private Monster _blobMonster;
    [SerializeField] private GameObject _castle;
    Monster target;
    void Start()
    {
        StartCoroutine(SpawnRoutine()); 
    }

    IEnumerator SpawnRoutine() {
        while(true) {
      
            yield return new WaitForSeconds(8f);
            Monster blob = Instantiate(_blobMonster,new Vector3(UnityEngine.Random.Range(2f,5f),UnityEngine.Random.Range(-3f,5f),0),Quaternion.identity,this.transform);
            blob.OnDeathEvent += Blob_OnDeathEvent;
            enemies.Add(blob.GetComponent<BlobMonster>());
            if (_castle == null) {
                StopCoroutine(SpawnRoutine());
            }
            else  
            blob.GetComponent<BlobMonster>().AIStateMachine();
        }
    }

    private void Blob_OnDeathEvent(object sender, Monster.MonsterChosenEventArgs e) {
       e.chosenPet.OnDeathEvent -= Blob_OnDeathEvent;
        enemies.Remove(e.chosenPet);
    }
}
