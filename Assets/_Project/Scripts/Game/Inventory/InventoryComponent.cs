using System.Linq;
using LudumDare57.Game;
using LudumDare57.UI;
using TSS.Audio;
using TSS.ContentManagement;
using UnityEngine;

namespace LudumDare57.Inventory
{
    public class InventoryComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] _itemsContainers;

        private ItemEntry[] _entries;

        private void Awake() => _entries = new ItemEntry[_itemsContainers.Length];

        public void ShowSoldButtons()
        {
            for (int i = 0; i < _entries.Length; i++)
                if (_entries[i].Component)
                    _entries[i].Component.ShowSoldButton();
        }

        public void HideSoldButtons()
        {
            for (int i = 0; i < _entries.Length; i++)
                if (_entries[i].Component)
                    _entries[i].Component.HideSoldButton();
        }
        
        public bool TryAddItem(string id, int cost)
        {
            var emptyIndex = -1;
            for (int i = 0; i < _entries.Length; i++)
            {
                if (string.IsNullOrEmpty(_entries[i].Id) && emptyIndex == -1)
                    emptyIndex = i;
                if (_entries[i].Id != id) 
                    continue;
                _entries[i].Count++;
                _entries[i].Cost += cost;
                _entries[i].Component.Tooltip.SetCost(_entries[i].Cost);
                _entries[i].Component.SetCount(_entries[i].Count);
                return true;
            }

            if (emptyIndex == -1)
                return false;

            var newComponent = Instantiate(CMS.InventoryItemPrefab, _itemsContainers[emptyIndex]);
            _entries[emptyIndex] = new ItemEntry()
            {
                Count = 1,
                Cost = cost,
                Component = newComponent,
                Id = id
            };
            newComponent.Tooltip.SetCost(cost);
            newComponent.Tooltip.SetName(CMS.InventoryItems[id].Name);
            newComponent.SetCount(1);
            newComponent.SetIcon(CMS.InventoryItems[id].Icon);
            return true;
        }

        public bool HasSpaceFor(string item)
        {
            for (int i = 0; i < _entries.Length; i++)
            {
                if (string.IsNullOrEmpty(_entries[i].Id))
                    return true;
                if (_entries[i].Id == item)
                    return true;
            }

            return false;
        }
        
        public (string id, int count)[] GetAvailableItems() => _entries.Where(e => !string.IsNullOrEmpty(e.Id))
            .Select(e => (e.Id, e.Count)).ToArray();

        public void SellItem(ItemComponent component)
        {
            AudioSystem.ItemSell.PlayOneShot();
            for (int i = 0; i < _entries.Length; i++)
            {
                if (_entries[i].Component != component)
                    continue;
                GameContext.Coins.Value += _entries[i].Cost;
                RemoveItem(_entries[i].Id, 1f);
            }
        }
        
        public void RemoveItem(string id, int count)
        {
            for (int i = 0; i < _entries.Length; i++)
            {
                if (_entries[i].Id == id)
                {
                    _entries[i].Count -= count;
                    if (_entries[i].Count <= 0)
                    {
                        Destroy(_entries[i].Component.gameObject);
                        _entries[i].Id = null;
                        _entries[i].Component = null;
                    }
                    else
                    {
                        var cost = Mathf.CeilToInt(_entries[i].Cost * (1f * count / _entries.Length));
                        _entries[i].Cost = cost;
                        _entries[i].Component.Tooltip.SetCost(cost);
                        _entries[i].Component.SetCount(_entries[i].Count);
                    }
                    return;
                }
            }
        }
        
        public void RemoveItem(string id, float amount)
        {
            for (int i = 0; i < _entries.Length; i++)
            {
                if (_entries[i].Id == id)
                {
                    var count = Mathf.CeilToInt(_entries[i].Count * amount);
                    _entries[i].Count -= count;
                    if (_entries[i].Count <= 0)
                    {
                        Destroy(_entries[i].Component.gameObject);
                        _entries[i].Id = null;
                        _entries[i].Component = null;
                    }
                    else
                    {
                        var cost = Mathf.CeilToInt(_entries[i].Cost * amount);
                        _entries[i].Cost = cost;
                        _entries[i].Component.Tooltip.SetCost(cost);
                        _entries[i].Component.SetCount(_entries[i].Count);
                    }
                    return;
                }
            }
        }
    }
}