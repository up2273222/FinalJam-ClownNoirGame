
using System.Collections;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private bool canBeDragged = true;
    private bool addedtocounter = false;
    private Vector3 offset;
    private Camera mainCamera;
    
    private Vector3 correctPosition;
    private float tolerance = 1f;
    public BoxCollider spawnArea;
    public Transform centerPoint;
    
    void Start()
    {
        mainCamera = Camera.main;
        correctPosition = gameObject.transform.position;
        int seed = System.DateTime.Now.Millisecond * gameObject.GetInstanceID();
        UnityEngine.Random.InitState(seed);
        RandomisePosition();
    }
    void Update()
    {
     
        
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform && canBeDragged)
            {
                isDragging = true;
                offset = transform.position - hit.point;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && canBeDragged)
        {
            MoveObject();
        }
        
        float distToCorrect = Vector3.Distance(transform.position, correctPosition);
        if (distToCorrect < tolerance)
        {
            transform.position = correctPosition;
            canBeDragged = false;
            if (!addedtocounter)
            {
                GameManager.Instance.checkPhotoState();
                addedtocounter = true;
            }
            
        }
        
    }
    private void MoveObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            transform.position = ray.GetPoint(distance) + offset;
        }
    }

    private void RandomisePosition()
    {
        Vector3 spawnSize = spawnArea.bounds.size;
        Vector3 randPos = new Vector3(
            Random.Range(-spawnSize.x / 2, spawnSize.x / 2),
            -spawnSize.y / 2,
            Random.Range(-spawnSize.z / 2, spawnSize.z / 2));
        
        Vector3 worldPos = randPos + centerPoint.position;
       // transform.position = new Vector3(worldPos.x, gameObject.transform.position.y, worldPos.z);
        transform.position = worldPos;
        
    }

    
    
}
