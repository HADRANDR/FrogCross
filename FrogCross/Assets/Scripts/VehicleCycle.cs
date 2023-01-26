using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCycle : MonoBehaviour
{
    [SerializeField]int random;
    private void Awake() 
    {

    }
    void Update()
    {
        this.gameObject.transform.Translate(random * Time.deltaTime * Vector3.forward);
    }
}
