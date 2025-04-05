using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LudumDare57.Hole
{
    public abstract class HoleEntryConfig : ScriptableObject
    {
        public float Depth => _depth;
        [SerializeField] private float _depth;

        public abstract UniTask StartEntry(CancellationToken cancellationToken);
        public abstract void Spawn();
    }
}