using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class AudioTake 
{   

    public string Name;
    public AudioClip[] AudioVariants;

    public AudioClip GetRandomClip()
    {
        int r = UnityEngine.Random.Range(0, AudioVariants.Length - 1);
        return AudioVariants[r];
    }
}
