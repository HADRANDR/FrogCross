using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;
    public static bool Pause = false;
    int _faceCount;
    float _delayTime;
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
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movementControl = true;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        _delayTime += Time.deltaTime;
        if (MenuManager.startControl == true)
        {
            if (_delayTime >= 3)
            {
                if (Input.GetMouseButtonDown(0)) // İlk Input verisinin tutulma aşamasını
                {
                    _firstPointX = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
                    _firstPointY = Camera.main.ScreenToViewportPoint(new Vector3(0, Input.mousePosition.y, 0));

                    _firstPointXtemp = _firstPointX.x;
                    _firstPointYtemp = _firstPointY.y;
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
                            _faceCount = 1;
                            rb.gameObject.transform.DORotate(new Vector3(0, -90, 0), 0.5f);
                            rb.gameObject.transform.DOMoveY(1, _jumpCount);
                            rb.gameObject.transform.DOMoveX(transform.position.x - speed, _speedCount);
                            animator.SetBool("Jump", true);
                            audioSource.Play();
                        }
                        if (_directionDifX < 0) // // X ekseninde sağa doğru hareket sağlandı mı?
                        {
                            _faceCount = 2;
                            rb.gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
                            rb.gameObject.transform.DOMoveY(1, _jumpCount);
                            rb.gameObject.transform.DOMoveX(transform.position.x + speed, _speedCount);
                            animator.SetBool("Jump", true);
                            audioSource.Play();
                        }
                    }
                    if (Mathf.Abs(_directionDifX) < Mathf.Abs(_directionDifY)) // Ekranda Y ekseninde X ekseninden daha fazla hareket sağlandı mı?
                    {
                        if (_directionDifY > 0) // Y ekseninde aşağı doğru hareket sağlandı mı?
                        {
                            _faceCount = 3;
                            rb.gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.5f);
                            rb.gameObject.transform.DOMoveY(1, _jumpCount);
                            rb.gameObject.transform.DOMoveZ(transform.position.z - speed, _speedCount);
                            animator.SetBool("Jump", true);
                            audioSource.Play();
                        }
                        if (_directionDifY < 0) // Y ekseninde yukarı doğru hareket sağlandı mı?
                        {
                            _faceCount = 3;
                            rb.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                            rb.gameObject.transform.DOMoveY(1, _jumpCount);
                            rb.gameObject.transform.DOMoveZ(transform.position.z + speed, _speedCount);
                            animator.SetBool("Jump", true);
                            audioSource.Play();
                        }
                    }
                    float _difGap = Mathf.Abs(_directionDifX) - Mathf.Abs(_directionDifY); // Ekrana kaydırılmadan düz bir şekilde basıldı mı?
                    if (Mathf.Abs(_difGap) < 0.01)
                    {
                        _faceCount = 3;
                        rb.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                        rb.gameObject.transform.DOMoveY(1, _jumpCount);
                        rb.gameObject.transform.DOMoveZ(transform.position.z + speed, _speedCount);
                        audioSource.Play();
                    }
                }
            }
        }
               
    }
    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("Jump", false);
        movementControl = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        movementControl = true;
        if (collision.collider.CompareTag("Car"))   
        {
            Pause = true;
            if (_faceCount ==1)
            {
                rb.gameObject.transform.DORotate(new Vector3(-90, -90, 0), 2f);
                rb.gameObject.transform.DOMoveY(3, _jumpCount);
                rb.gameObject.transform.DOMoveX(transform.position.z + 7.5f, 1f);
            }
            else if (_faceCount == 2)
            {
                rb.gameObject.transform.DORotate(new Vector3(90, 90, 0), 2f);
                rb.gameObject.transform.DOMoveY(3, _jumpCount);
                rb.gameObject.transform.DOMoveX(-transform.position.z + 7.5f, 1f);
            }
            else if (_faceCount == 3)
            {
                rb.gameObject.transform.DORotate(new Vector3(0, 0, -90), 2f);
                rb.gameObject.transform.DOMoveY(3, _jumpCount);
                rb.gameObject.transform.DOMoveX(transform.position.z + 7.5f, 1f);
            }
            Debug.Log("KAYBETTİN");
            menuManager.LoseScreen();           
        }  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("finish"))
        {
            Debug.Log("KAZANDIN");
            menuManager.WinScreen();
        }
    }
}

