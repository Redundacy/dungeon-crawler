using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{   
    private NetworkVariable<Vector3> _netPos = new(writePerm: NetworkVariableWritePermission.Owner);
    private NetworkVariable<Quaternion> _netRot = new(writePerm: NetworkVariableWritePermission.Owner);
    // Update is called once per frame
    void Update()
    {
        if(IsOwner){
            _netPos.Value = transform.position;
            _netRot.Value = transform.rotation;
        }else{
            transform.position = _netPos.Value;
            transform.rotation = _netRot.Value;
        }
    }
}
