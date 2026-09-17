using UnityEngine;

/// <summary>
/// 키를 눌러 상호작용 할 수 있는 오브젝트에 붙이는 인터페이스
/// </summary>
public interface IInteractable 
{
	/// <summary>
	/// 최종 작동 로직이 담겨 있는 함수를 호출하는 함수
	/// </summary>
    public void Interact();
}
