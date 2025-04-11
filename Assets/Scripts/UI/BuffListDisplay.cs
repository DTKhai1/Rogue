using UnityEngine;

public class BuffListDisplay : MonoBehaviour
{
    [SerializeField] private BuffList buffList;              
    [SerializeField] private GameObject buffUIPrefab;        
    [SerializeField] private Transform contentParent;        
    private void Start()
    {
        DisplayBuffs();
    }

    public void DisplayBuffs()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Buff buff in buffList.allBuffs)
        {
            GameObject buffDisplay = Instantiate(buffUIPrefab, contentParent);
            BuffIconUI buffUI = buffDisplay.GetComponent<BuffIconUI>();
            if (buffUI != null)
            {
                buffUI.SetupBuff(buff);
            }
        }
    }

    public void RefreshDisplay()
    {
        DisplayBuffs();
    }
}