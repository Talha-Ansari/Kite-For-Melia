
using UnityEngine;
using System;

public class AudioPlayer : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioPlayer instance;
    public float bgVolume;
    public bool muteMusic;

    public AudioSource bgSound;
    void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
        // Play("BG");

    }

    public void Play(string name)
    {
        if (muteMusic) return;
        Sound s = Array.Find(sounds, sound => sound.SoundName == name);
        s.source.Play();
        Debug.Log(name);
        if (name == "BG")
        {
            s.source.loop = true;
            s.source.volume = bgVolume;
        }


    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == name);
        s.source.Stop();

    }
}

