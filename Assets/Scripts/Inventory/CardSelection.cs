using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSelection : MonoBehaviour
{
    [SerializeField] private bool _isPicked;
    private Vector3 _newScale = new Vector3(2.5f, 2.5f, 1f);
    private Vector3 _oldScale;

    private void Awake()
    {
        _oldScale = gameObject.GetComponent<RectTransform>().localScale;
        _isPicked = false;
    }

    public void OnCardClick()
    {
        if(!_isPicked)
        {
            PickCard();
        }
        else
        {
            UnPickCard();
        }
    }

    private void PickCard()
    {
        _isPicked = true;
        gameObject.GetComponent<RectTransform>().localScale = _newScale;
    }

    private void UnPickCard()
    {
        _isPicked = false;
        gameObject.GetComponent<RectTransform>().localScale = _oldScale;
    }

    public bool IsPicked()
    {
        return _isPicked;
    }
}
