using DG.Tweening;
using TSS.Utils;
using UnityEngine;

namespace TSS.Tweening
{
    [System.Serializable]
    [ScriptableTweenPath("RectTransform/AnchorPos", 80)]
    public class ScriptableTweenRectTranform_DOAnchorPos : ScriptableTweenDurableItemBase<RectTransform>
    {
        [Box("RectTransform")]
        [Order(10)]
        [SerializeField] private bool _useStartValue;
        [Box("RectTransform")]
        [Order(11)]
        [ShowIf(nameof(_useStartValue))]
        [SerializeReference] private IVector2ValueProvider _startValue = new Vector2ValueProvider();
        [Box("RectTransform")]
        [Order(12)]
        [SerializeReference] private IVector2ValueProvider _endValue = new Vector2ValueProvider();

        protected override Tween CreateTween(RectTransform obj, float duration)
        {
            var tween = obj.DOAnchorPos(_endValue.Get(), duration);
            if (_useStartValue)
                tween.From(_startValue.Get());

            return tween;
        }
    }
    [System.Serializable]
    [ScriptableTweenPath("RectTransform/AnchorMin", 81)]
    public class ScriptableTweenRectTranform_DOAnchorMin : ScriptableTweenDurableItemBase<RectTransform>
    {
        [Box("RectTransform")]
        [Order(10)]
        [SerializeField] private bool _useStartValue;
        [Box("RectTransform")]
        [Order(11)]
        [ShowIf(nameof(_useStartValue))]
        [SerializeReference] private IVector2ValueProvider _startValue = new Vector2ValueProvider();
        [Box("RectTransform")]
        [Order(11)]
        [SerializeReference] private IVector2ValueProvider _endValue = new Vector2ValueProvider();

        protected override Tween CreateTween(RectTransform obj, float duration)
        {
            var tween = obj.DOAnchorMin(_endValue.Get(), duration);
            if (_useStartValue)
                tween.From(_startValue.Get());

            return tween;
        }
    }
    
    [System.Serializable]
    [ScriptableTweenPath("RectTransform/AnchorMax", 82)]
    public class ScriptableTweenRectTranform_DOAnchorMax : ScriptableTweenDurableItemBase<RectTransform>
    {
        [Box("RectTransform")]
        [Order(10)]
        [SerializeField] private bool _useStartValue;
        [Box("RectTransform")]
        [Order(11)]
        [ShowIf(nameof(_useStartValue))]
        [SerializeReference] private IVector2ValueProvider _startValue = new Vector2ValueProvider();
        [Box("RectTransform")]
        [Order(12)]
        [SerializeReference] private IVector2ValueProvider _endValue = new Vector2ValueProvider();

        protected override Tween CreateTween(RectTransform obj, float duration)
        {
            var tween = obj.DOAnchorMax(_endValue.Get(), duration);
            if (_useStartValue)
                tween.From(_startValue.Get());

            return tween;
        }
    }
    
    [System.Serializable]
    [ScriptableTweenPath("RectTransform/Pivot", 83)]
    public class ScriptableTweenRectTranform_DOPivot : ScriptableTweenDurableItemBase<RectTransform>
    {
        [Box("RectTransform")]
        [Order(10)]
        [SerializeField] private bool _useStartValue;
        [Box("RectTransform")]
        [Order(11)]
        [ShowIf(nameof(_useStartValue))]
        [SerializeReference] private IVector2ValueProvider _startValue = new Vector2ValueProvider();
        [Box("RectTransform")]
        [Order(12)]
        [SerializeReference] private IVector2ValueProvider _endValue = new Vector2ValueProvider();

        protected override Tween CreateTween(RectTransform obj, float duration)
        {
            var tween = obj.DOPivot(_endValue.Get(), duration);
            if (_useStartValue)
                tween.From(_startValue.Get());

            return tween;
        }
    }
    
    [System.Serializable]
    [ScriptableTweenPath("RectTransform/SizeDelta", 84)]
    public class ScriptableTweenRectTranform_DOSizeDelta : ScriptableTweenDurableItemBase<RectTransform>
    {
        [Box("RectTransform")]
        [Order(10)]
        [SerializeField] private bool _useStartValue;
        [Box("RectTransform")]
        [Order(11)]
        [ShowIf(nameof(_useStartValue))]
        [SerializeReference] private IVector2ValueProvider _startValue = new Vector2ValueProvider();
        [Box("RectTransform")]
        [Order(12)]
        [SerializeReference] private IVector2ValueProvider _endValue = new Vector2ValueProvider();

        protected override Tween CreateTween(RectTransform obj, float duration)
        {
            var tween = obj.DOSizeDelta(_endValue.Get(), duration);
            if (_useStartValue)
                tween.From(_startValue.Get());

            return tween;
        }
    }
}