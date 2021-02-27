using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuanTest : MonoBehaviour
{
    

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CameraCinemachineShake.Instance.SetShake(50f, 0.7f);
        }    
    }
}
