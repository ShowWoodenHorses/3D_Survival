using UnityEngine;
using UnityEngine.Rendering;

public class SetMaterialToInvisible : MonoBehaviour 
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {

        }
        
    }
}
