using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetingSystem : MonoBehaviour
{
    public Animator Anima;
    public Transform aimingWheel;
    public Transform aimingPoint;
    [Range(0, 1)]
    public float aimingWeight;
    public Vector3 offset = new Vector3(0, 0, 38);

    private Transform characterTransform;
    private Camera cam;
    private Vector3 mousePos;

    private void Start()
    {
        cam = Camera.main;
        characterTransform = Anima.transform;
    }

    private void Update()
    {
        GetMousePosition();
        RotateAimingWheel();
    }

    private void GetMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition + offset);
        mousePos.z = characterTransform.position.z;
    }

    private void RotateAimingWheel()
    {
        Quaternion targetRotation = Quaternion.LookRotation(mousePos - aimingWheel.position);
        aimingWheel.rotation = targetRotation;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Anima.SetIKPosition(AvatarIKGoal.RightHand, aimingPoint.position);
        Anima.SetIKPositionWeight(AvatarIKGoal.RightHand, aimingWeight);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mousePos, 1f);
    }
}