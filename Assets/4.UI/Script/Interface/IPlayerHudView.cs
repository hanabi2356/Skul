using UnityEngine;

public interface IPlayerHudView 
{
	public void Initialize();
	public void SetHP(int currentHP, int maxHP);
	
}
