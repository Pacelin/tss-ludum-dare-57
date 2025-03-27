using System;
using DG.Tweening;
using JetBrains.Annotations;
using TSS.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace TSS.Tweening
{
    [Serializable]
    [ScriptableTweenPath("Misc/Nested Tween", 1001)]
    [NotPreset]
    public class ScriptableTweenTween : IScriptableTweenItemNoTarget
    {
        [SerializeField] private ETweenConnectBehaviour _connectBehaviour;
        [SerializeField] private ScriptableTween _tween;

        public void AddTween(Sequence sequence)
        {
            if (_connectBehaviour == ETweenConnectBehaviour.Append)
                sequence.Append(_tween.GetNewTween());
            else
                sequence.Join(_tween.GetNewTween());
        }
    }
    
    [Serializable]
    [ScriptableTweenPath("Misc/Interval", 1001)]
    public class ScriptableTweenInterval : IScriptableTweenItemNoTarget
    {
        [SerializeReference] private IFloatValueProvider _interval;

        public void AddTween(Sequence sequence) =>
            sequence.AppendInterval(_interval.Get());
    }
    
    [Serializable]
    [ScriptableTweenPath("Misc/Callback", 1002)]
    [NotPreset]
    public class ScriptableTweenCallback : IScriptableTweenItemNoTarget
    {
        [SerializeField] private ETweenConnectBehaviour _connectBehaviour;
        [SerializeField] private UnityEvent _callback;

        public void AddTween(Sequence sequence)
        {
            if (_connectBehaviour == ETweenConnectBehaviour.Append)
                sequence.AppendCallback(() => _callback.Invoke());
            else
                sequence.JoinCallback(() => _callback.Invoke());
        }
    }
    
    [Serializable]
    [ScriptableTweenPath("Misc/Event", 1003)]
    [NoFoldout]
    public class ScriptableTweenEvent : IScriptableTweenItem<ScriptableTweenEventHandler>
    {
        [SerializeField] private ETweenConnectBehaviour _connectBehaviour;

        public void AddTween(Sequence sequence, ScriptableTweenEventHandler obj)
        {
            if (_connectBehaviour == ETweenConnectBehaviour.Append)
                sequence.AppendCallback(obj.OnTrigger);
            else
                sequence.JoinCallback(obj.OnTrigger);
        }
    }
    
    [PublicAPI]
    public abstract class ScriptableTweenEventHandler : MonoBehaviour
    {
        public abstract void OnTrigger();
    }
}