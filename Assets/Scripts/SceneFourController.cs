using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class SceneFourController : MonoBehaviour
{
    public List<int> myInts;
    public Scenarios scene4;

    [PunRPC]
    public void GetMyInt(int myIntu)
    {
        myInts.Add(myIntu);
    }

    [PunRPC]
    public void RemoveMyInt(int myIntu)
    {
        myInts.Remove(myIntu);
    }

    [PunRPC]
    public void ActivateChars4()
    {
        scene4 = FindObjectOfType<Scenarios>();
        for (int i = 0; i < myInts.Count; i++)
        {
            scene4.characterToOn[myInts[i]].SetActive(true);
        }
        myInts.Clear();
        scene4.enabled = true;
    }
}
