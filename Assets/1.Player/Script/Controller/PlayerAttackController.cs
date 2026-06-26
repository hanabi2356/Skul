using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using Unity.VisualScripting;
public class PlayerAttackController 
{
	public event Action<int> OnAttackStarted;
    public event Action OnAttackFinished;
    

    private IPlayerStatModel _statModel;

	private float _lastAttackTime = 0.0f;
    private float _lastInputTime = -1.0f;

	public int AttackCount { get; private set; } = 0;
    public bool IsAttacking { get; private set; } =  false;
    public bool IsReset {  get; private set; } = false;

	public PlayerAttackController(IPlayerStatModel statModel)
	{
		_statModel = statModel;
	}
	
	public void TryAttack()
	{
		_lastAttackTime = Time.time;
		IsReset = false;
	}
	
	public void OnAttackStateEnter()
	{
		IsAttacking = true;
		_lastInputTime = -1.0f;
		AttackCount++;

		OnAttackStarted?.Invoke(AttackCount);
	}
	public void OnAttackStateExit()
	{
		_lastAttackTime = Time.time;
		IsAttacking = false;

		OnAttackFinished?.Invoke();
	}
	public void ComboCoolDown()
	{
		if(!IsAttacking && AttackCount>0)
		{
			if(Time.time-_lastAttackTime > _statModel.FinalAttackCountResetDelay)
			{
				ResetCombo();
			}
		}
	}
	private void ResetCombo()
	{
		AttackCount = 0;
		IsReset = true;
	}
    /*void Update()
    {
        *//*if(!isAttacking && attackCount > 0 )
        {
            if(Time.time - lastAttackTime > attackCountResetDelay )
            {
                ComboReset();
            }
        }
        if(!isAttacking && (Time.time - lastInputTime <= inputBufferTime))
        {
            if (attackCount >= maxAttackCount)
                return;

            AttackStarted();

        }*//*
        
    }*/
  

   /* private IEnumerator IEAttack()
    {
        lastInputTime = -1.0f;

        yield return new WaitUntil(()=>!playerBase.animController.isAttackAnimPlaying);

        AttackFinished();

    }*/
   /* private void AttackStarted()
    {
        *//*if (attackCount >= 1 && isAttacking)
            return;*//*


        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        isReset=false;
        isAttacking = true;

        attackCount++;

        attackCoroutine = StartCoroutine(IEAttack());

        OnAttackStarted.Invoke();
    }*/
   /* private void AttackFinished()
    {
        lastAttackTime = Time.time;
        isAttacking = false;
        attackCoroutine = null;

        if(OnAttackFinished !=null)
            OnAttackFinished?.Invoke();
    }*/
   /* private void ComboReset()
    {
        attackCount = 0;
        isReset = true;
    }*/
   /* private void OnDisable()
    {
        if(attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = null;
        isAttacking = false;
    }*/

}
