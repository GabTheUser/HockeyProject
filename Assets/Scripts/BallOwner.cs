using UnityEngine;

public class BallOwner : MonoBehaviour
{
    public Transform ballTransform;
    public Transform whoToFollow;

    public Transform whereToPass;
    public bool dontHoldBall;
    private void Update()
    {
        if (dontHoldBall == false)
        {
            if (whoToFollow != null)
                ballTransform.position = whoToFollow.position;
        }
    }
}
