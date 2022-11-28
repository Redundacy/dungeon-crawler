using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackScript : MonoBehaviour
{
    [SerializeField] private float SwingCooldownOriginal = 20;
    [SerializeField] private float SwingTimerOriginal = 1;
    [SerializeField] private float SwingSpeed = 0.5f;
    [SerializeField] private bool isSwinging = false;
    GameObject Blade;
    private float SwingCooldown;
    private float SwingTimer;
    private bool collided;

    void Start()
    {
        SwingCooldown = SwingCooldownOriginal;
        SwingTimer = SwingTimerOriginal;
        Blade = GameObject.Find("Blade");
    
    }


    private void Swing()
    {
        if ((Input.GetKeyDown("q") || isSwinging ) && SwingTimer >= 0 && SwingCooldown <= 0)
        {
        //SWING
        Blade.transform.Rotate(SwingSpeed*0.1f, 0.0f, 0.0f, Space.Self);
        isSwinging = true;
        SwingTimer --;
        }
        else if ((Input.GetKeyDown("q") || isSwinging) && SwingTimer < 0) 
        {
        isSwinging = false;
        SwingTimer = SwingTimerOriginal;
        SwingCooldown = SwingCooldownOriginal;
        }
        else if (isSwinging == false && Blade.transform.localEulerAngles.x > 0) 
        {
            UnSwing();
        }
        else
        {
            SwingCooldown --;
        }
    }

    private void UnSwing()
    {
        Blade.transform.Rotate(SwingSpeed*-0.1f, 0.0f, 0.0f, Space.Self);
    }

    void OnCollisionEnter (Collision co)
    {
        if(isSwinging && co.gameObject.tag == "Enemy" && co.gameObject.tag != "Player")
        {
            Destroy (co.gameObject);
        }
    }


    void FixedUpdate()
    {
        Swing();
    }
}
