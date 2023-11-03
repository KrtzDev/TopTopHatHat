using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonIndicator : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject _buttonSelector;
    public void OnSelect(BaseEventData eventData)
    {
        _buttonSelector.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _buttonSelector.SetActive(false);
    }
}
