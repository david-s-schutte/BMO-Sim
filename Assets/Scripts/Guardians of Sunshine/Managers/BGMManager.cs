using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        bgm.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Submit"))
            bgm.volume = 0.2f;
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void StartBGM()
    {
        bgm.Play();
    }
}
