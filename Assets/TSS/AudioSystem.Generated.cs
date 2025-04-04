// Auto-generated code. Reference: "Packages/com.tss.cms/Editor/CMSGenerator.cs"

// ReSharper disable RedundantUsingDirective
#pragma warning disable CS1998

using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using FMODUnity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R3;
using TSS.Core;

namespace TSS.Audio
{
    [UsedImplicitly]
    [RuntimeOrder(ERuntimeOrder.SystemRegistration)]
    public class AudioSystem : IRuntimeLoader
    {
	    public static class Volumes
	    {
		    public static float MasterVolume
		    {
			    get
			    {
				    _masterBus.getVolume(out var volume);
				    return volume;
			    }
			    set
			    {
				    _masterBus.setVolume(value);
				    PlayerPrefs.SetFloat("master_volume", value);
			    }
		    }

		    public static float GetVolume(int index)
		    {
			    _buses[index].getVolume(out var volume);
			    return volume;
		    }

		    public static void SetVolume(int index, float volume)
		    {
			    _buses[index].setVolume(volume);
			    _buses[index].getID(out var id);
			    PlayerPrefs.SetFloat("volume_of_" + id, volume);
		    }
	    }
	    
		public static class Global
		{
			private static readonly FMOD.Studio.PARAMETER_ID MusicPitchId = new FMOD.Studio.PARAMETER_ID() { data1 = 2770718570, data2 = 3255492069 };

			public static void SetMusicPitch(float value) => RuntimeManager.StudioSystem.setParameterByID(MusicPitchId, value);
			public static float GetMusicPitch()
			{
				RuntimeManager.StudioSystem.getParameterByID(MusicPitchId, out var value);
				return value;
			}

		}

		public static SoundEvent_ButtonClick ButtonClick { get; } = new();
		public static SoundEvent_PlayButtonClick PlayButtonClick { get; } = new();
		public static SoundEvent_UIStateChanged UIStateChanged { get; } = new();
    
		private System.IDisposable _focusDisposable;
		
		private static FMOD.Studio.Bus _masterBus;
		private static FMOD.Studio.Bus[] _buses;
		
        public async UniTask Initialize(CancellationToken cancellationToken)
        {
			RuntimeManager.LoadBank("Master.strings", true);
			RuntimeManager.LoadBank("Master", true);

            await UniTask.WaitUntil(() => FMODUnity.RuntimeManager.HaveAllBanksLoaded);
            await UniTask.WaitWhile(FMODUnity.RuntimeManager.AnySampleDataLoading);
            
            var volumesSettings = await Addressables.LoadAssetAsync<AudioVolumesSettings>("Audio Volumes")
	            .ToUniTask(cancellationToken: cancellationToken);

            _masterBus = FMODUnity.RuntimeManager.GetBus(volumesSettings.MasterBusPath);
            _masterBus.setVolume(PlayerPrefs.GetFloat("master_volume", volumesSettings.DefaultMasterVolume));

            _buses = new FMOD.Studio.Bus[volumesSettings.BusesPaths.Length];
            for (int i = 0; i < _buses.Length; i++)
            {
	            _buses[i] = FMODUnity.RuntimeManager.GetBus(volumesSettings.BusesPaths[i]);
	            _buses[i].getID(out var busId);
	            _buses[i].setVolume(PlayerPrefs.GetFloat("volume_of_" + busId, volumesSettings.DefaultVolume));
            }
            
            Addressables.Release(volumesSettings);
            
            _focusDisposable = Runtime.ObserveFocus().Subscribe(focus =>
            {
	            if (RuntimeManager.StudioSystem.isValid())
	            {
		            RuntimeManager.PauseAllEvents(!focus);

		            if (!focus)
			            RuntimeManager.CoreSystem.mixerSuspend();
		            else
			            RuntimeManager.CoreSystem.mixerResume();
	            }
            });
        }

        public void Dispose() => _focusDisposable.Dispose();
    }

	public class SoundEvent_ButtonClick : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 208;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 2080162309, Data2 = 1254913987, Data3 = -280735595, Data4 = -1170589711 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_PlayButtonClick : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 1175;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1817814564, Data2 = 1255403468, Data3 = -203230578, Data4 = 2052636638 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_UIStateChanged : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 156;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -790106898, Data2 = 1279714956, Data3 = -822204765, Data4 = -1405864594 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

}