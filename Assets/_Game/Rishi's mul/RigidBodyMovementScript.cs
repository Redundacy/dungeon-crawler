using Cinemachine;
using UnityEngine;
using Unity.Netcode;

public class RigidBodyMovementScript : NetworkBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;


    private CharacterController _charController;




    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private CinemachineFreeLook PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] public float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Jumpforce;
    [SerializeField] private float DodgeRollCooldownOriginal = 50;
    [SerializeField] private float DodgeRollTimerOriginal = 50;
    [SerializeField] private float Dodgeforce;
    [SerializeField] private bool isDodgeRolling = false;
    private float DodgeRollTimer;
    private float DodgeRollCooldown;
    public float rotationSpeed;

    public Animator animator;
    private void Start()
    {
        DodgeRollTimer = DodgeRollTimerOriginal;
        DodgeRollCooldown = DodgeRollCooldownOriginal;
    }


    private void Update()
    {
        if (!IsOwner)
        {
            Destroy(this);
        }
        UpdateMovePlayer();

    }

    
    [ServerRpc]
    private void RequestFireServerRpc() {
        FireClientRpc();
    }

    [ClientRpc]
    private void FireClientRpc() {
        if (!IsOwner) MovePlayer();
    }

    private void UpdateMovePlayer(){
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {   

        var velocity = (PlayerMovementInput) * Speed;
        Vector3 MoveVector = transform.TransformDirection(velocity);
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
        animator.SetFloat("Speed", velocity.magnitude);

        //JUMP
        if (Input.GetKey(KeyCode.Space))
        {
            if (Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask))
            {
                PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            }
        }

        //DODGE
        if ((Input.GetKey("f") || isDodgeRolling) && DodgeRollTimer >= 0 && DodgeRollCooldown <= 0)
        {
            PlayerBody.AddForce(transform.forward * Dodgeforce, ForceMode.Force);
            isDodgeRolling = true;
            DodgeRollTimer -= 1;

        }
        else if ((Input.GetKey("f") || isDodgeRolling) && DodgeRollTimer < 0)
        {
            isDodgeRolling = false;
            DodgeRollTimer = DodgeRollTimerOriginal;
            DodgeRollCooldown = DodgeRollCooldownOriginal;
        }
        else
        {
            DodgeRollCooldown--;
        }

    }


    private void FixedUpdate()
    {   
        
        MovePlayer();
        transform.localEulerAngles = new Vector3(0f, PlayerCamera.m_XAxis.Value, 0f);
    }

}


