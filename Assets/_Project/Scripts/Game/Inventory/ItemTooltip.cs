using Cysharp.Threading.Tasks;
using TMPro;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.Localization;

namespace LudumDare57.Inventory
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private LocalizedString _nameStr;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private LocalizedString _costStr;
        [SerializeField] private ScriptableTween _openTween;
        [SerializeField] private ScriptableTween _closeTween;

        private int _cost;
        private LocalizedString _name;
        
        private void OnEnable()
        {
            _nameStr.StringChanged += UpdateNameStr;
            UpdateNameStr(_nameStr.GetLocalizedString());
            _costStr.StringChanged += UpdateCostStr;
            UpdateCostStr(_costStr.GetLocalizedString());
        }

        private void OnDisable()
        {
            _nameStr.StringChanged -= UpdateNameStr;
            _costStr.StringChanged -= UpdateCostStr;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            if (_closeTween.IsPlaying)
                _closeTween.Pause();
            _openTween.Play();
        }

        public void Hide()
        {
            if (_openTween.IsPlaying)
                _openTween.Pause();
            _closeTween.Play();
            _closeTween.WaitWhilePlay().ContinueWith(() => gameObject.SetActive(false)).Forget();
        }
        
        public void SetCost(int cost)
        {
            _cost = cost;
            UpdateCostStr(_costStr.GetLocalizedString());
        }

        public void SetName(LocalizedString newName)
        {
            _name = newName;
            UpdateNameStr(_nameStr.GetLocalizedString());
        }
        
        private void UpdateNameStr(string str) => _nameText.text = string.Format(str, _name.GetLocalizedString());
        private void UpdateCostStr(string str) => _costText.text = string.Format(str, _cost);
    }
}