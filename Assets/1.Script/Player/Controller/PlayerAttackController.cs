using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
public class PlayerAttackController : MonoBehaviour
{
    public event Action onAttackStarted;
    public event Action onAttackFinished;
    PlayerBase playerBase;
    [SerializeField, Label("공격 간 딜레이")] private float attackDelay = 0.2f;
    [SerializeField, Label("공격 초기화 달레이")] private float attackCountResetDelay = 0.5f;
    private float lastAttackTime=0.0f;

    [Header("확인용 변수(조작 X)")]
    [field : SerializeField]public int attackCount { get; private set; } = 0;
    [field : SerializeField] public bool isAttacking { get; private set; } =  false;
    private Coroutine attackCoroutine;

    void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
  
    }

    void Update()
    {
        if(!isAttacking && attackCount > 0)
        {
            if(Time.time - lastAttackTime > attackCountResetDelay)
                ComboReset();
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && !isAttacking && playerBase.moveController.isDashing)
        {
            AttackStarted();
        }
    }

    private IEnumerator IEAttack()
    {
        isAttacking = true;
        attackCount++;
        yield return new WaitForSeconds(attackDelay);
        lastAttackTime = Time.time;
        isAttacking = false;

        if (attackCount >= 1)
        {
            yield return new WaitForSeconds(attackDelay);
            attackCount = 0;
        }
        

    }
    private void AttackStarted()
    {
        if (attackCount >= 1 && isAttacking)
            return;


        if (!isAttacking)
            StopCoroutine(IEAttack());

        attackCoroutine = StartCoroutine(IEAttack());

        onAttackStarted.Invoke();
    }
    private void AttackFinished()
    {
        Debug.Log("AttackFinished Call");
        
    }
    private void ComboReset()
    {
        attackCount = 0;
    }
    private void OnDisable()
    {
        attackCoroutine = null;
    }

}
