using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using SA.IOSNative.Models;
using SA.IOSNative.UserNotifications;
using UnityEngine;

namespace SA.IOSNative.Core
{
	public class AppController : Singleton<AppController>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnApplicationDidEnterBackground__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnApplicationDidBecomeActive__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnApplicationDidReceiveMemoryWarning__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnApplicationWillResignActive__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnApplicationWillTerminate__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<LaunchUrl> OnOpenURL__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<UniversalLink> OnContinueUserActivity__BackingField = delegate
		{
		};

		public static LaunchUrl LaunchUrl
		{
			get
			{
				return new LaunchUrl(string.Empty, string.Empty);
			}
		}

		public static UniversalLink LaunchUniversalLink
		{
			get
			{
				return new UniversalLink(string.Empty);
			}
		}

		public static NotificationRequest launchNotification
		{
			get
			{
				return NotificationCenter.launchNotification;
			}
		}

		public static event Action OnApplicationDidEnterBackground
		{
			add
			{
				Action action = OnApplicationDidEnterBackground__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationDidEnterBackground__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = OnApplicationDidEnterBackground__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationDidEnterBackground__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action OnApplicationDidBecomeActive
		{
			add
			{
				Action action = OnApplicationDidBecomeActive__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationDidBecomeActive__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = OnApplicationDidBecomeActive__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationDidBecomeActive__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action OnApplicationDidReceiveMemoryWarning
		{
			add
			{
				Action action = OnApplicationDidReceiveMemoryWarning__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationDidReceiveMemoryWarning__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = OnApplicationDidReceiveMemoryWarning__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationDidReceiveMemoryWarning__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action OnApplicationWillResignActive
		{
			add
			{
				Action action = OnApplicationWillResignActive__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationWillResignActive__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = OnApplicationWillResignActive__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationWillResignActive__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action OnApplicationWillTerminate
		{
			add
			{
				Action action = OnApplicationWillTerminate__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationWillTerminate__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = OnApplicationWillTerminate__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnApplicationWillTerminate__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action<LaunchUrl> OnOpenURL
		{
			add
			{
				Action<LaunchUrl> action = OnOpenURL__BackingField;
				Action<LaunchUrl> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnOpenURL__BackingField, (Action<LaunchUrl>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<LaunchUrl> action = OnOpenURL__BackingField;
				Action<LaunchUrl> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnOpenURL__BackingField, (Action<LaunchUrl>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action<UniversalLink> OnContinueUserActivity
		{
			add
			{
				Action<UniversalLink> action = OnContinueUserActivity__BackingField;
				Action<UniversalLink> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnContinueUserActivity__BackingField, (Action<UniversalLink>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<UniversalLink> action = OnContinueUserActivity__BackingField;
				Action<UniversalLink> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnContinueUserActivity__BackingField, (Action<UniversalLink>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public static void Subscribe()
		{
			Singleton<AppController>.Instance.enabled = true;
		}

		private void openURL(string data)
		{
			LaunchUrl obj = new LaunchUrl(data);
			OnOpenURL__BackingField(obj);
		}

		private void continueUserActivity(string absoluteUrl)
		{
			UniversalLink obj = new UniversalLink(absoluteUrl);
			OnContinueUserActivity__BackingField(obj);
		}

		private void applicationDidEnterBackground()
		{
			OnApplicationDidEnterBackground__BackingField();
		}

		private void applicationDidBecomeActive()
		{
			OnApplicationDidBecomeActive__BackingField();
		}

		private void applicationDidReceiveMemoryWarning()
		{
			OnApplicationDidReceiveMemoryWarning__BackingField();
		}

		private void applicationWillResignActive()
		{
			OnApplicationWillResignActive__BackingField();
		}

		private void applicationWillTerminate()
		{
			OnApplicationWillTerminate__BackingField();
		}

		protected override void OnApplicationQuit()
		{
			base.OnApplicationQuit();
			OnApplicationWillTerminate__BackingField();
		}
	}
}
