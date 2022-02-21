using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private Axis _axis;

    private Vector3 _direction;

    private void Start()
    {
        switch (_axis)
        {
            case Axis.X:
                _direction = Vector3.right;
                break;
            case Axis.Y:
                _direction = Vector3.up;
                break;
            case Axis.Z:
                _direction = Vector3.forward;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        Vector3 deltaRotation = _direction * _speed * Time.deltaTime;
        transform.Rotate(deltaRotation);
    }
}

public enum Axis
{
    X,
    Y,
    Z
}