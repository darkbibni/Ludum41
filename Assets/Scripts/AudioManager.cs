using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    [Header("All sounds of the game")]
    public SfxEntry[] allClips;

    public static AudioManager instance;

    private Dictionary<string, List<AudioClip>> sfxDictionnary;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        AudioClipsToDictionnary();
    }

    // Return the specified audio clip from dictionnary.
    public AudioClip GetSound(string sfxName)
    {
        List<AudioClip> sfxs;
        
        if (!sfxDictionnary.TryGetValue(sfxName, out sfxs))
        {
            Debug.LogWarning("Sound " + sfxName + " is missing !");
        }

        AudioClip clip = null;

        if(sfxs.Count > 0)
        {
            clip = sfxs[Random.Range(0, sfxs.Count)];
        }

        return clip;
    }

    #region Utility methods

    // Convert an array of AudioClips to a dictionnary.
    private void AudioClipsToDictionnary()
    {
        sfxDictionnary = new Dictionary<string, List<AudioClip>>();

        foreach (SfxEntry entry in allClips)
        {
            sfxDictionnary.Add(entry.sfxName, entry.sfxs);
        }
    }

    #endregion
}

/// <summary>
/// Struct which store a sfx linked to his name.
/// </summary>
[System.Serializable]
public struct SfxEntry
{
    public string sfxName;
    public List<AudioClip> sfxs;
}

