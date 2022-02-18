using UnityEngine;
using Cinemachine;

public class DragCamera : MonoBehaviour
{

    public Camera cam;
    public CinemachineVirtualCamera camAtual;
    
    private Vector3 dragOrigin;

    // Update is called once per frame
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

            cam.transform.position += difference;
            camAtual.transform.position = cam.transform.position;
        }
    }
}
