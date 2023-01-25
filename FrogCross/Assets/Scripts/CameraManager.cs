using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform Player;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = gameObject.transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new((Player.transform.position.x) + offset.x, gameObject.transform.position.y, Player.transform.position.z + offset.z);
        gameObject.transform.position = newPos;
    }
}
