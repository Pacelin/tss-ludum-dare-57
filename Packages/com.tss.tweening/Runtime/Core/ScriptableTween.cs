using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace TSS.Tweening
{
    [PublicAPI]
    public sealed class ScriptableTween : ScriptableTweenBase
    {
        public bool IsPlaying => _sequence?.IsPlaying() ?? false;

        private Sequence _sequence;

        private void OnEnable()
        {
            if (PlayOnEnable)
                Play();
        }

        private void OnDisable()
        {
            if (_sequence == null) return;
            
            _sequence.Kill();
            _sequence = null;
        }

        public UniTask WaitForCompletion()
        {
            Assert.IsNotNull(_sequence);
            return _sequence.AsyncWaitForCompletion().AsUniTask();
        }

        public UniTask WaitForStart()
        {
            Assert.IsNotNull(_sequence);
            return _sequence.AsyncWaitForStart().AsUniTask();
        }
        
        public void Play()
        {
            if (CacheTween)
            {
                if (_sequence == null)
                    _sequence = GetNewTween();
            }
            else
            {
                if (_sequence != null)
                {
                    _sequence.Kill();
                    _sequence = null;
                }

                _sequence = GetNewTween();
            }

            _sequence.Restart();
        }

        public void TogglePause()
        {
            Assert.IsNotNull(_sequence);
            _sequence.TogglePause();
        }

        public void Complete(bool withCallbacks)
        {
            Assert.IsNotNull(_sequence);
            _sequence.Complete(withCallbacks);
        }

        public void Resume()
        {
            Assert.IsNotNull(_sequence);
            _sequence.Play();
        }

        public void Pause()
        {
            Assert.IsNotNull(_sequence);
            _sequence.Pause();
        }

        private void BuildSequence()
        {
            _sequence = DOTween.Sequence(this);
            var targetIndex = 0;
            foreach (var scriptableTweenItem in Items)
            {
                if (scriptableTweenItem is IScriptableTweenItemNoTarget)
                    scriptableTweenItem.AddTween(_sequence, null);
                else
                    scriptableTweenItem.AddTween(_sequence, Targets[targetIndex++]);
            }
        }

        internal Sequence GetNewTween()
        {
            _sequence = DOTween.Sequence(this)
                .SetLoops(Loops, LoopType);
            var targetIndex = 0;
            foreach (var scriptableTweenItem in Items)
            {
                if (scriptableTweenItem is IScriptableTweenItemNoTarget)
                    scriptableTweenItem.AddTween(_sequence, null);
                else
                    scriptableTweenItem.AddTween(_sequence, Targets[targetIndex++]);
            }

            return _sequence;
        }
    }
}