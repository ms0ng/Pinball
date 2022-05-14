using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFrame;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField]
    List<MyAudioClip> mAudioClips;

    Dictionary<string, AudioSource> _AudioSources;

    [System.Serializable]
    class MyAudioClip
    {
        public string Name;
        public AudioClip AudioClip;
    }

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _AudioSources = new Dictionary<string, AudioSource>();
    }

    public void Play(string name, float delay = 0, bool loop = false)
    {
        AudioClip audioClip = null;
        foreach (var item in mAudioClips)
        {
            if (item.Name.Equals(name)) audioClip = item.AudioClip;
        }

        if (audioClip == null)
        {
            Debug.LogWarning($"找不到音乐:{name}，请检查是否添加");
            return;
        }

        AudioSource source;
        if (!_AudioSources.TryGetValue(name, out source))
        {
            source = gameObject.AddComponent<AudioSource>();
            source.clip = audioClip;
            source.volume = 0.15f;
            _AudioSources.Add(name, source);
        }
        source.loop = loop;
        source.PlayDelayed(delay);

    }

    public void Stop(string name)
    {
        if (_AudioSources.TryGetValue(name, out AudioSource source))
        {
            source.Stop();
        }
    }
}
