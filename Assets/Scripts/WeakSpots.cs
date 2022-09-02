using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpots : MonoBehaviour
{
    public List<Vector3> weakSpotsPos = new List<Vector3>();
    public List<Vector3> weakSpotsPosUI = new List<Vector3>();
    [SerializeField]
    private GameObject missOne;
    [SerializeField]
    private Transform closeOne;
    [SerializeField]
    private Transform parentOfIndicators;
    [SerializeField]
    private PhotonView pView;
    [SerializeField]
    private GameObject posss;

    [SerializeField]
    private GameObject[] scoreDisplayers;
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Vector3 hitPoint = GetComponent<BoxCollider>().ClosestPoint(other.gameObject.transform.position);
                //GameObject newWeakPoint = Instantiate(posss, hitPoint, Quaternion.identity);

                weakSpotsPos.Add(hitPoint);
                weakSpotsPosUI.Add(closeOne.InverseTransformPoint(hitPoint));
                //GameObject spawnedOne = Instantiate(missOne, parentOfIndicators, false);
                GameObject spawnedOne = PhotonNetwork.Instantiate(missOne.name, parentOfIndicators.position, Quaternion.Euler(0, 0, 0));
                spawnedOne.transform.parent = parentOfIndicators.transform;
                spawnedOne.transform.localPosition = new Vector2(weakSpotsPosUI[weakSpotsPosUI.Count - 1].x, weakSpotsPosUI[weakSpotsPosUI.Count - 1].y);
                spawnedOne.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            //savedWeakPoints.Add(newWeakPoint);
        }
    }*/

    [PunRPC]
    public void MissedOneAdd(Vector3 tempPos)
    {
        Vector3 hitPoint = GetComponent<BoxCollider>().ClosestPoint(tempPos);

        weakSpotsPos.Add(hitPoint);
        weakSpotsPosUI.Add(closeOne.InverseTransformPoint(hitPoint));
        GameObject spawnedOne = PhotonNetwork.Instantiate(missOne.name, parentOfIndicators.position, Quaternion.Euler(0, 0, 0));
        spawnedOne.transform.parent = parentOfIndicators.transform;
        spawnedOne.transform.localPosition = new Vector2(weakSpotsPosUI[weakSpotsPosUI.Count - 1].x, weakSpotsPosUI[weakSpotsPosUI.Count - 1].y);
        spawnedOne.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    [PunRPC]
    public void ClearWeakSpots()
    {
        weakSpotsPos.Clear();
        weakSpotsPosUI.Clear();
        scoreDisplayers[0].SetActive(false);
        scoreDisplayers[1].SetActive(false);
        foreach (Transform child in parentOfIndicators.transform)
        {
            Destroy(child.gameObject);
        }
    }

    [PunRPC]
    private void ActivateWSObj(bool curState)
    {
        parentOfIndicators.gameObject.SetActive(curState);
        scoreDisplayers[0].SetActive(true);
        scoreDisplayers[1].SetActive(true);
    }
}
