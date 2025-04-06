using TMPro;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.Localization;

namespace LudumDare57.Game.Shop
{
    public class ShopUpgradeTooltip : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private LocalizedString _nameStr;
        [SerializeField] private TMP_Text _desctiptionText;
        [SerializeField] private LocalizedString _descriptionStr;
        [Space] 
        [SerializeField] private ScriptableTween _openTween;
        [SerializeField] private ScriptableTween _closeTween;

        public void UpdateTooltip(string itemName, string desctription)
        {
            _nameText.text = string.Format(_nameStr.GetLocalizedString(), itemName);
            _desctiptionText.text = string.Format(_descriptionStr.GetLocalizedString(), desctription);
        }
        
        public void Show()
        {
            if (_closeTween.IsPlaying)
                _closeTween.Pause();
            _openTween.Play();
        }

        public void Hide()
        {
            if (!gameObject.activeSelf)
                return;
            if (_openTween.IsPlaying)
                _openTween.Pause();
            _closeTween.Play();
        }
    }
}