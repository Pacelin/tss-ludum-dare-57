using UnityEngine;

namespace LudumDare57.Game
{
    public class PlayerComponent : MonoBehaviour
    {
        public Transform BackpackPoint => _backpackPoint;
        
        [SerializeField] private Transform _backpackPoint;
    }
}