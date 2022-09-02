using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;

public class CameraRay : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform aim;
    [SerializeField] private ProgressBar progressBar;
    public bool hitButton;

    public TMP_InputField joinField;
    public KeyBoardUI keyBoardUI;
    public CreateAndJoin createAndJoin;

    [SerializeField] private float maxTimeToWait;
    private float curTimeToWait;

    private void Update()
    {
        Training();
    }

    private void SoloPlay()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hitButton == false)
            {
                if (hit.collider.GetComponent<ButtonAction>())
                {
                    hitButton = true;
                }
            }
        }
        else
        {
            hitButton = false;
        }

        if (hitButton == true)
        {
            if (curTimeToWait < maxTimeToWait)
            {
                curTimeToWait += Time.deltaTime;
                progressBar.current = curTimeToWait;
            }
            else
            {
                //hit.collider.gameObject.GetComponent<ButtonAction>().InVokeTheEvent();
                curTimeToWait = 0;
                progressBar.current = 0;
                hitButton = false;
            }
        }
        else
        {
            curTimeToWait = 0;
            progressBar.current = 0;
        }
    }

    private void Training()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Button>() && hit.collider.CompareTag("KeyB"))
                {
                    if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetMouseButtonDown(0))
                    {
                        joinField.text += hit.collider.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
                    }
                }

                if (hit.collider.CompareTag("Untagged"))
                {
                    if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetMouseButtonDown(0))
                    {
                        if (hit.collider.GetComponent<ExitScript>()) hit.collider.GetComponent<ExitScript>().ExitApp();
                    }
                }

                if (hit.collider.CompareTag("JoinB") && hit.collider.GetComponent<Button>())
                {
                    if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetMouseButtonDown(0))
                    {
                        createAndJoin.JoinRoom();
                    }
                }

                if (hit.collider.GetComponent<RoomListing>())
                {
                    if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetMouseButtonDown(0))
                    {
                        hit.collider.GetComponent<RoomListing>().JoinRoom();
                    }
                }

                if (hit.collider.CompareTag("DeleteK"))
                {
                    if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetMouseButtonDown(0))
                    {
                        joinField.text = joinField.text.Remove(joinField.text.Length - 1);
                    }
                }

                if (hit.collider.CompareTag("updatek"))
                {
                    if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetMouseButtonDown(0))
                    {
                        createAndJoin.JoinLobbyOnClick();
                    }
                }
            }
        }
    }

}
