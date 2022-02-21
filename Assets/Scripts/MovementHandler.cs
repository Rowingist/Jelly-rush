using System;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private float _dragForce = 20f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _forwardSpeed = 10f;
    [SerializeField] private Rigidbody _rigidbody;

    private Transform _transform;

    private Vector3 _allowedMovementY;
    private Vector3 _allowedMovementX;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _allowedMovementY = Vector3.Lerp(_allowedMovementY, Input.GetAxisRaw("Mouse Y") * Vector3.up * _dragForce, Time.deltaTime * _speed);
            _allowedMovementX = Vector3.Lerp(_allowedMovementX, Input.GetAxisRaw("Mouse X") * Vector3.right * _dragForce, Time.deltaTime * _speed);
        }
        else
        {
            _allowedMovementY = Vector3.Lerp(_allowedMovementY, Vector3.zero, Time.deltaTime * _dragForce);
            _allowedMovementX = Vector3.Lerp(_allowedMovementX, Vector3.zero, Time.deltaTime * _dragForce);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _transform.forward * _forwardSpeed + _allowedMovementY + _allowedMovementX;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ChangeShapeArea changeShapeArea))
        {
            Destroy(changeShapeArea.gameObject);
        }
    }
}