﻿using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.Localization.Settings;

namespace TSS.Core
{
    [UsedImplicitly] 
    [RuntimeOrder(ERuntimeOrder.SubsystemRegistration, 1)]
    internal class RuntimeLocalization : IRuntimeLoader
    {
        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            await LocalizationSettings.InitializationOperation
                .ToUniTask(cancellationToken: cancellationToken);
        }
        public void Dispose() { }
    }
}