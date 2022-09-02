/*This script must be on player object
 * its used to switched hands
 */
using UnityEngine;
using Photon.Pun;
public class SwitchHands : MonoBehaviour
{
    public OVRGrabber ovrGraber;
    [Header("right, left")]
    public Transform[] hitBoxTransform;
    [SerializeField]
    private Transform takeVarsLeft, takeVarsRight;
    [SerializeField]
    private Transform defVarsLeft, defVarsRight;
    [SerializeField]
    private Transform rightHand, leftHand;
    [SerializeField]
    private Transform parentLeft, parentRight;

    public bool switched = false;
    private PhotonView pView;

    [SerializeField]
    private Vector3[] hitBoxesStartPos, hitBoxesStartScale;
    [SerializeField]
    private Quaternion[] hitBoxesStartRot;

    private void Start()
    {
        pView = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.B))
        {
              pView.RPC("SwitchHandsNow", RpcTarget.All);
              //SwitchHandsNow();
        }
    }
    
    [PunRPC]
    public void SwitchHandsNow()
    {
        switched = !switched;
        if (switched)
        {
            ovrGraber.m_controller = OVRInput.Controller.RTouch;
            rightHand.parent = parentLeft;
            rightHand.SetPositionAndRotation(takeVarsRight.position, takeVarsRight.rotation);
            hitBoxTransform[0].SetPositionAndRotation(takeVarsRight.position, takeVarsRight.rotation);
            rightHand.localScale = new Vector3(takeVarsRight.localScale.x, takeVarsRight.localScale.y, takeVarsRight.localScale.z);
            hitBoxTransform[0].localScale = new Vector3(-hitBoxTransform[0].localScale.x, hitBoxTransform[0].localScale.y, hitBoxTransform[0].localScale.z);

            leftHand.parent = parentRight;
            leftHand.SetPositionAndRotation(takeVarsLeft.position, takeVarsLeft.rotation);
            hitBoxTransform[1].SetPositionAndRotation(takeVarsLeft.position, takeVarsLeft.rotation);
            leftHand.transform.localScale = new Vector3(takeVarsLeft.localScale.x, takeVarsLeft.localScale.y, takeVarsLeft.localScale.z);
            hitBoxTransform[1].localScale = new Vector3(-hitBoxTransform[1].localScale.x, hitBoxTransform[1].localScale.y, hitBoxTransform[1].localScale.z);
        }
        else
        {
            ovrGraber.m_controller = OVRInput.Controller.LTouch;
            rightHand.parent = parentRight;
            rightHand.SetPositionAndRotation(defVarsRight.position, defVarsRight.rotation);
            hitBoxTransform[0].SetPositionAndRotation(defVarsRight.position, defVarsRight.rotation);
            rightHand.localScale = new Vector3(defVarsRight.localScale.x, defVarsRight.localScale.y, defVarsRight.localScale.z);
            hitBoxTransform[0].localScale = new Vector3(-hitBoxTransform[0].localScale.x, hitBoxTransform[0].localScale.y, hitBoxTransform[0].localScale.z);

            leftHand.parent = parentLeft;
            leftHand.SetPositionAndRotation(defVarsLeft.position, defVarsLeft.rotation);
            hitBoxTransform[1].SetPositionAndRotation(defVarsLeft.position, defVarsLeft.rotation);
            leftHand.localScale = new Vector3(defVarsLeft.localScale.x, defVarsLeft.localScale.y, defVarsLeft.localScale.z);
            hitBoxTransform[1].localScale = new Vector3(-hitBoxTransform[1].localScale.x, hitBoxTransform[1].localScale.y, hitBoxTransform[1].localScale.z);
        }
    }
}
