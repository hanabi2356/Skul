using UnityEngine;

public interface INormalEnemyHudView 
{
	/// <summary>
	/// UI 초기화
	/// </summary>
	public void Initialize();

	/// <summary>
	/// HP 스탯 UI와 연동을 하여 화면에 출력
	/// </summary>
	/// <param name="currentHP">현재 HP</param>
	/// <param name="maxHP">최대 HP</param>
	public void Show(int currentHP, int maxHP);

	/// <summary>
	/// Progress Bar 숨기기 함수
	/// </summary>
	public void Hide();


	

	
}
