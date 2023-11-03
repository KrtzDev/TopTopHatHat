using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class FirstSelectedButton : MonoBehaviour
{
    private Button _thisButton;

    private void Awake()
    {
        _thisButton = gameObject.GetComponent<Button>();
        _thisButton.Select();
    }
}
