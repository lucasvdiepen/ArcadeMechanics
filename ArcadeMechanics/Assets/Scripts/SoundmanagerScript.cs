using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.UI;
>>>>>>> development

public class SoundmanagerScript : MonoBehaviour
{
    public AudioClip[] JumpSounds;
    public AudioClip[] DeathSounds;
    public AudioClip[] BGM;
    private AudioSource source;
<<<<<<< HEAD
    public float SoundsVolume = 0.5f;
    public float BGMVolume = 0.3f;

=======
    float BGMVolume = 0.3f;
    float SFXVolume = 0.5f;
>>>>>>> development


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(StartBGMMusic());
        StartBGMMusic();

    }
<<<<<<< HEAD
    // Update is called once per frame
    void Update()
    {
        
    }
=======
>>>>>>> development

    public void PlayJumpSounds()
    {
        source.clip = JumpSounds [Random.Range(0, JumpSounds.Length)];
<<<<<<< HEAD
        source.volume = SoundsVolume;
=======
        source.volume = SFXVolume;
>>>>>>> development
        source.PlayOneShot(source.clip);
    }
    public void PlayDeathSounds()
    {
        source.clip = DeathSounds[Random.Range(0, DeathSounds.Length)];
<<<<<<< HEAD
        source.volume = SoundsVolume;
=======
        source.volume = SFXVolume;
>>>>>>> development
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
