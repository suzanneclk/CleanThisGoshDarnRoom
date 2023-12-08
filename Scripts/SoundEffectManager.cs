using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(AudioClip sfx){
        sfxSource.clip = sfx;
        sfxSource.Play();
    }
    public void StopMusic(){
        musicSource.Stop();
    }

    public void PlayMusic(AudioClip music){
        musicSource.clip = music;
        musicSource.Play();
    }
}
