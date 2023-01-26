using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    float time;
    Rigidbody rb;
    Animator animator;
    [SerializeField] private float _speedCount = 0.5f; // X ekseninde belirtilen birim ilerleme süresi
    [SerializeField] private float _jumpCount = 0.1f; // Y ekseninde belirtilen birim ilerleme süresi
    [SerializeField] private float speed = 5f; // X ekseninde kaç birim ilerleneceğini belirler.
    private Vector3 _firstPointX; // Ekranda ilk basılan X noktayı referans alır.
    private Vector3 _stayPointX; // Ekranda basılı tutulan X noktayı referans alır.
    private Vector3 _firstPointY;  // Ekranda ilk basılan Y noktayı referans alır.
    private Vector3 _stayPointY;// Ekranda basılı tutulan Y noktayı referans alır.
    private float _firstPointXtemp; // Ekranda ilk basılan X noktanın referansını float değerde taşır.
    private float _firstPointYtemp; // Ekranda ilk basılan Y noktanın referansını float değerde taşır.
    private float _stayPointXtemp; // Ekranda sürekli basılan X noktanın referansını float değerde taşır.
    private float _stayPointYtemp; // Ekranda sürekli basılan Y noktanın referansını float değerde taşır.
    private float _directionDifX; // X ekseninde ekranda ilk basılan ve sürekli basılan referanslar arasındaki değeri tutar.
    private float _directionDifY; // Y ekseninde ekranda ilk basılan ve sürekli basılan referanslar arasındaki değeri tutar.
    private bool movementControl; // Animasyon esnasında tekrar kontrolü sağlar.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movementControl = true;
    }
    void Update()
    { 
        if (Input.GetMouseButtonDown(0)) // İlk Input verisinin tutulma aşamasını
        {
            _firstPointX = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
            _firstPointY = Camera.main.ScreenToViewportPoint(new Vector3(0, Input.mousePosition.y, 0));

            _firstPointXtemp = _firstPointX.x;
            _firstPointYtemp = _firstPointY.y;

            Debug.Log(_firstPointXtemp);
            Debug.Log(_firstPointYtemp);
        }
        if (Input.GetMouseButton(0))
        {
            _stayPointX = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
            _stayPointY = Camera.main.ScreenToViewportPoint(new Vector3(0, Input.mousePosition.y, 0));

            _stayPointXtemp = _stayPointX.x;
            _stayPointYtemp = _stayPointY.y;

            _directionDifX = _firstPointXtemp - _stayPointXtemp;
            _directionDifY = _firstPointYtemp - _stayPointYtemp;
        }
        if (Input.GetMouseButtonUp(0) && movementControl == true)
        {

            if (Mathf.Abs(_directionDifX) > Mathf.Abs(_directionDifY)) // Ekranda X ekseninde Y ekseninden daha fazla hareket sağlandı mı?
            {
                if (_directionDifX > 0) // X ekseninde sola doğru hareket sağlandı mı?
                {
                    rb.gameObject.transform.DORotate(new Vector3(0, -90, 0), 0.5f);
                    rb.gameObject.transform.DOMoveY(1, _jumpCount);
                    rb.gameObject.transform.DOMoveX(transform.position.x - speed, _speedCount);               
                    animator.SetBool("Jump", true);
                }
                if (_directionDifX < 0) // // X ekseninde sağa doğru hareket sağlandı mı?
                {
                    rb.gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
                    rb.gameObject.transform.DOMoveY(1, _jumpCount);
                    rb.gameObject.transform.DOMoveX(transform.position.x + speed, _speedCount);
                    animator.SetBool("Jump", true);
                }
            }
            if (Mathf.Abs(_directionDifX) < Mathf.Abs(_directionDifY)) // Ekranda Y ekseninde X ekseninden daha fazla hareket sağlandı mı?
            {
                if (_directionDifY > 0) // Y ekseninde aşağı doğru hareket sağlandı mı?
                {
                    rb.gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.5f);
                    rb.gameObject.transform.DOMoveY(1, _jumpCount);
                    rb.gameObject.transform.DOMoveZ(transform.position.z - speed, _speedCount);
                    animator.SetBool("Jump", true);
                }
                if (_directionDifY < 0) // Y ekseninde yukarı doğru hareket sağlandı mı?
                {
                    rb.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                    rb.gameObject.transform.DOMoveY(1, _jumpCount);
                    rb.gameObject.transform.DOMoveZ(transform.position.z + speed, _speedCount);
                    animator.SetBool("Jump", true);
                }             
            }
            float _difGap = Mathf.Abs(_directionDifX) - Mathf.Abs(_directionDifY); // Ekrana kaydırılmadan düz bir şekilde basıldı mı?
            if (Mathf.Abs(_difGap) < 0.01)
            {
                rb.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                rb.gameObject.transform.DOMoveY(1, _jumpCount);
                rb.gameObject.transform.DOMoveZ(transform.position.z+ speed, _speedCount);
            }
        } 
    }
    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("Jump", false);
        Debug.Log("Zeminden ayrıldı");
        movementControl = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Zemine düştü");
        movementControl = true;
        if (collision.collider.CompareTag("Car"))   
        {
            rb.gameObject.transform.DOScaleX(0.1f, 0.1f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("finish"))
        {
            Debug.Log("finish");
        }
    }

}
