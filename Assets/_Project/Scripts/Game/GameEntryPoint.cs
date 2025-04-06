using System;
using System.Threading;
using JetBrains.Annotations;
using TSS.ContentManagement;
using TSS.Core;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace LudumDare57.Game
{
    [UsedImplicitly]
    public class GameEntryPoint : IInitializable, IDisposable, ITickable
    {
        private CancellationTokenSource _cts;
        
        public void Initialize()
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(Runtime.CancellationToken);
            GameContext.CancellationToken = _cts.Token;
            
            GameContext.Hole = Object.Instantiate(CMS.HolePrefab);
            GameContext.Player = Object.Instantiate(CMS.PlayerPrefab);
            GameContext.Inventory = Object.Instantiate(CMS.InventoryPrefab);
            
            GameContext.StateMachine = new GameStateMachine();
            
            GameContext.StateMachine.Run();
        }

        public void Tick()
        {
            GameContext.StateMachine.Update();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}