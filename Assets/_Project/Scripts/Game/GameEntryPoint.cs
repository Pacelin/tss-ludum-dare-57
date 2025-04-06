using System;
using System.Threading;
using JetBrains.Annotations;
using mixpanel;
using R3;
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
        private IDisposable _disposable;
        
        public void Initialize()
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(Runtime.CancellationToken);
            GameContext.CancellationToken = _cts.Token;
            GameContext.Coins.Value = 0;
            GameContext.PickaxeLevel = 1;
            GameContext.PickaxeStrength = 0;
            GameContext.IncomeMultiplier = 1;
            _disposable = GameContext.Coins.Subscribe(c => Mixpanel.Register("Coins", c));
            Mixpanel.Track("fun_start");
            
            GameContext.Hole = Object.Instantiate(CMS.HolePrefab);
            GameContext.Player = Object.Instantiate(CMS.PlayerPrefab);
            GameContext.Inventory = Object.Instantiate(CMS.InventoryPrefab);
            GameContext.Shop = Object.Instantiate(CMS.ShopPrefab);
            GameContext.LocationView = Object.Instantiate(CMS.LocationPrefab);
            
            GameContext.StateMachine = new GameStateMachine();
            
            GameContext.StateMachine.Run();
        }

        public void Tick()
        {
            GameContext.StateMachine.Update();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}