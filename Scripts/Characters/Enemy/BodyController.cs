using System.Collections;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    public Leg[] legs;
    [SerializeField] private Transform skeletonTransform;
    [SerializeField] private Transform rayOriginsTransform;

    private float maxTipWait = 2.5f;

    private bool stepOrder = true;

    private Coroutine curCor;
    private Coroutine curRotateCor;
    private Coroutine curMoveCor;
    private Coroutine curAdjustCor;

    private WaitForFixedUpdate waitFixedUpdate1;
    private WaitForFixedUpdate waitFixedUpdate2;
    private WaitForFixedUpdate waitFixedUpdate3;
    private WaitForFixedUpdate waitFixedUpdate4;
    private WaitForFixedUpdate waitFixedUpdate5;

    public Vector3 TargetPosition = Vector3.zero;
    public Vector3 TargetMoveDirection = Vector3.zero;
    public Vector3 RecentMovePoint = Vector3.zero;

    private bool isRotating;
    private bool isMovingForward;

    private Quaternion defaultSkeletonRotOff;
    private float skeletonYValueOff;

    private void Start()
    {
        InitDefaultValue();
    }

    private void InitDefaultValue()
    {
        waitFixedUpdate1 = new WaitForFixedUpdate();
        waitFixedUpdate2 = new WaitForFixedUpdate();
        waitFixedUpdate3 = new WaitForFixedUpdate();
        waitFixedUpdate4 = new WaitForFixedUpdate();
        waitFixedUpdate5 = new WaitForFixedUpdate();

        RecentMovePoint = transform.position;

        defaultSkeletonRotOff = skeletonTransform.rotation;
        Vector3 cummulativeFeetPos = Vector3.zero;
        foreach (Leg leg in legs)
        {
            cummulativeFeetPos += leg.curTip;
        }
        cummulativeFeetPos /= legs.Length;
        skeletonYValueOff = skeletonTransform.position.y - cummulativeFeetPos.y;
    }

    public void Move(Vector3 movePoint)
    {
        if ((movePoint - RecentMovePoint).magnitude < 0.5f) return;

        Vector3 temp = new Vector3((movePoint - transform.position).x, 0, (movePoint - transform.position).z);
        float distance = temp.magnitude;

        RecentMovePoint = movePoint;
        TargetMoveDirection = temp.normalized * (distance - 10);
        TargetPosition = transform.position + temp.normalized * (distance - 10);

        if (distance - 10 <= Mathf.Epsilon)
        {
            RotateInPlaceCoroutine();
        }
        else
        {
            curCor = StartCoroutine(MoveCoroutine());
        }
    }

    public void Stop()
    {
        if (curMoveCor != null)
        {
            StopCoroutine(curMoveCor);

            waitFixedUpdate4 = new WaitForFixedUpdate();
        }
        if (curRotateCor != null)
        {
            StopCoroutine(curRotateCor);

            waitFixedUpdate1 = new WaitForFixedUpdate();
            waitFixedUpdate2 = new WaitForFixedUpdate();
            waitFixedUpdate3 = new WaitForFixedUpdate();
        }
        if (curAdjustCor != null)
        {
            StopCoroutine(curAdjustCor);
            
            waitFixedUpdate5 = new WaitForFixedUpdate();
        }
        if (curCor != null)
            StopCoroutine(curCor);
    }

    private IEnumerator MoveCoroutine()
    {
        if (curAdjustCor != null)
        {
            StopCoroutine(curAdjustCor);

            waitFixedUpdate5 = new WaitForFixedUpdate();
        }

        // 정면 바라보도록 회전
        yield return new WaitWhile(() => isRotating); //그 이전의 회전에서 skeleton과 다리들이 적어도 같이 회전되도록
        if (curRotateCor != null)
        {
            StopCoroutine(curRotateCor);
            waitFixedUpdate1 = new WaitForFixedUpdate();
            waitFixedUpdate2 = new WaitForFixedUpdate();
            waitFixedUpdate3 = new WaitForFixedUpdate();
        }
        curRotateCor = StartCoroutine(yawRotation());

        // 회전 다 끝나면 정면으로 이동
        yield return curRotateCor;
        yield return new WaitWhile(() => isMovingForward);
        if (curMoveCor != null) { 
            StopCoroutine(curMoveCor);
            waitFixedUpdate4 = new WaitForFixedUpdate();
        }
        curMoveCor = StartCoroutine(MovingForward());
    }

    private IEnumerator RotateInPlaceCoroutine()
    {
        // 정면 바라보도록 회전
        yield return new WaitWhile(() => isRotating); //그 이전의 회전에서 skeleton과 다리들이 적어도 같이 회전되도록
        if (curRotateCor != null) { 
            StopCoroutine(curRotateCor);
            waitFixedUpdate1 = new WaitForFixedUpdate();
            waitFixedUpdate2 = new WaitForFixedUpdate();
            waitFixedUpdate3 = new WaitForFixedUpdate();
        }
        curRotateCor = StartCoroutine(yawRotation());
    }


    private void Update()
    {
        if (legs.Length < 2) return;

        for (int i = 0; i < legs.Length; i++)
        {
            if (legs[i].TipDistance > maxTipWait)
            {
                stepOrder = i % 2 == 0;
                break;
            }
        }

        foreach (Leg leg in legs)
        {
            leg.Movable = stepOrder;
            stepOrder = !stepOrder;
        }

        int index = stepOrder ? 0 : 1;
    }

    private IEnumerator yawRotation()
    {
        float stepAngle = 15;
        float rotationAngle = Vector2.SignedAngle(new Vector2((transform.forward).x, (transform.forward).z), new Vector2(TargetMoveDirection.x, TargetMoveDirection.z));
        float angleSign = (rotationAngle > 0 ? 1.0f : -1.0f);
        Quaternion defaultRotation = transform.rotation;

        for (int i = 0; i < (int)Mathf.Abs(rotationAngle / stepAngle); i++)
        {
            while (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, -stepAngle * angleSign * (i + 1), 0) * defaultRotation) > 2f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -stepAngle * angleSign, 0) * transform.rotation, 2 * Time.deltaTime);
                yield return waitFixedUpdate1;
            }
            transform.rotation = Quaternion.Euler(0, -stepAngle * angleSign * (i + 1), 0) * defaultRotation;
            yield return waitFixedUpdate2;
        }

        while (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, -rotationAngle, 0) * defaultRotation) > 2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -rotationAngle, 0) * defaultRotation, 2 * Time.deltaTime);
            yield return waitFixedUpdate3;
        }
        transform.rotation = Quaternion.Euler(0, -rotationAngle, 0) * defaultRotation;

        AdjustBody();
    }

    private IEnumerator MovingForward()
    {
        rayOriginsTransform.parent = null;

        Vector3 defaultRayOriginsPosition = rayOriginsTransform.position;
        Vector3 defaultPosition = transform.position;
        Vector3 direction = TargetMoveDirection;
        float distance = TargetMoveDirection.magnitude > 1.0f ? TargetMoveDirection.magnitude : 1.0f;

        float percent = 4 * Time.fixedDeltaTime / distance; ;
        while (PlanarDistance(defaultPosition + direction, transform.position) > 0.05f)
        {
            isMovingForward = true;
            rayOriginsTransform.parent = null;

            rayOriginsTransform.position = Vector3.Lerp(rayOriginsTransform.position, defaultRayOriginsPosition + direction, percent);
            transform.position = Vector3.Lerp(transform.position, defaultPosition + direction, percent);

            rayOriginsTransform.parent = transform;
            isMovingForward = false;

            yield return waitFixedUpdate4; //여기서 변했던 포지션이 다시 원래대로 됨

            AdjustBody();
        }
        rayOriginsTransform.parent = null;
        rayOriginsTransform.position = defaultRayOriginsPosition + direction;
        transform.position = defaultPosition + direction;
        rayOriginsTransform.parent = transform;

        AdjustBody();
    }


    private float PlanarDistance(Vector3 vec1, Vector3 vec2)
    {
        return (vec1.x - vec2.x) * (vec1.x - vec2.x) + (vec1.z - vec2.z) * (vec1.z - vec2.z);
    }

    private void AdjustBody()
    {
        if(curAdjustCor != null)
        {
            StopCoroutine(curAdjustCor);
            waitFixedUpdate5 = new WaitForFixedUpdate();
        }
        curAdjustCor = StartCoroutine(AdjustBodyTransform());
    }

    private IEnumerator AdjustBodyTransform()
    {
        Vector3 cummulativeLeftFeetPos = Vector3.zero;
        Vector3 cummulativeRightFeetPos = Vector3.zero;
        Vector3 cummulativeFrontFeetPos = Vector3.zero;
        Vector3 cummulativeHindFeetPos = Vector3.zero;
        Vector3 cummulativeFeetPos = Vector3.zero;
        for (int i = 0; i < legs.Length; i++)
        {
            if (i < legs.Length / 2)
            {
                cummulativeRightFeetPos += legs[i].curTip;
                cummulativeFeetPos += legs[i].curTip;
            }
            else
            {
                cummulativeLeftFeetPos += legs[i].curTip;
                cummulativeFeetPos += legs[i].curTip;
            }

            if (i < 6 && i > 1)
            {
                cummulativeHindFeetPos += legs[i].curTip;
            }
            else
            {
                cummulativeFrontFeetPos += legs[i].curTip;
            }
        }

        Vector3 averageLeftFeetPos = cummulativeLeftFeetPos / (legs.Length / 2);
        Vector3 averageRightFeetPos = cummulativeRightFeetPos / (legs.Length / 2);
        Vector3 averageFrontFeetPos = cummulativeFrontFeetPos / (legs.Length / 2);
        Vector3 averageHindFeetPos = cummulativeHindFeetPos / (legs.Length / 2);
        Vector3 averageFeetPos = cummulativeFeetPos / legs.Length;

        Vector3 rollVector = (averageLeftFeetPos - averageRightFeetPos);
        Vector3 pitchVector = (averageFrontFeetPos - averageHindFeetPos);

        float rollAngle = Vector3.SignedAngle(transform.right, rollVector, transform.forward);
        float pitchAngle = Vector3.SignedAngle(transform.forward, pitchVector, transform.right);

        Quaternion roll = Quaternion.AngleAxis(rollAngle, -skeletonTransform.up);
        Quaternion pitch = Quaternion.AngleAxis(pitchAngle, skeletonTransform.right);

        float duration = 3.0f;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / duration;
            skeletonTransform.position = Vector3.Lerp(skeletonTransform.position, new Vector3(skeletonTransform.position.x, skeletonYValueOff + averageFeetPos.y, skeletonTransform.position.z), time);
            skeletonTransform.rotation = Quaternion.Slerp(skeletonTransform.rotation, roll * pitch * transform.rotation * defaultSkeletonRotOff, time);
            yield return waitFixedUpdate5;
        }
    }
}
