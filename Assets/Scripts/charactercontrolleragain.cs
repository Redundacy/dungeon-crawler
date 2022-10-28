using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontrolleragain : MonoBehaviour
{
    private Rigidbody _rb;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //transform.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);
        _rb.velocity = new Vector3(Input.GetAxis("Horizontal"), _rb.velocity.y, Input.GetAxis("Vertical"));
    }
}
