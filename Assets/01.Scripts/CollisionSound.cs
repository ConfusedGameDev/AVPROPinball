using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]

public class CollisionSound : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource component

    private void Awake()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();
        if(audioSource)
        audioSource.Play(); // Play the sound
    }
}


