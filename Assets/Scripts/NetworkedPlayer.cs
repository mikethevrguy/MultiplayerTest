using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkedPlayer : MonoBehaviourPunCallbacks
{
    public GameObject head;
    public GameObject rightHand;
    public GameObject leftHand;
    public Transform playerGlobal;
    public Transform playerLocal;
    public Transform leftHandTransform;
    public Transform rightHandTransform;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            playerGlobal = GameObject.Find("Player").transform;
            playerLocal = playerGlobal.Find("SteamVRObjects/VRCamera").transform;
           leftHandTransform = playerGlobal.Find("SteamVRObjects/LeftHand").transform;
            rightHandTransform = playerGlobal.Find("SteamVRObjects/RightHand").transform;


            this.transform.SetParent(playerLocal);
            this.transform.localPosition = Vector3.zero;
            rightHand.transform.SetParent(rightHandTransform);
            rightHand.transform.localPosition = Vector3.zero;
            leftHand.transform.SetParent(leftHandTransform, false);
            leftHand.transform.localPosition = Vector3.zero;
            
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);
            stream.SendNext(leftHandTransform.localPosition);
            stream.SendNext(leftHandTransform.localRotation);
            stream.SendNext(rightHandTransform.localPosition);
            stream.SendNext(rightHandTransform.localRotation);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            head.transform.position = (Vector3)stream.ReceiveNext();
            head.transform.rotation = (Quaternion)stream.ReceiveNext();
            rightHand.transform.position = (Vector3)stream.ReceiveNext();
            leftHand.transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
