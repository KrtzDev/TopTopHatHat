using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorNextLevel : MonoBehaviour
{
    [SerializeField] private string _nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TopHatCharacter>() != null)
        {
            SceneManager.LoadScene(_nextLevel);
        }
    }
}
