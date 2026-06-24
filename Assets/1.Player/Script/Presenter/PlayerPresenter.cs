using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
	private IPlayerStatModel _statModel;
	private IPlayerView _view;
	private SkulDataLoader _dataLoader;


	[Inject]
	public void Initialize(IPlayerStatModel statModel, IPlayerView view, SkulDataLoader dataLoader)
	{
		Debug.Log("Init Call");
		_statModel = statModel;
		_view = view;
		_dataLoader = dataLoader;

		
	}
	private async void Awake()
	{
		SkulStatData loadData = _dataLoader.SkulStatDataLoad("LittleBorn");

	}
}

