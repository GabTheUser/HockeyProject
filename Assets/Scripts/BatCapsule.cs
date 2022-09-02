using UnityEngine;
using Photon.Pun;
public class BatCapsule : MonoBehaviour
{
	[SerializeField]
	private CapsFollower _batCapsuleFollowerPrefab;
	[SerializeField]
	private SwitchHands switchHands;
	[SerializeField]
	private CloseTrapPlayer closeTrap;
	[SerializeField]
	private bool right;
	[SerializeField]
	private PhotonView pView;
	
	private void SpawnBatCapsuleFollower()
	{
		CapsFollower follower = Instantiate(_batCapsuleFollowerPrefab, transform.position, transform.rotation);
		
        if (right)
        {
			switchHands.hitBoxTransform[0] = follower.transform;
        }
        else
        {
			pView.RPC("PassCollider", RpcTarget.All);
			//pView.GetComponent<CloseTrapPlayer>().PassCollider();
			switchHands.hitBoxTransform[1] = follower.transform;
		}
		follower.transform.position = transform.position;
		follower.SetFollowTarget(this);
	}

	private void Start()
	{
		SpawnBatCapsuleFollower();
	}
}
