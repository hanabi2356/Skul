using UnityEngine;

public class PlayerInteractableDetector : MonoBehaviour
{
	public IInteractable Current { get; private set; }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.TryGetComponent(out IInteractable interactable))
		{
			Current = interactable;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.TryGetComponent(out IInteractable interactable))
		{
			if(Current == interactable)
			{
				Current = null;
			}
		}
	}
}
	
