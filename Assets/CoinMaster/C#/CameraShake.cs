using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _power;

    private Camera _camera;
    private Transform _transform;
    private Quaternion _rotation;
    void Start()
    {
        _camera = GetComponent<Camera>();
        if(_camera == null )
            _camera = Camera.main;

        _transform = _camera.transform;

        _rotation = _transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Shake(5);
        float y = Shake(6);
        _transform.rotation = _rotation * Quaternion.Euler(x, y, 0);
    }
    float Shake(float value)
        => Mathf.Sin(value * Time.time * Mathf.Deg2Rad * _speed) * _power;
}
