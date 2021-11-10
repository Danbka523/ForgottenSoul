using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] Camera camera;

    private Vector3 mousePosition;

    public float speed = 6f;


    void Update()
    {
        MovePlayer();

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
        TerrainCollider terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>(); ;
        Vector3 worldPosition;
        Ray ray;

        ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (terrainCollider.Raycast(ray, out hitData, 1000))
        {
            worldPosition = hitData.point;
            transform.LookAt(worldPosition);
        }
    }
}
