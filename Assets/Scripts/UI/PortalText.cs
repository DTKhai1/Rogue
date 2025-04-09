using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalText : MonoBehaviour, IUpdateObserver
{
    public GameObject portal;
    public Vector3 offset;
    private bool isRegistered = false;
    private void OnEnable()
    {
        if (!isRegistered)
        {
            UpdateManager.RegisterUpdateObserver(this);
            isRegistered = true;
        }
    }

    private void OnDisable()
    {
        if (isRegistered)
        {
            UpdateManager.UnregisterUpdateObserver(this);
            isRegistered = false;
        }
    }

    public void ObservedUpdate()
    {
        if (portal != null)  // Check if portal still exists
        {
            transform.position = Camera.main.WorldToScreenPoint(portal.transform.position + offset);
        }
        else
        {
            // If portal is destroyed, unregister this observer
            UpdateManager.UnregisterUpdateObserver(this);
            isRegistered = false;
        }
    }
}
