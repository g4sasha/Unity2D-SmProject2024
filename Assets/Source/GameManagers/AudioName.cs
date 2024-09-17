using UnityEngine;

[System.Serializable]
public class AudioName
{
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public AudioClip Audio { get; private set; }
}