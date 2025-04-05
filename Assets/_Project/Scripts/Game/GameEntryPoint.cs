using System;
using JetBrains.Annotations;
using VContainer.Unity;

namespace LudumDare57.Game
{
    [UsedImplicitly]
    public class GameEntryPoint : IInitializable, IDisposable, ITickable
    {
        public void Initialize()
        {
            GameContext.StateMachine = new GameStateMachine();
            GameContext.StateMachine.Run();
        }

        public void Tick()
        {
            GameContext.StateMachine.Update();
        }

        public void Dispose()
        {
            GameContext.StateMachine.Stop();
        }
    }
}