// Auto-generated code. Reference: "Packages/com.tss.cms/Editor/CMSGenerator.cs"

// ReSharper disable RedundantUsingDirective
#pragma warning disable CS1998

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using JetBrains.Annotations;
using UnityEngine;
using LudumDare57.Game;
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
 

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
			await Scenes.Initialize(cancellationToken);
			HolePrefab = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/P_Hole_View.prefab")
				.ToUniTask(cancellationToken: cancellationToken)).GetComponent<HoleComponent>();
			PlayerPrefab = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/P_Player.prefab")
				.ToUniTask(cancellationToken: cancellationToken)).GetComponent<PlayerComponent>();
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