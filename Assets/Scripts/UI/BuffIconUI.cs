using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconUI : MonoBehaviour
{
    [SerializeField] private Image buffIcon;
    [SerializeField] private TMP_Text buffDescription;
    public GameObject _description;
    private void Awake()
    {
        _description.SetActive(false);
    }
    public void SetupBuff(Buff buff)
    {
        if (buff == null) return;

        if (buffIcon != null) buffIcon.sprite = buff.icon;
        if (buffDescription != null) buffDescription.text = buff.description;
    }
    public void ToggleDescription()
    {
        if (_description.activeSelf)
        {
            _description.SetActive(false);
        }
        else
        {
            _description.SetActive(true);
        }
    }
}
