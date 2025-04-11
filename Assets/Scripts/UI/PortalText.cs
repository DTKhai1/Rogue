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
        if (portal != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(portal.transform.position + offset);
        }
        else
        {
            UpdateManager.UnregisterUpdateObserver(this);
            isRegistered = false;
        }
    }
}
