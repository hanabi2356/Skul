using UnityEngine;
using Zenject;
public class StoreInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<StoreManager>().AsSingle();
	}
}
