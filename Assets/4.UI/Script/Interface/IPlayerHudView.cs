using UnityEngine;

public interface IPlayerHudView 
{
	public void Initialize();
	public void SetHP(int currentHP, int maxHP);
	public void SetGold(int currentGold);
}
