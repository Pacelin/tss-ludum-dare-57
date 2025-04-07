using Cysharp.Threading.Tasks;
using TSS.ContentManagement;
using TSS.Core;
using TSS.SceneManagement;
using TSS.Tweening;
using UnityEngine;

namespace LudumDare57.Game.GameEvent
{
    public class EndgameActivation : ActivationTrigger
    {
        [SerializeField] private ScriptableTween _tween;
        
        protected override void OnActivate()
        {
            End().Forget();
        }

        private async UniTaskVoid End()
        {
            GameContext.Hole.SetStop(true);
            _tween.Play();
            await _tween.WaitWhilePlay();
            await SceneManager.Scene(CMS.Scenes.MainMenu).Single().Load(Runtime.CancellationToken);
        }
    }
}