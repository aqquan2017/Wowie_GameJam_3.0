using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCinemachineTrack : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.Follow = gameManager.target.transform;
    }
}
