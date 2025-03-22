using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.Localization.Settings;

namespace TSS.Core
{
    [UsedImplicitly] 
    [RuntimeOrder(ERuntimeOrder.SubsystemRegistration, 1)]
    internal class RuntimeLocalization : IRuntimeLoader
    {
        public UniTask Initialize(CancellationToken cancellationToken) =>
            LocalizationSettings.InitializationOperation
                .ToUniTask(cancellationToken: cancellationToken);
        public void Dispose() { }
    }
}