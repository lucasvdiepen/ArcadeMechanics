using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundmanagerScript : MonoBehaviour
{
    public AudioClip[] JumpSounds;
    public AudioClip[] DeathSounds;
    public AudioClip[] BGM;
    private AudioSource source;
    public GameObject GetVolume;
    float BGMVolume = 0.5f;
    float SFXVolume = 0.5f;
    


    // Start is called before the first frame update
    void Start()
    {

        source = GetComponent<AudioSource>();
        StartCoroutine(StartBGMMusic());
        StartBGMMusic();

    }
    public void ChangeSFXVolume(float value)
    {
        BGMVolume = value;
    }
    public void ChangeBGMVolume(float value)
    {
        SFXVolume = value;
    }

    public void PlayJumpSounds()
    {
        source.clip = JumpSounds [Random.Range(0, JumpSounds.Length)];
        source.volume = SFXVolume;
        source.PlayOneShot(source.clip);
    }
    public void PlayDeathSounds()
    {
        source.clip = DeathSounds[Random.Range(0, DeathSounds.Length)];
        source.volume = SFXVolume;
        source.PlayOneShot(source.clip);
    }
    public IEnumerator StartBGMMusic()
    {
        while (true)
        {
            AudioClip audioClip = BGM[Random.Range(0, BGM.Length)];
            source.clip = audioClip;
            source.volume = BGMVolume;
            source.PlayOneShot(source.clip);
            yield return new WaitForSeconds(audioClip.length);
        }
    }    
}
