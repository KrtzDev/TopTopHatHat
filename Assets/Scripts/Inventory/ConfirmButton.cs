using System.Collections;
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

    public void ConfirmSelection()
    {
        CheckBools();
        if(_boolCounter == 2)
        {
            // activate effects

            // go to next level

            Debug.Log("Confirm Selection");

            DisableNotification();
        }
        else
        {
            StartCoroutine(FadeInTextDisclaimer(0f, 1f));
        }
    }

    private void CheckBools()
    {
        _boolCounter = 0;

        for (int i = 0; i < _cardsArray.Length; i++)
        {
            Debug.Log(i + " : " + _cardsArray[i].IsPicked());

            if (_cardsArray[i].IsPicked())
            {
                _boolCounter++;
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
