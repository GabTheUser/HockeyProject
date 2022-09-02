using UnityEngine;

public class CapsFollower : MonoBehaviour
{
	private BatCapsule _batFollower;
	private Rigidbody _rigidbody;
	private Vector3 _velocity;

	[SerializeField]
	private float _sensitivity = 100f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void LateUpdate()
	{
		if (_batFollower != null)
		{

			//Vector3 destination = _batFollower.transform.position;
			//_velocity = (destination - _rigidbody.transform.position) * _sensitivity;
			_rigidbody.MovePosition(_batFollower.transform.position/* + _velocity * Time.deltaTime*/);
			_rigidbody.MoveRotation(_batFollower.transform.rotation);

			//_velocity = (destination - _rigidbody.transform.position) * _sensitivity;

			//_rigidbody.velocity = _velocity;
			//transform.rotation = _batFollower.transform.rotation;

			

		}
	}

	public void SetFollowTarget(BatCapsule batFollower)
	{
		_batFollower = batFollower;
	}
}
