using UnityEngine;

public class GA_FREE_DemoPlaySound : MonoBehaviour
{
	public int m_AudioSourceCount = 2;

	private AudioSource[] m_AudioSource;

	public AudioClip m_Audio_Button1;

	public AudioClip m_Audio_Button2;

	private void Start()
	{
		if (m_AudioSource == null)
		{
			m_AudioSource = new AudioSource[m_AudioSourceCount];
			for (int i = 0; i < m_AudioSource.Length; i++)
			{
				AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
				audioSource.rolloffMode = AudioRolloffMode.Linear;
				m_AudioSource[i] = audioSource;
			}
		}
	}

	private void Update()
	{
	}

	private void PlayOneShot(AudioClip pAudioClip)
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

	public void PlaySoundButton1()
	{
		PlayOneShot(m_Audio_Button1);
	}

	public void PlaySoundButton2()
	{
		PlayOneShot(m_Audio_Button2);
	}
}
