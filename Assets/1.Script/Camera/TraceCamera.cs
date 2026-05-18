using UnityEngine;

public class TraceCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private string targetName = "Player";
    [SerializeField, Label("카메라 위치 보정 수치")] private Vector3 offset = new Vector3(0.0f, 0.0f, -10.0f);
    [SerializeField, Label("카메라가 따라오는 속도")] private float smoothTime = 0.2f;
    private Vector3 currentVelocity = Vector3.zero;
    void Awake()
    {
        if(target==null)
            target = GameObject.FindGameObjectWithTag(targetName).transform;   
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPos = target.position + offset;
        Vector3 smoothDamp = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);

        transform.position = smoothDamp;
    }
}
