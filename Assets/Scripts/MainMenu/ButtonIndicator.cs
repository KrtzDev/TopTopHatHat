using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonIndicator : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject _buttonSelector;
    public void OnSelect(BaseEventData eventData)
    {
        _buttonSelector.SetActive(true);
        SFXManager.instance.PlaySound("ButtonSelect");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _buttonSelector.SetActive(false);
    }

    public void OnDisable()
    {
        _buttonSelector.SetActive(false);
    }

}
