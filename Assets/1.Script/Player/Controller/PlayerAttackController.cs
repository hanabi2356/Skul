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
    [SerializeField, Label("공격 최대 횟 수")] private int maxAttackCount = 2;
    private float lastAttackTime=0.0f;
    [SerializeField] private float inputBufferTime = 0.2f;
    private float lastInputTime = -1.0f;

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
        if(!isAttacking && (attackCount > 0 || attackCount <= maxAttackCount))
        {
            if(Time.time - lastAttackTime > attackCountResetDelay )
            {
                ComboReset();
            }
        }
        if(!isAttacking && (Time.time - lastInputTime <= inputBufferTime))
        {
            AttackStarted();

        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(!playerBase.moveController.isDashing)
            {
                if(attackCount < maxAttackCount || !isAttacking)
                    lastInputTime = Time.time;
            }
        }
    }

    private IEnumerator IEAttack()
    {
        isAttacking = true;
        lastInputTime = -1.0f;

        if(attackCount < maxAttackCount)
        attackCount++;
        yield return new WaitUntil(()=>!playerBase.animController.isAttackAnimPlaying);

        yield return null;

        lastAttackTime = Time.time;
        isAttacking = false;

        onAttackFinished.Invoke();

        AttackFinished();

    }
    private void AttackStarted()
    {
        /*if (attackCount >= 1 && isAttacking)
            return;*/


        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = StartCoroutine(IEAttack());

        onAttackStarted.Invoke();
    }
    private void AttackFinished()
    {
        isAttacking = false;
        onAttackFinished?.Invoke();
    }
    private void ComboReset()
    {
        attackCount = 0;
    }
    private void OnDisable()
    {
        if(attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = null;
        isAttacking = false;
    }

}
