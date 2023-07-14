using System.Collections.Generic;
using UnityEngine;

public enum AudioName
{
    fire,
    enemyDie,
}
public enum AudioRangeName
{
    playerRun,
    enemyDamage,

}

public class SoundsService : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SoundsArchive soundsArchive;

    Dictionary<AudioName, AudioClip> clipsArchive = new Dictionary<AudioName, AudioClip>();
    Dictionary<AudioRangeName, AudioClip[]> clipsRangeArchive = new Dictionary<AudioRangeName, AudioClip[]>();

    static SoundsService i;
    private void Awake()
    {
        if (!i) i = this; 
        else
        {
            Destroy(this);
            return;
        }

        foreach (var i in soundsArchive.clips)
        {
            clipsArchive.Add(i.name, i.clip);
        }
        foreach (var i in soundsArchive.clipsRange)
        {
            clipsRangeArchive.Add(i.name, i.clips);
        }
    }
    public static void Play(AudioName data, float volume = 1)
    {
        if (i.clipsArchive[data])
        {
            i.audioSource.PlayOneShot(i.clipsArchive[data], volume);
        }
        else
        {
            Debug.LogWarning("Вызывается недоступный клип!");
        }
    }
    public static void PlayRange(AudioRangeName data, float volume = 1)
    {
        if (i.clipsRangeArchive[data].Length > 0)
        {
            int r = Random.Range(0, i.clipsRangeArchive[data].Length);
            i.audioSource.PlayOneShot(i.clipsRangeArchive[data][r], volume);
        }
        else
        {
            Debug.LogWarning("Вызывается недоступный клип!");
        }
    }
}


[System.Serializable]
public struct AudioClipData
{
    public AudioName name;
    public AudioClip clip;
}
[System.Serializable]
public struct AudioClipsRangeData
{
    public AudioRangeName name;
    public AudioClip[] clips;
}



