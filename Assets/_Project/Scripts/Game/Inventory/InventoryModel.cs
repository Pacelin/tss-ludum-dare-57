namespace LudumDare57.Inventory
{
    public class InventoryModel
    {
        private readonly int[] _itemIds;
        
        public InventoryModel(int size)
        {
            _itemIds = new int[size];
            for (int i = 0; i < _itemIds.Length; i++)
                _itemIds[i] = -1;
        }

        public void Add()
        {
            
        }
    }
}