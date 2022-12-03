using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MeleeAttackScript : NetworkBehaviour
{
    [SerializeField] private float SwingCooldownOriginal = 20;
    [SerializeField] private float SwingTimerOriginal = 1;
    [SerializeField] private float SwingSpeed = 0.5f;
    [SerializeField] private int isSwinging = 0;
    GameObject Blade;
    private float SwingCooldown;
    private float SwingTimer;
    private bool collided;

    void Start()
    {
        SwingCooldown = SwingCooldownOriginal;
        SwingTimer = SwingTimerOriginal;
        Blade = transform.Find("weapon").gameObject;
    }

    private void Swing()
    {
        if ((Input.GetKey(KeyCode.Mouse0) || isSwinging == 1) && SwingTimer >= 0 && SwingCooldown <= 0)
        {
        //SWING
        Blade.transform.Rotate(SwingSpeed*0.1f, 0.0f, 0.0f, Space.Self);
        
        isSwinging = 1;
        
        SwingTimer --;
        }
        else if ((Input.GetKey(KeyCode.Mouse0) || isSwinging == 1) && SwingTimer < 0) 
        {
        isSwinging = 0;
        SwingTimer = SwingTimerOriginal;
        SwingCooldown = SwingCooldownOriginal;
        }
        else if (isSwinging == 0 && Blade.transform.localEulerAngles.x >= SwingSpeed * 0.1f) 
        {
            UnSwing();
        }
        else
        {
            if (SwingCooldown > 0) { SwingCooldown--; }
        }
    }

    private void UnSwing()
    {
        Blade.transform.Rotate(SwingSpeed*-0.1f, 0.0f, 0.0f, Space.Self);
    }

    void OnCollisionEnter (Collision co)
    {
        if(isSwinging == 1 && co.gameObject.tag == "Enemy" && co.gameObject.tag != "Player")
        {
            Destroy (co.gameObject);
        }
    }


    void FixedUpdate()
    {
        if (!IsOwner) return;
        RequestFireServerRpc();
        Swing();
    }

    [ServerRpc]
    private void RequestFireServerRpc() {
        FireClientRpc();
    }

    [ClientRpc]
    private void FireClientRpc() {
        if (!IsOwner) Swing();
    }
}
