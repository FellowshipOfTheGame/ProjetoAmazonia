using UnityEngine;
using Cinemachine;

public class DragCamera : MonoBehaviour
{

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private CinemachineVirtualCamera camAtual;
    
    private Vector3 dragOrigin;

    [SerializeField]
    private SpriteRenderer tabuleiro;

    private float tabuleiroMinX, tabuleiroMaxX, tabuleiroMinY, tabuleiroMaxY;

    private void Awake(){
        
        tabuleiroMinX = (tabuleiro.transform.position.x - tabuleiro.bounds.size.x) / 2f;
        tabuleiroMaxX = (tabuleiro.transform.position.x + tabuleiro.bounds.size.x) / 2f;

        tabuleiroMinY = (tabuleiro.transform.position.y - tabuleiro.bounds.size.y) / 2f;
        tabuleiroMaxY = (tabuleiro.transform.position.y + tabuleiro.bounds.size.y) / 2f;

    }


    void LateUpdate()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position = ClampCamera(cam.transform.position + difference);
            camAtual.transform.position = cam.transform.position;
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition){

        float camHeight = cam.orthographicSize; 
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = tabuleiroMinX + camWidth;
        float maxX = tabuleiroMaxX - camWidth;
        float minY = tabuleiroMinY + camHeight;
        float maxY = tabuleiroMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float nexY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, nexY, targetPosition.z);

    }
    
}
