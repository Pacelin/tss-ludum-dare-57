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
            try
            {
                Mixpanel.Track("$session_start");
                Mixpanel.Flush();
            }
            catch
            {
                Mixpanel.Disable();
            }
        }

        public void Dispose()
        {
        }
    }
}