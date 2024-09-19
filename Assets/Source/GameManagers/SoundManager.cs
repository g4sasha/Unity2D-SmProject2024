using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }
	[SerializeField] private List<AudioClip> _sounds;
	private AudioSource _audioSource;

	private void OnValidate()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

    public void PlaySound(string name)
    {
        foreach (var sound in _sounds)
		{
			if (sound.name == name)
			{
				_audioSource.PlayOneShot(sound);
				return;
			}
		}
    }
}