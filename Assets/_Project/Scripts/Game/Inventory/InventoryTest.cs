using LudumDare57.Game;
using UnityEngine;

namespace LudumDare57.Inventory
{
    public class InventoryTest : MonoBehaviour
    {
        [InventoryItem] 
        [SerializeField] private string _item;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                GameContext.Inventory.TryAddItem(_item, 10);
            if (Input.GetKeyDown(KeyCode.R))
                GameContext.Inventory.RemoveItem(_item, 1);
        }
    }
}