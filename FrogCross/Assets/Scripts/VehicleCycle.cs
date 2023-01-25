using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCycle : MonoBehaviour
{
    int random;
    private void Awake() 
    {
        random = Random.Range(5, 15);
    }
    void Update()
    {
        this.gameObject.transform.Translate(random * Time.deltaTime * Vector3.forward);
    }
}
