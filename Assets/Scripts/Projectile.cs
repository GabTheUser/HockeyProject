using UnityEngine;
using Photon.Pun;
public class Projectile : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    public float m_Speed, timer;
    public bool stop, hittedSomething;
    [SerializeField]
    private Transform shaibaRotation;
    public bool timerGo = true;

    private OVRGrabber myGrabber;
    private OVRGrabbable grabbable;
    private PhotonView pview;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        //tempVector = Vector3.Distance(transform.position, point.position);
        m_Speed = FindObjectOfType<SpeedSaver>().speed;
        m_Rigidbody.AddForce(transform.forward * m_Speed, ForceMode.Impulse);
       
        grabbable = GetComponent<OVRGrabbable>();
        pview = GetComponent<PhotonView>();
    }


    private void FixedUpdate()
    {
        /*if (stop == false)
        {
            m_Rigidbody.velocity = m_Speed * Time.fixedDeltaTime * transform.forward;
        }

        timerdist += Time.deltaTime;

        float newDistance = Vector3.Distance(transform.position, point.position);
        if(transform.position.x >= 15 && stop == false)
        {
            stop = true;
            Debug.LogError("time: " + timerdist);
        }*/

        if (timerGo)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            stop = true;
        }
    }


    [PunRPC]
    public void HitConfirmForAll()
    {
        hittedSomething = true;
    }

    public void HitConfirmCaller()
    {
        if (pview.IsMine)
        {
            pview.RPC("HitConfirmForAll", RpcTarget.All);
        }
    }
}
