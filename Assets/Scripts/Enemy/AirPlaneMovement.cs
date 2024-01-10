
using UnityEngine;

public class AirPlaneMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    private void Update()
    {
        transform.Translate(Vector3.up * Mathf.PingPong(_minValue, _maxValue) * 
            Time.deltaTime * _speed);
    }
}
