using UnityEngine;

namespace LudumDare57.Hole
{
    public class PlayerView : MonoBehaviour
    {
        public Transform BackpackPoint => _backpackPoint;
        
        [SerializeField] private Transform _backpackPoint;
    }
}