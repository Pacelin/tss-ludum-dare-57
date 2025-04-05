using UnityEngine;

namespace LudumDare57.Game
{
    public abstract class ActivationBounds : MonoBehaviour
    {
        [SerializeField] private float _activationY = -15;
        [SerializeField] private float _deactivationY = 15;

        private bool _activated;
        
        private void Update()
        {
            var y = transform.position.y;
            if (!_activated)
            {
                if (y > _activationY)
                {
                    OnActivate();
                    _activated = true;
                }
            }
            else
            {
                if (y < _activationY || y > _deactivationY)
                {
                    OnDeactivate();
                    _activated = false;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            var pos = transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(pos.x - 4, pos.y - _activationY), new Vector3(pos.x + 4, pos.y - _activationY));
            Gizmos.DrawLine(new Vector3(pos.x - 4, pos.y - _deactivationY), new Vector3(pos.x + 4, pos.y - _deactivationY));
            Gizmos.color = Color.white;
            OnGizmosSelected();
        }
        
        protected virtual void OnGizmosSelected() {}

        protected abstract void OnActivate();
        protected abstract void OnDeactivate();
    }
}