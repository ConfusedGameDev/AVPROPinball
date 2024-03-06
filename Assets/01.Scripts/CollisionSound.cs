using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]

public class CollisionSound : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource component

    public void OnCollisionEnter(Collision collision)
    {
        audioSource.Play(); // Play the sound
    }
}


