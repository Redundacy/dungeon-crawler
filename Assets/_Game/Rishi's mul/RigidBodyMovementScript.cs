using UnityEngine;
using Unity.Netcode;
public class RigidBodyMovementScript : NetworkBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;


    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Jumpforce;
    [SerializeField] private float DodgeRollCooldownOriginal = 50;
    [SerializeField] private float DodgeRollTimerOriginal = 50;
    [SerializeField] private float Dodgeforce;
    [SerializeField] private bool isDodgeRolling = false;
    private float DodgeRollTimer;
    private float DodgeRollCooldown;


    private void Start()
    {
    DodgeRollTimer = DodgeRollTimerOriginal;
    DodgeRollCooldown = DodgeRollCooldownOriginal;
    }




    private void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed; 
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);


        //JUMP
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if (Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask)) 
            {
                PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            }
        }

      //DODGE
        if ((Input.GetKeyDown("f") || isDodgeRolling) && DodgeRollTimer >= 0 && DodgeRollCooldown <= 0) 
        {
            PlayerBody.AddForce(transform.forward * Dodgeforce, ForceMode.Force);
            isDodgeRolling = true;
            DodgeRollTimer -= 1;
                
        } 
        else if ((Input.GetKeyDown("f") || isDodgeRolling) && DodgeRollTimer < 0) 
        {
            isDodgeRolling = false;
            DodgeRollTimer = DodgeRollTimerOriginal;
            DodgeRollCooldown = DodgeRollCooldownOriginal;

        }
        else
        {
            DodgeRollCooldown --;
        }

    }

    private void MovePlayerCamera() 
    {
        xRot -= PlayerMouseInput.y * Sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * 2 * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

    }

    private void FixedUpdate()
    {
        MovePlayer();
        MovePlayerCamera();
    }

    public override void OnNetworkSpawn() {
        if (!IsOwner) Destroy(this);
    }
}
