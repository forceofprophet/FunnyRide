using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargets : MonoBehaviour
{
	[SerializeField] Transform rightFootIKTarget;
	[SerializeField] Transform leftFootIKTarget;
	Animator riderAnim;
	float ikWeightRightFoot = 1.0f;
	float ikWeightLeftFoot = 1.0f;
	void Awake()
    {
		riderAnim = GetComponent<Animator>();
    }
	public void OnAnimatorIK(int layerIndex)				//во избежание рассинхронизации движения ног и педалей создал цели для струпней, которые повторяют движения педалей (прикреплены к костям педалей.
	{
		riderAnim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikWeightLeftFoot);
		riderAnim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikWeightLeftFoot);
		riderAnim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootIKTarget.position);
		riderAnim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootIKTarget.rotation);

		riderAnim.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikWeightRightFoot);
		riderAnim.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikWeightRightFoot);
		riderAnim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootIKTarget.position);
		riderAnim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootIKTarget.rotation);
	}
}
