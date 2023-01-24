using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    private Vector3 _firstPoint;
    private Vector3 _stayPoint;

    private Vector3 _firstPointX;
    private Vector3 _stayPointX;
    private Vector3 _firstPointY;
    private Vector3 _stayPointY;
    private float _instantRotationSpeed;
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
        if (Input.GetMouseButtonUp(0))
        {
            if (Mathf.Abs(_directionDifX) > Mathf.Abs(_directionDifY))
            {
                if (_directionDifX>0)
                {
                    rb.AddForce(5000 * Vector3.left);
                }
                if (_directionDifX < 0)
                {
                    rb.AddForce(5000 * Vector3.right);
                }
            }
            if (Mathf.Abs(_directionDifX) < Mathf.Abs(_directionDifY))
            {
                if (_directionDifY > 0)
                {
                    rb.AddForce(5000 * Vector3.back);
                }
                if (_directionDifY < 0)
                {
                    rb.AddForce(5000 * Vector3.forward);
                }             
            }
            float _difGap = Mathf.Abs(_directionDifX) - Mathf.Abs(_directionDifY);
            if (Mathf.Abs(_difGap) < 0.01)
            {
                rb.AddForce(5000 * Vector3.forward);
            }
        }
    }
}
