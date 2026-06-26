using Unity.VisualScripting;
using UnityEngine;

public interface IFSMMachine 
{
	/// <summary>
	/// 현재 상태
	/// </summary>
	IState CurrentState { get; }
	

}
