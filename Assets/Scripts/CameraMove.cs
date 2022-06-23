using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{

    [SerializeField]
    public CinemachineVirtualCamera p1Cam;
    [SerializeField]
    public CinemachineVirtualCamera p2Cam;
    [SerializeField]
    public CinemachineVirtualCamera p3Cam;
    [SerializeField]
    private CinemachineVirtualCamera mapCam;

    private static GameObject canvas;
    private int jogadorDaVez = 0;
    private bool visualizarMapa;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
    }

    // Start is called before the first frame update
    void Start()
    {
        jogadorDaVez = canvas.GetComponent<Dado>().jogador;
        visualizarMapa = false;
    }

    public void SwitchCamera()
    {

        jogadorDaVez = canvas.GetComponent<Dado>().jogador;

        if (!visualizarMapa)
        {

            mapCam.Priority = 0;

            switch (jogadorDaVez)
            {
                case 0:
                    p1Cam.Priority = 3;
                    p2Cam.Priority = 2;
                    p3Cam.Priority = 1;
                    break;
                case 1:
                    p1Cam.Priority = 1;
                    p2Cam.Priority = 3;
                    p3Cam.Priority = 2;
                    break;
                case 2:
                    p1Cam.Priority = 2;
                    p2Cam.Priority = 1;
                    p3Cam.Priority = 3;
                    break;
            }
        }
        else
        {
            mapCam.Priority = 10;
        }

    }

    public void ControlMapCamera()
    {
        visualizarMapa = !visualizarMapa;
        SwitchCamera();
    }

}