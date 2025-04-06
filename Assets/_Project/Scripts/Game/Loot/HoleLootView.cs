using Cysharp.Threading.Tasks;
using LudumDare57.Inventory;
using TSS.Tweening;
using TSS.Utils.Randoms.Weighted;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace LudumDare57.Game
{
    public class HoleLootView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ScriptableTween _onClickTween;
        [SerializeField] private ScriptableTween _onDestroyTween;
        [Space]
        [SerializeField] private Vector2Int _costRange;
        [SerializeField] private Vector2Int _kicksCount;
        [SerializeField] private Vector2 _scaleRange;
        [SerializeField] private Vector2 _rotateRange;
        [SerializeField] private int _requirePickaxeLevel = 1;
        [InventoryItem] 
        [SerializeField] private string _dropItem;
        [SerializeField] private float _dropChance = 1;

        private int _cost;
        private int _kicksRemaining;
        
        public void Start()
        {
            _cost = Mathf.CeilToInt(Random.Range(_costRange.x, _costRange.y + 1) * GameContext.IncomeMultiplier);
            var maxCost = Mathf.CeilToInt(_costRange.y * GameContext.IncomeMultiplier);
            var t = 0f;
            if (_cost != maxCost)
                t = 1f * (maxCost - _costRange.x) / (maxCost - _cost);
            _kicksRemaining = (int) Mathf.Lerp(_kicksCount.x, _kicksCount.y, t);
            _kicksRemaining = Mathf.Min(1, _kicksRemaining - GameContext.PickaxeStrength);
            if (GameContext.PickaxeLevel < _requirePickaxeLevel)
                _kicksRemaining += 99;
            transform.localScale = Vector3.one * Mathf.Lerp(_scaleRange.x, _scaleRange.y, t);

            var rotateAngle = Random.Range(_rotateRange.x, _rotateRange.y);
            var flip = transform.position.x > GameContext.Hole.HoleCenterX;
            transform.rotation = Quaternion.Euler(0, flip ? 180 : 0, rotateAngle);
        } 

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_kicksRemaining <= 0)
                return;
            if (--_kicksRemaining <= 0)
            {
                DropItems();
                _onDestroyTween.Play();
                _onDestroyTween.WaitWhilePlay().ContinueWith(() =>
                {
                    Destroy(gameObject);
                });
            }
            else
            {
                _onClickTween.Play();
            }
        }

        private void DropItems()
        {
            if (Random.Range(0f, 1f) <= _dropChance)
            {
                if (GameContext.Inventory.TryAddItem(_dropItem, _cost))
                {
                    
                }
            }
        }
    }
}