using UnityEngine;

public class AutoTiling : MonoBehaviour
{
    private Renderer _renderer;
    public float scaleX;
    public float scaleY;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    void Start()
    {
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        _renderer.material.SetTextureScale("_MainTex", new Vector2(scaleX, scaleY));
    }
}