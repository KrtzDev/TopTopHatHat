using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTopHats : MonoBehaviour
{
    [SerializeField] private GameObject _topHat_Prefab;

    [SerializeField, Range(0f, 0.05f)] private float _topHatSizeIncrease;
    [SerializeField, Range(0f, 0.05f)] private float _topHatHeightIncrease;
    [SerializeField, Range(0f, 20f)] private float _randomXRotationMax;
    [SerializeField, Range(-20f, 0f)] private float _randomXRotationMin;
    [SerializeField, Range(0f, 20f)] private float _randomZRotationMax;
    [SerializeField, Range(-20f, 0f)] private float _randomZRotationMin;

    [SerializeField] List<GameObject> _topHatList;

    private void Start()
    {
        for(int i = 0; i < _topHatList.Count; i++)
        {
            Transform tempTransform = _topHatList[i].GetComponent<Transform>();
            tempTransform.localPosition = new Vector3(tempTransform.localPosition.x, _topHatHeightIncrease * i, tempTransform.localPosition.z);
            tempTransform.localScale = new Vector3(1 + _topHatSizeIncrease * i, 1 + _topHatSizeIncrease * i, 1 + _topHatSizeIncrease * i);

            Vector3 eulerRotation = new Vector3(Random.Range(_randomXRotationMin, _randomXRotationMax), 0, Random.Range(_randomZRotationMin, _randomZRotationMax));
            Quaternion newRotation = Quaternion.Euler(eulerRotation);
            tempTransform.localRotation = newRotation;

            AddTopHat(tempTransform);
        }
    }

    private void AddTopHat(Transform parent)
    {
        GameObject _topHat = _topHat_Prefab;
        _topHat.GetComponent<Transform>().localPosition = Vector3.zero;
        Instantiate(_topHat, parent);
    }
}
