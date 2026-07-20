using System;


/// <summary>
/// MonoBehavior는 AnimEvent를 호출하기 위해 선언함
/// </summary>
public interface IAnimEventListener
{

	/// <summary>
	/// 공격 시작시 호출 할 이벤트
	/// </summary>
	public event Action OnAttackStart;

	/// <summary>
	/// 공격 종료시 호출 할 이벤트
	/// </summary>
	public event Action OnAttackEnd;

	//밑에 두 함수는 위에 있는 Action을 Invoke하는 역할만 한다(함수를 담는 주체는 Action 변수이다)
	
	/// <summary>
	/// 공격 시작 시 AnimEvent로 호출 할 함수
	/// </summary>
	public void AnimEventAttackStart();
	/// <summary>
	/// 공격 종료 시 AnimEvent로 호출 할 함수
	/// </summary>
	public void AnimEventAttackEnd();
}
