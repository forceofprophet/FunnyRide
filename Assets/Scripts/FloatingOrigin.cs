using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FloatingOrigin : MonoBehaviour 
{
    public float threshold = 150.0f;
    void LateUpdate()
    {
        Vector3 cameraPosition = gameObject.transform.position;
        cameraPosition.y = 0f;
        if (cameraPosition.magnitude > threshold)  // когда игрок достигает позиции 150 по оси Z - обнуляется его позиция по это оси
        {
            for (int z = 0; z < SceneManager.sceneCount; z++)
            {
                foreach (GameObject g in SceneManager.GetSceneAt(z).GetRootGameObjects())
                {
                    g.transform.position -= cameraPosition;
                }
            }
            Vector3 originDelta = Vector3.zero - cameraPosition;
        }
    }
}

