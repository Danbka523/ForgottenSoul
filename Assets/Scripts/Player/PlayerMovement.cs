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

    private TerrainCollider terrainCollider;
    private CharacterController controller;
    [SerializeField] Camera cam;
    


    private void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
        gravity = new Vector3(0, -10, 0);
        groundDistance = controller.radius;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if (isGrounded)
        {
            MovePlayer();
        }
        else
        controller.Move(gravity * Time.deltaTime);

        ChangeRotation();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
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
