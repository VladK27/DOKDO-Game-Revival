using System;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_DemoPlaySound : MonoBehaviour
{
	public int m_AudioSourceCount;

	protected AudioSource[] m_AudioSource;

	public AudioClip m_Audio_Button1;

	public AudioClip m_Audio_Button2;

	public GA_FREE_JS_DemoPlaySound()
	{
		m_AudioSourceCount = 2;
	}

	public virtual void Start()
	{
		if (m_AudioSource == null)
		{
			m_AudioSource = new AudioSource[m_AudioSourceCount];
			for (int i = 0; i < m_AudioSource.Length; i++)
			{
				AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
				audioSource.rolloffMode = AudioRolloffMode.Linear;
				m_AudioSource[i] = audioSource;
			}
		}
	}

	public virtual void Update()
	{
	}

	public virtual void PlayOneShot(AudioClip pAudioClip)
	{
		for (int i = 0; i < m_AudioSource.Length; i++)
		{
			if (!m_AudioSource[i].isPlaying)
			{
				m_AudioSource[i].PlayOneShot(pAudioClip);
				break;
			}
		}
	}

	public virtual void PlaySoundButton1()
	{
		PlayOneShot(m_Audio_Button1);
	}

	public virtual void PlaySoundButton2()
	{
		PlayOneShot(m_Audio_Button2);
	}
}
