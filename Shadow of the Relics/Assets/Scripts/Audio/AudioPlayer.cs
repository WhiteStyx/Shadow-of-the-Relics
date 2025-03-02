using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    static List<AudioPlayer> ActiveAudios = new List<AudioPlayer>();

    public AudioSource source;
    public float minPitch, maxPitch, length;

    bool playing, onCooldown;

    void OnEnable()
    {
        ActiveAudios.Add(this);
    }

    void OnDisable()
    {
        ActiveAudios.Remove(this);
    }

    public void Play()
    {
        if(onCooldown)
            return;

        //playing = true;
        PlayAudio();
        StartCoroutine(Cooldown(length));
    }

    public void Stop()
    {
        //playing = false;
    }

    IEnumerator Cooldown(float time)
    {
        onCooldown = true;
        yield return new WaitForSeconds(time);
        onCooldown = false;
    }

    void PlayAudio()
    {
        source.Stop();
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
    }

    public void SetVolume()
    {

    }

    public static void PauseAll(bool pause)
    {
        foreach(AudioPlayer audio in ActiveAudios)
        {
            audio.Pause(pause);
        }
    }

    void Pause(bool pause)
    {
        if(pause)
        {
            if(source.isPlaying)
                source.Pause();
            return;
        }
        source.UnPause();
    }
}
