using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class CardSelection : MonoBehaviour
{
    [SerializeField] private bool _isPicked;
    private Vector3 _newScale = new Vector3(2.5f, 2.5f, 1f);
    private Vector3 _oldScale;

    [SerializeField] private Image _icon;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _take;
    [SerializeField] private TextMeshProUGUI _give;

    private TaskAndGiveGenerator.TakeByID _takeTask;
    private TaskAndGiveGenerator.GiveByID _giveTask;

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

    public void SetCardNameIcon(RandomNameGenerator.WeaponIconByName iconname)
    {
        SetIcon(iconname.icon);
        SetName(iconname.name);
    }

    private void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    private void SetName(string name)
    {
        _name.text = name;
    }

    public void SetTake(TaskAndGiveGenerator.TakeByID takeTask)
    {
        _takeTask = takeTask;
        _take.text = takeTask.text;
    }

    public void SetGive(TaskAndGiveGenerator.GiveByID giveTask)
    {
        _giveTask = giveTask;
        _give.text = giveTask.text;
    }

    public TaskAndGiveGenerator.TakeByID ActiveTakeTask()
    {
        return _takeTask;
    }

    public TaskAndGiveGenerator.GiveByID ActiveGiveTask()
    {
        return _giveTask;
    }
}
