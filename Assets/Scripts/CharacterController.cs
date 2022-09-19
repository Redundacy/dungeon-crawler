using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterController : NetworkBehaviour
{
    public float forceModifier = 2;
    public float jumpForce = 2;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * forceModifier, 0, Input.GetAxis("Vertical")) * forceModifier, ForceMode.Force);
        }
        if(Input.GetKeyDown("space"))
        {
            _rb.AddForce(new Vector3(0, 1f * jumpForce, 0), ForceMode.Impulse);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }
}
