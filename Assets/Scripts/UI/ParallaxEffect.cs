using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour, IUpdateObserver
{
    public Camera cam;
    Transform followTarget;

    Vector2 startingPosition;

    float startingZ;

    Vector2 camMoveSinceStart => (Vector2) cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // The futher the object from player, the faster the ParallaxEffect object will move. Drag it's Z value closer to the target to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;


    // Start is called before the first frame update
    void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    private void OnEnable()
    {
        UpdateManager.RegisterUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterUpdateObserver(this);
    }
    // Update is called once per frame
    public void ObservedUpdate()
    {
        // when the target moves, move the parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
