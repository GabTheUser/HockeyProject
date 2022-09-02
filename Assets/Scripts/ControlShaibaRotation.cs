using UnityEngine;

public class ControlShaibaRotation : MonoBehaviour
{
    private Transform parentTransform;
    private void Start()
    {
        parentTransform = GetComponentInParent<Transform>();
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x - parentTransform.eulerAngles.x, transform.eulerAngles.y - parentTransform.eulerAngles.y, transform.eulerAngles.z - parentTransform.eulerAngles.z);
    }
}
