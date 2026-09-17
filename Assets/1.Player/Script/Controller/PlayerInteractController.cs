using UnityEngine;

public class PlayerInteractController 
{
    private readonly PlayerInteractableDetector _detector;

	public PlayerInteractController(PlayerInteractableDetector detector)
	{
		_detector = detector;
	}	

	public void TryInteract()
	{
		if(_detector.Current != null)
		{
			_detector.Current.Interact();
		}
	}	
}
