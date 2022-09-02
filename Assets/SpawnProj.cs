using UnityEngine;

public class SpawnProj : MonoBehaviour
{
    public GameObject lol;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Instantiate(lol, transform.position, transform.rotation);;
    }
}
