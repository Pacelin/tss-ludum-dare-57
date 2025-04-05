using System.Threading;
using Cysharp.Threading.Tasks;
using TSS.Core;

namespace LudumDare57.Game
{
    public class GameStateMachine
    {
        private CancellationTokenSource _cts;
        private GameState _currentState;
        
        public GameStateMachine()
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(Runtime.CancellationToken);
        }
        
        public void Run()
        {
            _currentState = new GameStartState();
            _currentState.Start(_cts.Token).Forget();
        }

        public void Update()
        {
            _currentState?.Update();
        }
        
        public void Stop()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        public void SwitchTo(GameState state)
        {
            if (_cts != null)
            {
                _currentState = state;
                _currentState.Start(_cts.Token).Forget();
            }
        }
    }
}