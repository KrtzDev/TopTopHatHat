using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class FirstSelectedButton : MonoBehaviour
{
    private Button _thisButton;
    [SerializeField] private GameObject SelectorIndicator;

    private void Awake()
    {
        _thisButton = gameObject.GetComponent<Button>();
        _thisButton.Select();
    }

    private void OnEnable()
    {
        _thisButton = gameObject.GetComponent<Button>();
        _thisButton.Select();

        if(SelectorIndicator != null)
        {
            SelectorIndicator.SetActive(true);
        }
    }
}
