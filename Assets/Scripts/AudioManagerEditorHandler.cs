using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AudioManagerEditorHandler : MonoBehaviour
{
    AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager == null)
            manager = GetComponent<AudioManager>();
        else
        {
            foreach (Sound s in manager.sounds)
            {
                if (s.source != null)
                {
                    continue;
                }
                else
                {
                    GameObject newsound = new GameObject(s.name);
                    newsound.transform.parent = transform;
                    s.source = newsound.AddComponent<AudioSource>();
                    s.source.outputAudioMixerGroup = manager.mixers[(int)s.audioType];
                    s.source.clip = s.clip;
                    s.source.playOnAwake = false;
                    s.source.volume = s.volume;
                    s.source.pitch = s.pitch;
                    s.source.spatialBlend = s.spatial;
                    s.source.loop = s.loop;
                    if (!manager.SoundLibrary.ContainsKey(s.name))
                        manager.SoundLibrary.Add(s.name, s);
                }
            }
        }
    }

    private void OnDestroy()
    {
        foreach (Sound s in manager.sounds)
        {
            if (s.source != null)
                DestroyImmediate(s.source.gameObject);
        }
    }
}
