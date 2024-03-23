using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObstacle : MonoBehaviour
{

    public int scoreValue;
    public UnityEngine.Playables.PlayableDirector director;
    public bool oneShot;
    public Collider objectCollider;
    public void onActivate()
    {
        ScoreManager.Instance.UpdateScore(scoreValue);
        if (director)
            director.Play();
        if (oneShot)
            objectCollider.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
