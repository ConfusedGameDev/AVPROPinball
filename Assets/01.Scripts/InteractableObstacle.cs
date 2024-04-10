using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InteractableObstacle : MonoBehaviour
{

    public int scoreValue;
    public PlayableDirector director;
    public bool oneShot;
    public Collider objectCollider;
    public AudioSource source;
    public void onActivate()
    {
        ScoreManager.Instance.UpdateScore(scoreValue);
        if (director)
            director.Play();
        if(!source)
        {
            source = GetComponent<AudioSource>();
            if (source)
                source.Play();
        }
        if (oneShot)
            objectCollider.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent<Collider>();
        director = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
