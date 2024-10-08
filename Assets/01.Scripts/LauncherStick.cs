using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LauncherStick : PinchInteractable
{
    public Transform stickTransform;
    public Vector3 startPos;
    public bool isLaunching;
    float delta;
    public Vector3 launchStarPos;
    public UnityEvent onFinishedLaunch;
    public float speed = 3f;
    public float force = 25f;
    public Ball ball;
    public float animationDuration = 1f;
    public float maxDeltaZ = 0.59f;
    public bool canLaunch = true;

    public Vector2 minMaxForce = new Vector2(75, 250);

    public float ballDetectionDistance = 0.1f;
    public LayerMask ballLayer;
    public Transform ballChecker;
    // Start is called before the first frame update
    void Start()
    {
        startPos = stickTransform.localPosition;
    }

    public void setup()
    {
        canLaunch = true;
        stickTransform.localPosition = startPos;
    }
    public override void updateDelta(Vector3 currentDelta)
    {
        
       // base.updateDelta(currentDelta);
       // if (!isLaunching)
         //   stickTransform.position = startPos + stickTransform.forward * zDelta;
    }
    public void Activate()
    {
        Debug.Log("Activate Collider");
        GetComponent<Collider>().enabled = true;
    }
    public void onReleasePinch()
    {
        delta = 0;
        isLaunching = true;
        launchStarPos = stickTransform.position;
         
    }
    public void startShoot()
    {
       
       
        if(canLaunch)   
        {
            StartCoroutine(animateLaunch());
        }
        
    }
    public IEnumerator animateLaunch()
    {
        canLaunch = false;
        float d = 0f;
        launchStarPos = stickTransform.localPosition;
        var launchEndPos= launchStarPos;
        launchEndPos.z = maxDeltaZ;

        while (d < animationDuration)
        {
            yield return new WaitForEndOfFrame();
            d += Time.deltaTime;
            stickTransform.localPosition = Vector3.Lerp(launchStarPos, launchEndPos, d / animationDuration);

        }
        
        while(d>0)
        {
            yield return new WaitForEndOfFrame();
            d -= Time.deltaTime*3f;
            stickTransform.localPosition = Vector3.Lerp(launchStarPos, launchEndPos, d / animationDuration);
        }
        Shoot();
         
    }
    public void FixedUpdate()
    {
        /*if (isLaunching)
        {
            stickTransform.position = Vector3.Lerp(launchStarPos, startPos, delta);
            delta += Time.deltaTime*(speed*zDelta);
        }
        if (delta >= 1f)
        {
            Shoot(); 

        }
        */
        Ray ray = new Ray(ballChecker.position, ballChecker.forward);
        RaycastHit hito;
        if (ballChecker && Physics.Raycast(ray, out hito,ballDetectionDistance, ballLayer))
        {
            Debug.Log(hito.transform.name);
            Ball b;
            canLaunch = hito.transform.TryGetComponent<Ball>(out b);


        }
        else
        {
            canLaunch = false;
        }

    }
    public void Shoot()
    {
        delta = 0;
        isLaunching = false;
        onFinishedLaunch.Invoke();
        if (!ball)
            ball = FindObjectOfType<Ball>();
        if (ball)
        {
            var currentForce = Random.Range(minMaxForce.x, minMaxForce.y);
            ball.addImpulse(transform.forward * currentForce);
            Debug.Log("Current Force " + currentForce);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            startShoot();

    }
}
