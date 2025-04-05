// Auto-generated code. Reference: "Packages/com.tss.cms/Editor/CMSGenerator.cs"

// ReSharper disable RedundantUsingDirective
#pragma warning disable CS1998

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using JetBrains.Annotations;
using UnityEngine;
using LudumDare57.Hole;
using TSS.Core;

namespace TSS.ContentManagement
{
    [PublicAPI]
    [UsedImplicitly]
    [RuntimeOrder(ERuntimeOrder.SystemRegistration)]
    public class CMS : IRuntimeLoader
    {
 

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
			await Scenes.Initialize(cancellationToken);
			await Hole.Initialize(cancellationToken);
			await Player.Initialize(cancellationToken);
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
		[PublicAPI]
		public static class Hole
		{
			public static HoleConfig Config { get; private set; }
			public static HoleView Prefab { get; private set; }

			public static async UniTask Initialize(CancellationToken cancellationToken)
			{
				Config = await Addressables.LoadAssetAsync<HoleConfig>("Hole Config")
					.ToUniTask(cancellationToken: cancellationToken);
				Prefab = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/P_Hole_View.prefab")
					.ToUniTask(cancellationToken: cancellationToken)).GetComponent<HoleView>();
			}
		}
		[PublicAPI]
		public static class Player
		{
			public static PlayerView Prefab { get; private set; }

			public static async UniTask Initialize(CancellationToken cancellationToken)
			{
				Prefab = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/P_Player.prefab")
					.ToUniTask(cancellationToken: cancellationToken)).GetComponent<PlayerView>();
			}
		}
    }
}