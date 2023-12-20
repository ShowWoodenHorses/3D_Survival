using UnityEngine;

public class CheckTriggerWeaponAndObstacle : MonoBehaviour
{
    public bool isWeaponInObstacle;
    private bool _check;

    private void Update()
    {
        if (_check)
            isWeaponInObstacle = false;
        if (!_check)
            isWeaponInObstacle = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            _check = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            _check = false;
        }
    }
}
