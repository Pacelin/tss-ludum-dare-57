using VContainer;
using VContainer.Unity;

namespace LudumDare57.Game
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}