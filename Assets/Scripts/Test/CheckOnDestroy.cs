using UnityEngine;

public class CheckOnDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDestroy()
    {
        Debug.Log(gameObject + " is destroyed");
    }
}
