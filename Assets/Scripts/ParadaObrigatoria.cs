using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ParadaObrigatoria : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] private Button backButton;
    private int jogador;

    private void Start()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
    }

    private void Update()
    {
        if ((ulong) videoPlayer.frame == videoPlayer.frameCount - 1) // o video acabou
        {
            backButton.interactable = true;
        }
    }

    private void OnEnable()
    {
        
    }
}
