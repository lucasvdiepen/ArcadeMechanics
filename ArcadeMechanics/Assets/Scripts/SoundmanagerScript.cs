using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundmanagerScript : MonoBehaviour
{
    public AudioClip[] JumpSounds;
    public AudioClip[] DeathSounds;
    public AudioClip[] BGM;

    public GameObject GetVolume;
    public float BGMVolume;
    public float SFXVolume;

    private AudioSource sfxSource;
    private AudioSource bgmSource;

    private static SoundmanagerScript soundManager;



    // Start is called before the first frame update
    void Start()
    {
    
        StartCoroutine(StartBGMMusic());
        StartBGMMusic();
    }
    public void ChangeSFXVolume(float value)
    {
        SFXVolume = value * 0.01f;
    }
    public void ChangeBGMVolume(float value)
    {
        BGMVolume = value * 0.01f;
    }
    public void PlayJumpSounds()
    {
        sfxSource.clip = JumpSounds [Random.Range(0, JumpSounds.Length)];
        sfxSource.volume = SFXVolume;
        sfxSource.PlayOneShot(sfxSource.clip);
    }
    public void PlayDeathSounds()
    {
        sfxSource.clip = DeathSounds[Random.Range(0, DeathSounds.Length)];
        sfxSource.volume = SFXVolume;
        sfxSource.PlayOneShot(sfxSource.clip);
    }
    public IEnumerator StartBGMMusic()
    {
        while (true)
        {
            AudioClip audioClip = BGM[Random.Range(0, BGM.Length)];
            bgmSource.clip = audioClip;
            bgmSource.volume = BGMVolume;
            bgmSource.PlayOneShot(bgmSource.clip);
            yield return new WaitForSeconds(audioClip.length);
        }
    }
    private void Awake()
    {
        if (!soundManager)
        {
            DontDestroyOnLoad(gameObject);
            soundManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
