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
		public static SoundEvent_MainMenuOST MainMenuOST { get; } = new();
		public static SoundEvent_UIStateChanged UIStateChanged { get; } = new();
		public static SoundEvent_OreImpact OreImpact { get; } = new();
		public static SoundEvent_RopeMove RopeMove { get; } = new();
		public static SoundEvent_MinecartImpact MinecartImpact { get; } = new();
		public static SoundEvent_ChestImpact ChestImpact { get; } = new();
		public static SoundEvent_CrystalImpact CrystalImpact { get; } = new();
		public static SoundEvent_SeashellImpact SeashellImpact { get; } = new();
		public static SoundEvent_TreeImpact TreeImpact { get; } = new();
		public static SoundEvent_ItemBuy ItemBuy { get; } = new();
		public static SoundEvent_ItemSell ItemSell { get; } = new();
		public static SoundEvent_ShopMusicOutside ShopMusicOutside { get; } = new();
		public static SoundEvent_ShopMusicIn ShopMusicIn { get; } = new();
		public static SoundEvent_Pickup Pickup { get; } = new();
		public static SoundEvent_EventStart EventStart { get; } = new();
		public static SoundEvent_ChestDest ChestDest { get; } = new();
		public static SoundEvent_OreDest OreDest { get; } = new();
		public static SoundEvent_CrystalDest CrystalDest { get; } = new();
		public static SoundEvent_MinecartDest MinecartDest { get; } = new();
		public static SoundEvent_SeashellDest SeashellDest { get; } = new();
		public static SoundEvent_TreeDest TreeDest { get; } = new();
		public static SoundEvent_Ambient Ambient { get; } = new();
    
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

	public class SoundEvent_MainMenuOST : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 156865;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1577484616, Data2 = 1116902958, Data3 = -170917233, Data4 = -1447747404 };

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

	public class SoundEvent_OreImpact : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1658257145, Data2 = 1323841259, Data3 = 1304008345, Data4 = -1641693361 };

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

	public class SoundEvent_RopeMove : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 949990675, Data2 = 1134441188, Data3 = 586303889, Data4 = -681587699 };

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

	public class SoundEvent_MinecartImpact : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 643714837, Data2 = 1232708717, Data3 = -540856142, Data4 = 490874778 };

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

	public class SoundEvent_ChestImpact : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1109024351, Data2 = 1135181994, Data3 = 852854419, Data4 = 577194044 };

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

	public class SoundEvent_CrystalImpact : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 380309167, Data2 = 1200027272, Data3 = -1540122489, Data4 = -1246061969 };

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

	public class SoundEvent_SeashellImpact : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 880041399, Data2 = 1318255629, Data3 = 833368448, Data4 = 785426000 };

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

	public class SoundEvent_TreeImpact : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1603147207, Data2 = 1163847768, Data3 = -1845571698, Data4 = 2082470133 };

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

	public class SoundEvent_ItemBuy : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 946549812, Data2 = 1191918384, Data3 = -13755471, Data4 = -104665173 };

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

	public class SoundEvent_ItemSell : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -834849947, Data2 = 1193172006, Data3 = 603743660, Data4 = -1742568347 };

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

	public class SoundEvent_ShopMusicOutside : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 144117;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1011058284, Data2 = 1302288042, Data3 = -1782199132, Data4 = -531724281 };

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

	public class SoundEvent_ShopMusicIn : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 144117;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 50964380, Data2 = 1158872879, Data3 = 2094541200, Data4 = -255182967 };

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

	public class SoundEvent_Pickup : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 852837047, Data2 = 1227124840, Data3 = -1878547019, Data4 = 1140207272 };

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

	public class SoundEvent_EventStart : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -147601977, Data2 = 1227875667, Data3 = -1861775685, Data4 = -1545227128 };

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

	public class SoundEvent_ChestDest : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -391278287, Data2 = 1278130092, Data3 = -1133765247, Data4 = -895861294 };

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

	public class SoundEvent_OreDest : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 524757650, Data2 = 1204382717, Data3 = 1213488038, Data4 = -868725365 };

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

	public class SoundEvent_CrystalDest : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 2106156729, Data2 = 1191060462, Data3 = -1608245102, Data4 = -1244471058 };

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

	public class SoundEvent_MinecartDest : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 881892032, Data2 = 1337948779, Data3 = -248219000, Data4 = -1623316334 };

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

	public class SoundEvent_SeashellDest : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1542804518, Data2 = 1079777795, Data3 = 2056507012, Data4 = -1664985262 };

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

	public class SoundEvent_TreeDest : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1283246317, Data2 = 1253177355, Data3 = -1337630323, Data4 = -776634669 };

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

	public class SoundEvent_Ambient : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 89191;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 33554630, Data2 = 1229588207, Data3 = -1015728718, Data4 = -795709280 };

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