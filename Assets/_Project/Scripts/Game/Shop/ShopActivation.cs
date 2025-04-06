namespace LudumDare57.Game.Shop
{
    public class ShopActivation : ActivationTrigger
    {
        protected override void OnActivate() => GameContext.Shop.Show();
    }
}