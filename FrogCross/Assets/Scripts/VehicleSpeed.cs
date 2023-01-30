using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpeed : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rb;
    [SerializeField] int _speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (PlayerController.Pause == false)
        {
            this.gameObject.transform.Translate(_speed * Time.deltaTime * Vector3.forward);
            if (this.gameObject.transform.position.x >= 70)
            {
                this.gameObject.SetActive(false);
                this.gameObject.transform.Translate(new Vector3(0, 0, -140));
                this.gameObject.SetActive(true);
            }
        }
        else rb.constraints = RigidbodyConstraints.FreezePosition;
      
    }
}
