using UnityEngine;

public class RotateTransformBullet : MonoBehaviour
{
    public Transform Player;
    private void Update()
    {
        transform.position = Player.position;
        transform.rotation = Player.rotation;
    }
}
