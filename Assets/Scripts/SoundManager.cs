using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SoundName
{
    PlayerDie, PlayerChange, PlayerShot, EnemyHit, EnemyDie, WallHit
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private AudioSource audioSource;

    public List<SoundDB> soundDB = new List<SoundDB>();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundName s)
    {
        int i = (int)s;
        audioSource.PlayOneShot(soundDB[i].audio, soundDB[i].volume);
    }
}


[System.Serializable]
public class SoundDB
{
    public SoundName name;
    public AudioClip audio;
    public float volume;
}
