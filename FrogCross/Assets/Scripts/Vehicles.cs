using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles : MonoBehaviour
{
    int _repeatingCount;
    [SerializeField] int _repeatingStop;
    [SerializeField] float _repeatingRate;
    [SerializeField] private GameObject[] Array;
    private void Awake()
    {
        InvokeRepeating(nameof(Create), 1f, _repeatingRate);
    }
    void Update()
    {
        if (_repeatingCount >= _repeatingStop)
        {
            CancelInvoke(nameof(Create));
        }
    }
    void Create()
    {
        foreach (var item in Array)
        {
            Instantiate(item, gameObject.transform.position, gameObject.transform.rotation);
            item.SetActive(true);
        }
        _repeatingCount++;

    }
}
