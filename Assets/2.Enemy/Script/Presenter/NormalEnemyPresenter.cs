using UnityEngine;
using Zenject;

public class NormalEnemyPresenter : MonoBehaviour
{
	private INormalEnemyStatModel _normalEnemyModel;
	private INormalEnemyView _view;

	[Inject]
	public void Initialize(
		INormalEnemyStatModel normalEnemyModel,
		INormalEnemyView view)
	{
		_normalEnemyModel = normalEnemyModel;
		_view = view;
		SubscribeEvent();
	}
    void Start()
    {
        
    }

    
    void Update()
    {
		if (PlayerTransformProvider.PlayerTransform == null) return;

		_view.UpdateTargetPosition(PlayerTransformProvider.PlayerTransform.position);
    }

	private void SubscribeEvent()
	{

	}
}
