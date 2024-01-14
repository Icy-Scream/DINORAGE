using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class LifeSteal : MonoBehaviour

{
    [SerializeField] private int _damage = 4;
    [SerializeField] private float DPS;
    [SerializeField] private LayerMask _target;
    [SerializeField] public bool _isAttacking = false;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private GameObject lineRenderer;

    private float lifeReserve;
    private Transform endVFX;
    private LineRenderer lr;
    void Start()
    {
        gameInput.OnLeftClickPerformed += GameInput_OnLeftClick;
        gameInput.OnLeftClickCancel += GameInput_OnLeftClickCancel;
        lineRenderer = Instantiate(lineRenderer,this.transform);
        lr = lineRenderer.GetComponentInChildren<LineRenderer>();
        endVFX = lineRenderer.transform.GetChild(1);
    }

    private void Update() {
        if (_isAttacking) {
            lineRenderer.gameObject.SetActive(true);
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endVFX.transform.position = new Vector3(mousePos.x,mousePos.y,0);
            var direction = mousePos;
            lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 0.5f,0));
            lr.SetPosition(1,new Vector3(direction.x,direction.y,0));
        }
        else if (!_isAttacking)
            lineRenderer.gameObject.SetActive(false);

    }

    private void GameInput_OnLeftClickCancel(object sender, System.EventArgs e) {
        _isAttacking = false;
        StopCoroutine(LifeDrainRoutine());
        Debug.Log("END");
    }

    private void GameInput_OnLeftClick(object sender, System.EventArgs e) {
            Debug.Log("LIFE DRAIN");
            _isAttacking=true;
            StartCoroutine(LifeDrainRoutine());
    }

    private IEnumerator LifeDrainRoutine() {
        while (_isAttacking) {
            
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(transform.position, (mousePos - transform.position).normalized, Mathf.Infinity, _target);
            yield return new WaitForSeconds(DPS);
            try{
                if (hit.transform.TryGetComponent<IDamagable>(out IDamagable enemy)) {
                    enemy.Damage(_damage);
                    Debug.Log("DRAINING");
                }
            }
            catch (System.NullReferenceException) {
                    Debug.Log("MISS");
                _isAttacking = false;
            }
          
        }    
    }
}
