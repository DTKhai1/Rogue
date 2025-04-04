using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalText : MonoBehaviour
{
    public GameObject portal;
    public Vector3 offset;
    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(portal.transform.position + offset);
    }
}
