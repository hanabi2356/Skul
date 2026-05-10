using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private float attackDelay = 0.2f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Attack();
        }
    }
    private void Attack()
    {
        Debug.Log("Attack Call");
    }
    private IEnumerator IEAttack()
    {

        yield return new WaitForSeconds(attackDelay);
    }
}
