using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;
    private float groundDistance;
    private Vector3 worldPosition;
    private Ray ray;
    private RaycastHit hitData;
    public bool isGrounded;
    [SerializeField] float speed;
    private Vector3 gravity;
    public bool isPaused;

    private TerrainCollider terrainCollider;
    private CharacterController controller;
    [SerializeField]
    private GameObject flashLight;
    [SerializeField] Camera cam;
    [SerializeField] AudioClip foot_steps;
    [SerializeField] AudioSource audio;


    private Animator animator; //
    private bool isSteping;

    private void Start()
    {
        animator = GetComponent<Animator>(); //

        controller = transform.GetComponent<CharacterController>();
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
        gravity = new Vector3(0, -8, 0);
        groundDistance = controller.skinWidth;
    }

    void Update()
    {
        if (!isPaused)
        {
            isGrounded = Physics.CheckSphere(transform.position, groundDistance*2, groundMask);

            if (isGrounded)
            {
                MovePlayer();
            }
            else
                controller.Move(gravity * Time.deltaTime);

            ChangeRotation();
            if (Input.GetKeyDown(KeyCode.L))
                if (flashLight.active)
                    flashLight.SetActive(false);
                else
                    flashLight.SetActive(true);
        }
        else 
            animator.SetFloat("Move", 0.25f); 

    }


    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1f)
        {
            if (!audio.isPlaying)
                audio.PlayOneShot(foot_steps);
            if (System.Math.Sign(transform.forward.x) == System.Math.Sign(direction.x) || System.Math.Sign(transform.forward.z) == System.Math.Sign(direction.z)) //
                animator.SetFloat("Move", 0.5f); //
            else if (System.Math.Sign(transform.forward.x) != System.Math.Sign(direction.x) || System.Math.Sign(transform.forward.z) != System.Math.Sign(direction.z)) //
                animator.SetFloat("Move", 0f); //

            controller.Move(direction * speed * Time.deltaTime);
        }
        else
        {
            audio.Stop();
            animator.SetFloat("Move", 0.25f); //
        }

    }

    void ChangeRotation()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (terrainCollider.Raycast(ray, out hitData, 1000))
        {
            worldPosition = hitData.point;
            worldPosition.y = transform.position.y;
            transform.LookAt(worldPosition, Vector3.up);
        }
    }

  

}
