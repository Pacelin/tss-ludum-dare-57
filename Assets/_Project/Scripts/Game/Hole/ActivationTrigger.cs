using UnityEngine;

namespace LudumDare57.Game
{
    public abstract class ActivationTrigger : MonoBehaviour
    {
        private bool _activated;
        
        private void Update()
        {
            if (!_activated && transform.position.y >= 0)
            {
                _activated = true;
                OnActivate();
            }
            else if (_activated && transform.position.y < 0)
            {
                _activated = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            var pos = transform.position;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector3(pos.x - 4, pos.y), new Vector3(pos.x + 4, pos.y));
            Gizmos.color = Color.white;
        }
        

        protected abstract void OnActivate();
    }
}