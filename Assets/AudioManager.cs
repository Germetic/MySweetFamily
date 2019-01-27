using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioTake[] AllAudios;
    private AudioSource _audioSource;
    public static AudioManager Instance = null;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }
    public void Play(string audioTakeName)
    {   
        for (int i = 0; i < AllAudios.Length; i++)
        {
            if(AllAudios[i].Name == audioTakeName)
            {
                _audioSource.clip = AllAudios[i].GetRandomClip();
                _audioSource.Play();
                break;
            }

        }
    }
}
