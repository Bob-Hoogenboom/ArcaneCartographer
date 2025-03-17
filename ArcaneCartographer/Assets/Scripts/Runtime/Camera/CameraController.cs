using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 10f;
    [SerializeField]
    private float scrollSpeed = 10f;
    
    private Camera _cam;


    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        Move();
        ScrollZoom();
    }

    private void Move()
    {
        float camSpeed = _cam.fieldOfView * cameraSpeed;

        Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        gameObject.transform.position += moveVec * camSpeed * Time.deltaTime;
    }

    private void ScrollZoom()
    {
        _cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    }


}
