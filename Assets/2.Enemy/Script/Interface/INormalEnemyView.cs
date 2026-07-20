using System;
using UnityEngine;

public interface INormalEnemyView 
{
	public event Action OnAttack;
    public Vector2 TargetPosition { get; }
	public void UpdateTargetPosition(Vector2 targetPosition);
	
}
