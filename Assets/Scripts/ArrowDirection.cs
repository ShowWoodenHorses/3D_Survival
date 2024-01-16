using UnityEngine;
using UnityEngine.UI;

public class ArrowDirection : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _arrowTransform;
    private Transform TriggerArrow;
    private Camera _camera;

    private void Start()
    {
        TriggerArrow = this.transform;
        _camera = Camera.main;
    }

    public void Initialize(Transform player)
    {
        _playerTransform = player;

    }

    private void Update()
    {
        Vector3 direction = TriggerArrow.position - _playerTransform.position;
        Ray ray = new Ray(_playerTransform.position, direction);
        Debug.DrawRay(_playerTransform.position, direction);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        float minDistance = Mathf.Infinity;
        int planeIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                    planeIndex = i;
                }
            }
        }

        minDistance = Mathf.Clamp(minDistance, 0f, direction.magnitude);
        Vector3 worldPoint = ray.GetPoint(minDistance);
        _arrowTransform.position = _camera.WorldToScreenPoint( worldPoint);
        _arrowTransform.rotation = GetRotation(planeIndex);
    }

    private Quaternion GetRotation(int index)
    {
        if (index == 0)
            return Quaternion.Euler(0f, 0f, 90f);
        else if (index == 1)
            return Quaternion.Euler(0f, 0f, -90f);
        else if (index == 2)
            return Quaternion.Euler(0f, 0f, 180f);
        else if (index == 3)
            return Quaternion.Euler(0f, 0f, 0f);
        else 
            return Quaternion.identity;
    }
}
