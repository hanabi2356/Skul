using UnityEngine;

public interface INormalEnemyView 
{
    public Vector2 TargetPosition { get; }
	public void UpdateTargetPosition(Vector2 targetPosition);
	
}
