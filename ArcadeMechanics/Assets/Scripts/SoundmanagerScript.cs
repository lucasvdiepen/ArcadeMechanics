using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundmanagerScript : MonoBehaviour
{
    public AudioClip[] JumpSounds;
    public AudioClip[] DeathSounds;
    public AudioClip[] BGM;
    private AudioSource source;
    public float SoundsVolume = 0.5f;
    public float BGMVolume = 0.3f;



    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(StartBGMMusic());
        StartBGMMusic();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayJumpSounds()
    {
        source.clip = JumpSounds [Random.Range(0, JumpSounds.Length)];
        source.volume = SoundsVolume;
        source.PlayOneShot(source.clip);
    }
    public void PlayDeathSounds()
    {
        source.clip = DeathSounds[Random.Range(0, DeathSounds.Length)];
        source.volume = SoundsVolume;
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
