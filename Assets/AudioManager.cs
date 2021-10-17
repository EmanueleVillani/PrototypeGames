using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Dictionary<string, Sound> SoundLibrary=new Dictionary<string, Sound>();
    public static AudioManager instance;
    public AudioMixer audioMixer;
    public AudioMixerGroup[] mixers;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = mixers[(int)s.audioType];
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if(!SoundLibrary.ContainsKey(s.name))
                SoundLibrary.Add(s.name,s);
        }
    }

    private void Start()
    {
        Play("BgMusic");
    }
    public void Play(string name)
    {
        if (SoundLibrary.ContainsKey(name))
        {
            SoundLibrary[name].source.Play(); //PlayOneShot è meglio per i suoni corti, ma per quelli un po' più lunghi è meglio usare Play e basta
            //s.source.PlayOneShot(s.clip); //PlayOneShot è meglio per i suoni corti, ma per quelli un po' più lunghi è meglio usare Play e basta
        }
    }

    public void PlayLongSound(string name)
    {
        if (SoundLibrary.ContainsKey(name))
        {
            SoundLibrary[name].source.Play(); //PlayOneShot è meglio per i suoni corti, ma per quelli un po' più lunghi è meglio usare Play e basta
        }
    }

    public void StopSound(string name)
    {
        if (SoundLibrary.ContainsKey(name))
        {
            SoundLibrary[name].source.Stop(); //PlayOneShot è meglio per i suoni corti, ma per quelli un po' più lunghi è meglio usare Play e basta
        }
    }

    public void SetMasterAudio(float value)
    {
        _ = audioMixer.SetFloat("MasterVolume", value);
    }
    public void SetFxAudio(float value)
    {
        _ = audioMixer.SetFloat("FxVolume", value);
    }
    public void SetMusicAudio(float value)
    {
        _ = audioMixer.SetFloat("MusicVolume", value);
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioType audioType;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}

public enum AudioType
{
    Master=0,Music=1,Fx=2
}