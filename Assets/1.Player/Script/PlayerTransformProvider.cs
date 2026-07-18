using UnityEngine;

/// <summary>
/// Player 위치 정보를 담는 공급자
/// </summary>
public static class PlayerTransformProvider
{
    public static Transform PlayerTransform { get; private set; }
	public static void Resgister(Transform transform)
	{
		PlayerTransform = transform;
	}
	
	public static void Unregister()
	{
		PlayerTransform = null;
	}
}
