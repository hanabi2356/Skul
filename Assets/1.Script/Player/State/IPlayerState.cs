using UnityEngine;

/// <summary>
/// 상태의 진입, 퇴장 및 현재 상태일 때에 대한 로직을 담는 인터페이스
/// Enter, Exit는 상태 변환 시 한번 만 실행 됨
/// </summary>
public interface IPlayerState 
{
    /// <summary>
    /// 상태 진입 시 실행할 로직 작성
    /// </summary>
    public void Enter();

    /// <summary>
    /// 현재 상태에서 Update문에서 실행 되어야할 로직 작성
    /// </summary>
    public void Execute();

    /// <summary>
    /// 현재 상태 탈출 시 실행 되어야 하는 로직
    /// </summary>
    public void Exit();

    /// <summary>
    /// 상태 전이조건 정의
    /// </summary>
    public void SetupTransitions();

}
