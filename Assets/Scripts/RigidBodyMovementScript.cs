using UnityEngine;

public class RigidBodyMovementScript : MonoBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;


    private CharacterController _charController;




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
    public float rotationSpeed;


    private void Start()
    {
    DodgeRollTimer = DodgeRollTimerOriginal;
    DodgeRollCooldown = DodgeRollCooldownOriginal;
    }




    private void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        PlayerMovementInput.Normalize();

        // Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed; 
        // PlayerBody.velocity = new Vector3(MoveVector.x,0f,MoveVector.z);

        // transform.Translate(PlayerMovementInput * Speed * Time.deltaTime, Space.World);


        
        if (PlayerMovementInput != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(PlayerMovementInput, Vector3.up);
            if (Input.GetKey("w")) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed; 
            PlayerBody.velocity = new Vector3(0f,0f,MoveVector.z);    
            } 
            else if (Input.GetKey("s")) 
            {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed; 
            PlayerBody.velocity = new Vector3(0f,0f,-MoveVector.z);    
            }   
            else if (Input.GetKey("a")) 
            {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed; 
            PlayerBody.velocity = new Vector3(-Speed,0f,0f);    
            }     
            else if (Input.GetKey("d")) 
            {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed; 
            PlayerBody.velocity = new Vector3(Speed,0f,0f);    
            } 
        }


    }

    private void MovePlayer()
    {
        


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

  
    


    private void FixedUpdate()
    {
        MovePlayer();
    }
}
