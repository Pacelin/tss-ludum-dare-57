using JetBrains.Annotations;
using UnityEngine;

namespace LudumDare57.UI
{
    [PublicAPI]
    public class OverlayInputLock : MonoBehaviour
    {
        private static GameObject _instance;
        private static int _enablers = 0;
        
        private void Awake()
        {
            var go = gameObject;
            DontDestroyOnLoad(go);
            go.SetActive(false);
            _instance = go;
        }

        public static bool Enabled => _instance.activeSelf;
        
        public static void Enable() => _instance.SetActive(++_enablers > 0);
        public static void Disable() => _instance.SetActive(--_enablers > 0);
    }
}