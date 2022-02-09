using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float height = 0.05f;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        transform.Rotate(1.5f, 0, 0);
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;         //сгенерировать новую позицию по оси Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);  //установить новую позицию монетке
    }
}
