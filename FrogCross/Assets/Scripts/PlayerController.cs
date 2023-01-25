using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    [SerializeField] private float _speedCount = 0.5f;
    [SerializeField] private float _jumpCount = 0.1f;
    private Vector3 _firstPointX;
    private Vector3 _stayPointX;
    private Vector3 _firstPointY;
    private Vector3 _stayPointY;
    private float _firstPointXtemp;
    private float _firstPointYtemp;
    private float _stayPointXtemp;
    private float _stayPointYtemp;
    private float _directionDifX;
    private float _directionDifY;
    private bool movementControl;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movementControl = true;
    }

    // Update is called once per frame
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

            if (Mathf.Abs(_directionDifX) > Mathf.Abs(_directionDifY))
            {
                if (_directionDifX>0)
                {
                    rb.gameObject.transform.DORotate(new Vector3(0, -90, 0), 0.5f);
                    rb.gameObject.transform.DOMoveY(1, _jumpCount);
                    rb.gameObject.transform.DOMoveX(transform.position.x - 1, _speedCount);               
                    animator.SetBool("Jump", true);
                }
                if (_directionDifX < 0)
                {
                    rb.gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
                    rb.gameObject.transform.DOMoveY(1, _jumpCount);
                    rb.gameObject.transform.DOMoveX(transform.position.x + 1, _speedCount);
                    animator.SetBool("Jump", true);
                }
            }
            if (Mathf.Abs(_directionDifX) < Mathf.Abs(_directionDifY))
            {
                if (_directionDifY > 0)
                {
                    rb.gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.5f);
                    rb.gameObject.transform.DOMoveY(1, _jumpCount);
                    rb.gameObject.transform.DOMoveZ(transform.position.z - 1, _speedCount);
                    animator.SetBool("Jump", true);
                }
                if (_directionDifY < 0)
                {
                    rb.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                    rb.gameObject.transform.DOMoveY(1, _jumpCount);
                    rb.gameObject.transform.DOMoveZ(transform.position.z + 1, _speedCount);
                    animator.SetBool("Jump", true);
                }             
            }
            float _difGap = Mathf.Abs(_directionDifX) - Mathf.Abs(_directionDifY);
            if (Mathf.Abs(_difGap) < 0.01)
            {
                rb.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                rb.gameObject.transform.DOMoveY(1, _jumpCount);
                rb.gameObject.transform.DOMoveZ(transform.position.z+1, _speedCount);
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
    }


}
