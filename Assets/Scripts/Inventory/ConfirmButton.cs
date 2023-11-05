using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ConfirmButton : MonoBehaviour
{
    [SerializeField] private CardSelection[] _cardsArray = new CardSelection[5];
    [SerializeField] private GameObject _confirmNotification;
    private Color _currentColor;
    [SerializeField] private float _fadeRate;
    [SerializeField] private float _waitTimeForFadeOut;
    private int _boolCounter;

    [SerializeField] private List<TaskAndGiveGenerator.TakeByID> _usedTakeTasks = new();
    [SerializeField] private List<TaskAndGiveGenerator.TakeByID> _NOTusedTakeTasks = new();
    [SerializeField] private List<TaskAndGiveGenerator.GiveByID> _usedGiveTasks = new();
    [SerializeField] private List<TaskAndGiveGenerator.GiveByID> _NOTusedGiveTasks = new();

    public void ConfirmSelection()
    {
        CheckBools();
        if(_boolCounter == 2)
        {
            for (int i = 0; i < _usedTakeTasks.Count; i++)
            {
                // put used Tasks to Ability Manager
                AbilityManager.instance.AddToTakeList(_usedTakeTasks[i]);
                GameManager.instance.PauseMenu.AddTaskToText_Take(_usedTakeTasks[i].text);
            }

            for (int i = 0; i < _NOTusedTakeTasks.Count; i++)
            {
                // put not used Tasks to Task Generator
                TaskAndGiveGenerator.instance.AddToTakeList(_NOTusedTakeTasks[i]);
            }

            for (int i = 0; i < _usedGiveTasks.Count; i++)
            {
                // put used Tasks to Ability Manager
                AbilityManager.instance.AddToGiveList(_usedGiveTasks[i]);
                GameManager.instance.PauseMenu.AddTaskToText_Give(_usedGiveTasks[i].text);
            }

            for (int i = 0; i < _NOTusedGiveTasks.Count; i++)
            {
                // put not used Tasks to Task Generator
                TaskAndGiveGenerator.instance.AddToGiveList(_NOTusedGiveTasks[i]);
            }

            // go to next level

            Debug.Log("Confirm Selection");

            SFXManager.instance.PlaySound("ConfirmButton");

            DisableNotification();

			SceneLoader.instance.LoadScene(SceneLoader.instance.SceneToLoad);
        }
        else
        {
            StartCoroutine(FadeInTextDisclaimer(0f, 1f));
        }
    }

    private void CheckBools()
    {
        _boolCounter = 0;
        _usedTakeTasks = new();
        _NOTusedTakeTasks = new();
        _usedGiveTasks = new();
        _NOTusedGiveTasks = new();

        for (int i = 0; i < _cardsArray.Length; i++)
        {
            Debug.Log(i + " : " + _cardsArray[i].IsPicked());

            if (_cardsArray[i].IsPicked())
            {
                _boolCounter++;
                _usedTakeTasks.Add(_cardsArray[i].ActiveTakeTask());
                _NOTusedGiveTasks.Add(_cardsArray[i].ActiveGiveTask());
            }
            else
            {
                _usedGiveTasks.Add(_cardsArray[i].ActiveGiveTask());
                _NOTusedTakeTasks.Add(_cardsArray[i].ActiveTakeTask());
            }
        }
    }

    private IEnumerator FadeInTextDisclaimer(float startAlpha, float targetAlpha)
    {
        _confirmNotification.SetActive(true);
        _currentColor = _confirmNotification.GetComponent<TextMeshProUGUI>().color;
        _currentColor = new Color(_currentColor.r, _currentColor.g, _currentColor.b, startAlpha);

        while(_currentColor.a <= Mathf.Abs(targetAlpha - 0.05f)) 
        {
            _currentColor.a = Mathf.Lerp(_currentColor.a, targetAlpha, _fadeRate * Time.unscaledDeltaTime);
            _confirmNotification.GetComponent<TextMeshProUGUI>().color = _currentColor;
            yield return null;
        }

        Debug.Log("Finish Couroutine");

        yield return new WaitForSecondsRealtime(_waitTimeForFadeOut);

        StartCoroutine(FadeOutTextDisclaimer(1f, 0f));
    }

    private IEnumerator FadeOutTextDisclaimer(float startAlpha, float targetAlpha)
    {
        Debug.Log("Start Fade Out");

        _currentColor = _confirmNotification.GetComponent<TextMeshProUGUI>().color;
        _currentColor = new Color(_currentColor.r, _currentColor.g, _currentColor.b, startAlpha);

        while (_currentColor.a != targetAlpha)
        {
            _currentColor.a = Mathf.Lerp(_currentColor.a, targetAlpha, _fadeRate * Time.unscaledDeltaTime);
            _confirmNotification.GetComponent<TextMeshProUGUI>().color = _currentColor;
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        _confirmNotification.SetActive(false);
    }

    private void DisableNotification()
    {
        _confirmNotification.SetActive(false);
    }
}
