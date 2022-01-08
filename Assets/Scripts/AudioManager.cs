using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public string Bg= "BgMusic";
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
        AudioSource a;
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = mixers[(int)s.audioType];
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatial;
            s.source.loop = s.loop;
            if(!SoundLibrary.ContainsKey(s.name))
                SoundLibrary.Add(s.name,s);
        }
    }

    public void Update()
    {
        foreach (Sound s in sounds)
        {
            if (s.source != null)
                continue;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = mixers[(int)s.audioType];
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatial;
            s.source.loop = s.loop;
            if (!SoundLibrary.ContainsKey(s.name))
                SoundLibrary.Add(s.name, s);
        }
    }

    public void Play3DSound(GameObject gb,string  name)
    {
        Sound sound;
        if (SoundLibrary.ContainsKey(name))
        {
            sound = SoundLibrary[name];
        }
        else
            return;
        GameObject audio = new GameObject(sound.name);
        audio.transform.parent = gb.transform;
        audio.transform.localPosition = Vector3.zero;

        AudioSource s_n = audio.AddComponent<AudioSource>();
        s_n.outputAudioMixerGroup = mixers[(int)sound.audioType];
        s_n.clip = sound.clip;

        s_n.volume = sound.volume;
        s_n.pitch = sound.pitch;
        s_n.spatialBlend = sound.spatial;
        s_n.maxDistance = 25f;
        s_n.loop = sound.loop;
        s_n.Play();
    }
    public void PlayBgMusic(string newbg)
    {
        Stop(Bg);
        Bg = newbg;
        Play(Bg);
    }
    public void Play(string name,float delay = 0)
    {
        if (delay != 0)
        {
            if (SoundLibrary.ContainsKey(name))
            {
                SoundLibrary[name].source.Play(); 
                                                  
            }
        }
        else
        {
            if (SoundLibrary.ContainsKey(name))
            {
                SoundLibrary[name].source.PlayDelayed(delay);

            }
        }
    }

    public float GetTime(string name)
    {
        if (SoundLibrary.ContainsKey(name))
        {
         return SoundLibrary[name].source.time;

        }
        return 0;
    }
    public bool IsPlaying(string name)
    {
        if (SoundLibrary.ContainsKey(name))
        {
            return SoundLibrary[name].source.isPlaying;//PlayOneShot è meglio per i suoni corti, ma per quelli un po' più lunghi è meglio usare Play e basta
            //s.source.PlayOneShot(s.clip); //PlayOneShot è meglio per i suoni corti, ma per quelli un po' più lunghi è meglio usare Play e basta
        }
        return false;
    }
    public void Stop(string name)
    {
        if (SoundLibrary.ContainsKey(name))
        {
            SoundLibrary[name].source.Stop();
        }
    }
    public void Pause(string name)
    {
        if (SoundLibrary.ContainsKey(name))
        {
            SoundLibrary[name].source.Pause();
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
    
    [Range(.1f, 1f)]
    public float spatial;

    public bool loop;

   // [HideInInspector]
    public AudioSource source;

}

public enum AudioType
{
    Master=0,Music=1,Fx=2
}