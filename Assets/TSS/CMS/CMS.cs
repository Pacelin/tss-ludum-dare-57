// Auto-generated code. Reference: "Packages/com.tss.cms/Editor/CMSGenerator.cs"

// ReSharper disable RedundantUsingDirective
#pragma warning disable CS1998

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using JetBrains.Annotations;
using UnityEngine;
using TSS.Core;

namespace TSS.ContentManagement
{
    [PublicAPI]
    [UsedImplicitly]
    [RuntimeOrder(ERuntimeOrder.SystemRegistration)]
    public class CMS : IRuntimeLoader
    {
		public static Transform GameObject { get; private set; }
 

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
			GameObject = (await Addressables.LoadAssetAsync<GameObject>("Assets/Scenes/GameObject.prefab")
				.ToUniTask(cancellationToken: cancellationToken)).GetComponent<Transform>();
        }

        public void Dispose() { }
    }
}