using UnityEngine;

namespace TSS.Utils
{
    [System.Serializable] 
    public class Vector3OneScaledValueProvider : IVector3ValueProvider
    {
        [SerializeField] private float _value;

        public Vector3OneScaledValueProvider(float value) =>
            _value = value;
        
        public Vector3 Get() => Vector3.one * _value;
    }
}