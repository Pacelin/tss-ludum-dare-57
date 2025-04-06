// Auto-generated code. Reference: "Packages/com.tss.cms/Editor/CMSGenerator.cs"

// ReSharper disable RedundantUsingDirective
#pragma warning disable CS1998

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using JetBrains.Annotations;
using UnityEngine;
using LudumDare57.Game;
using LudumDare57.Inventory;
using LudumDare57.Game.Shop;
using TSS.Core;

namespace TSS.ContentManagement
{
    [PublicAPI]
    [UsedImplicitly]
    [RuntimeOrder(ERuntimeOrder.SystemRegistration)]
    public class CMS : IRuntimeLoader
    {
		public static HoleComponent HolePrefab { get; private set; }
		public static PlayerComponent PlayerPrefab { get; private set; }
		public static ItemsCollection InventoryItems { get; private set; }
		public static InventoryComponent InventoryPrefab { get; private set; }
		public static ItemComponent InventoryItemPrefab { get; private set; }
		public static ShopComponent ShopPrefab { get; private set; }
 

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
			await Scenes.Initialize(cancellationToken);
			HolePrefab = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/P_Hole_View.prefab")
				.ToUniTask(cancellationToken: cancellationToken)).GetComponent<HoleComponent>();
			PlayerPrefab = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/P_Player.prefab")
				.ToUniTask(cancellationToken: cancellationToken)).GetComponent<PlayerComponent>();
			InventoryItems = await Addressables.LoadAssetAsync<ItemsCollection>("Assets/_Project/Configs/Inventory Items.asset")
				.ToUniTask(cancellationToken: cancellationToken);
			InventoryPrefab = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/P_Inventory.prefab")
				.ToUniTask(cancellationToken: cancellationToken)).GetComponent<InventoryComponent>();
			InventoryItemPrefab = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/P_InventoryItem.prefab")
				.ToUniTask(cancellationToken: cancellationToken)).GetComponent<ItemComponent>();
			ShopPrefab = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/UI/P_Shop.prefab")
				.ToUniTask(cancellationToken: cancellationToken)).GetComponent<ShopComponent>();
        }

        public void Dispose() { }

		[PublicAPI]
		public static class Scenes
		{
			public const string MainMenu = "Assets/Scenes/1_Menu.unity";
			public const string Game = "Assets/Scenes/2_Game.unity";

			public static async UniTask Initialize(CancellationToken cancellationToken)
			{
			}
		}
    }
}