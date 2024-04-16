using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    AudioSource bgm;
    float volume;
    bool gameStarted = false;
    [SerializeField] AudioSource[] allSources;

    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        bgm.volume = 0.5f;
        volume = bgm.volume;
        allSources = Resources.FindObjectsOfTypeAll<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Submit"))
            gameStarted = true;

        bgm.volume = volume/2;
        foreach(AudioSource source in allSources)
        {
            if(source != null)
                source.volume = volume;
        }
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void StartBGM()
    {
        bgm.Play();
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }
}
