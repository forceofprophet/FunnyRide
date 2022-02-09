using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollScript : MonoBehaviour
{
    [SerializeField] GameObject riderRig;
    [SerializeField] GameObject unicycleRig;
    [SerializeField] Animator riderAnim;
    [SerializeField] Animator unicycleAnim;
    [SerializeField] WheelCollider wheelCollider;
    [SerializeField] MeshCollider[] unicycleColliders;
    [SerializeField] Rigidbody unicycleRB;
    [SerializeField] Stopwatch stopwatch;
    Collider[] ragDollColliders;
    Rigidbody[] limbsRiderRigigidbodies;
    bool isRagDoll = false;
    void Start()
    {
        ragDollColliders = riderRig.GetComponentsInChildren<Collider>();
        limbsRiderRigigidbodies = riderRig.GetComponentsInChildren<Rigidbody>();
        RagdollModeOff();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RagdollModeOn();
        }
    }
    void FixedUpdate()
    {
        if(isRagDoll == true)
        {
            foreach (Rigidbody rigid in limbsRiderRigigidbodies)
            {
                rigid.AddForce(Vector3.forward * 25f);          // ����� ��������� �������� ��������� ����, ����������� �� ������ ��������� ���������, �� ����������� Z+
            }
        }
    }
    public void RagdollModeOn()                                 //���������� �������
    {
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rigid in limbsRiderRigigidbodies)
        {
            rigid.isKinematic = false;
        }
        foreach (Collider col in unicycleColliders)
        {
            col.enabled = true;
        }
        wheelCollider.enabled = false;
        riderAnim.enabled = false;
        unicycleAnim.enabled = false;
        isRagDoll = true;
        stopwatch.isActive = false;
    }
    void RagdollModeOff()                                       // ������ ��������
    {
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rigid in limbsRiderRigigidbodies)
        {
            rigid.isKinematic = true;
        }
        foreach (Collider col in unicycleColliders)
        {
            col.enabled = false;
        }
        wheelCollider.enabled = true;
        riderAnim.enabled = true;
        unicycleAnim.enabled = true;
    }
}
