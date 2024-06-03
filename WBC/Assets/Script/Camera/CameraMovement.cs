using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraMovement : MainCamera
{
    [Header("Movement")]
    [SerializeField] private int moveSpeed;

    [Header("Zoom")]
    [SerializeField] private int minZoomSize;
    [SerializeField] private int maxZoomSize;
    [SerializeField] private int zoomSpeed;

    [Header("MapContainer")]
    [SerializeField] private MapContainer mapContainer;

    private Camera mainCamera;

    private void Start()
    {
        SetMainCamera();
    }

    private void Update()
    {
        ScaleControl();

        Movement();
        MovementRestrictions();
    }
    public override void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);

        Vector2 moveVector = moveDirection * moveSpeed * Time.deltaTime;

        transform.Translate(moveVector); 
    }

    public override void ScaleControl()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float newSize = mainCamera.orthographicSize - scroll * zoomSpeed;

        newSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);

        Vector3 offset = mousePosition - mainCamera.transform.position;
        mainCamera.transform.position += offset * (1 - newSize / mainCamera.orthographicSize);

        mainCamera.orthographicSize = newSize;
    }

    public override void MovementRestrictions()
    {
        float cameraSizeY = mainCamera.orthographicSize;
        float cameraSizeX = cameraSizeY * mainCamera.aspect;

        if(mapContainer.Terrain == null)
            return;

        float minX = cameraSizeX - 0.5f;
        float maxX = mapContainer.Terrain.GetLength(0) - cameraSizeX - 0.5f;
        float minY = cameraSizeY - 0.5f;
        float maxY = mapContainer.Terrain.GetLength(1) - cameraSizeY - 0.5f;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        transform.position = clampedPosition;
    }

    public override void SetMainCamera()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}