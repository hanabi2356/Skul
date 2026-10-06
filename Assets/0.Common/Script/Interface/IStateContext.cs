using System;

public interface IStateContext<TStateEnum> where TStateEnum : struct, Enum
{
	TStateEnum CurrentStateEnum { get; }

	/// <summary>
	/// 상태 변경
	/// </summary>
	/// <param name="state">변경될 상태</param>
	/// <param name="stateEnum">변경될 상태에 맞춰 애니메이션을 컨트롤 하기 위한 Enum 값</param>
	void ChangeState(IState state, TStateEnum stateEnum);
}
