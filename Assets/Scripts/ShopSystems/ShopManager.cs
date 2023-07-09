using System;

namespace GitIntegration.ShopSystems
{
	public class ShopManager : MonoSingleton<ShopManager>
	{
		private void Start()
		{
			SubscribeToEvents();
		}
		
		protected override void OnDestroy()
		{
			if(Instance == this)
			{
				UnsubscribeFromEvents();	
			}
			base.OnDestroy();
			
		}
		private void SubscribeToEvents()
		{
			EventService.Instance.DiagButtonClickedByBot += OnDiagButtonClickedByBot;
		}
		private void UnsubscribeFromEvents()
		{
			EventService.Instance.DiagButtonClickedByBot -= OnDiagButtonClickedByBot;
		}

		public void OnDiagButtonClickedByBot(ButtonType buttonType)
		{
			if(buttonType == ButtonType.Shop)
			{
				var hero = HeroManager.Instance.GetCurrentHero();
				var shopList = hero.ShopLists.Dequeue();
				hero.ShopLists.Enqueue(shopList);
				ShopListRenderer.Instance.Render(shopList);
			}
		}
	}
}