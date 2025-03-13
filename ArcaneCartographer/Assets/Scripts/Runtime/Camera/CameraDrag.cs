using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Camera _cam;
    private Vector3 _origin;
    private Vector3 _difference;
    private Vector3 _resetCamera;

    private bool _isDragging = false;

    private void Start()
    {
        _cam = Camera.main;
        _resetCamera = _cam.transform.position;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _difference = (_cam.ScreenToWorldPoint(Input.mousePosition)) - _cam.transform.position;
            if(_isDragging == false)
            {
                _isDragging = true;
                _origin = _cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            _cam.transform.position = _origin - _difference;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _cam.transform.position = _resetCamera;
        }
    }
}
