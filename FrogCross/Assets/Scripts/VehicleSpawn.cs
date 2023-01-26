using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VehicleSpawn : MonoBehaviour
{
    float time = 0;
    [SerializeField] private GameObject[] Array = new GameObject[10];
    // Start is called before the first frame update
    private void Awake()
    {
        VehicleSpawner();
    }
    void Start()
    {

    }
    void Update()
    {
        VehicleOffandReplay();
    }
    private void VehicleSpawner()
    {
        for (int i = 0; i < Array.Length; i++)
        {
            Instantiate(Array[i], gameObject.transform.position, gameObject.transform.rotation);
            Array[i].SetActive(true);
        }
    }
    private void VehicleSetTrue()
    {
        int _randomValue;
        for (int i = 0; i < Array.Length; i++)
        {
            _randomValue = Random.Range(0, 10);
            Array[_randomValue].SetActive(true);
        }
    }
    private void VehicleOffandReplay()
    {
        for (int i = 0; i < Array.Length; i++)
        {
            if (Array[i].transform.position.x==70)
            {
                Array[i].SetActive(false);
                Array[i].transform.Translate(new Vector3(-70, 0, 0));
                Array[i].SetActive(true);
            }
        }
    }

}
