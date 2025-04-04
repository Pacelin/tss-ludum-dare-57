using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using mixpanel;

namespace TSS.Core
{
    [UsedImplicitly]
    [RuntimeOrder(ERuntimeOrder.SystemRegistration)]
    public class RuntimeMixpanel : IRuntimeLoader
    {
        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            Mixpanel.Init();
            await UniTask.WaitUntil(Mixpanel.IsInitialized, cancellationToken: cancellationToken);
            Mixpanel.Track("$session_start");
        }

        public void Dispose()
        {
        }
    }
}