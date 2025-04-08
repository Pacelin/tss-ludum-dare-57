using System.Threading;
using Cysharp.Threading.Tasks;
using mixpanel;
using TSS.Core;

namespace Jammer
{
    [RuntimeOrder(ERuntimeOrder.SubsystemRegistration)]
    public class ReplaceWithLocalStorage : IPreferences, IRuntimeLoader
    {
        public void DeleteKey(string key) => UserDataManager.DeleteKey(key);

        public int GetInt(string key) => int.Parse(UserDataManager.GetString(key));
        public int GetInt(string key, int defaultValue) => UserDataManager.GetInt(key, defaultValue);

        public string GetString(string key) => UserDataManager.GetString(key);
        public string GetString(string key, string defaultValue) => UserDataManager.GetString(key, defaultValue);

        public bool HasKey(string key) => UserDataManager.HasKey(key);

        public void SetInt(string key, int value) => UserDataManager.SetString(key, value.ToString());
        public void SetString(string key, string value) => UserDataManager.SetString(key, value);
    
        public UniTask Initialize(CancellationToken cancellationToken)
        {
            Mixpanel.SetPreferencesSource(this);
            return UniTask.CompletedTask;
        }

        public void Dispose() { }
    }
}