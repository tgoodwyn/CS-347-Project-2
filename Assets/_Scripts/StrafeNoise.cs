using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeNoise : MonoBehaviour
{
    bool IsMoving = true;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (Math.Abs(horizontal) > 0 ) IsMoving = true;
        else IsMoving = false;
        playAudio();
    }

    void playAudio()
    {

        //if (IsMoving && !audioSource.isPlaying && playTime > delay)
        if (IsMoving && !audioSource.isPlaying)
        {
            audioSource.Play(); // if player is moving and audiosource is not playing play it
            //playTime = 0;
        }
        if (!IsMoving) audioSource.Stop(); // if player is not moving and audiosource is playing stop
    }
}
