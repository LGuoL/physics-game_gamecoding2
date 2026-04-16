using UnityEngine;
using UnityEngine.Events;

public class HingeObject : MonoBehaviour
{
    public HingeJoint hinge;
    public float minAngle = 0f;
    public float maxAngle = 55f;
    public bool useSpring = true;
    public float springTargetAngle = 0f;
    public float springForce = 70f;
    public float springDamper = 10f;
    public float eventThreshold = 2f;

    public UnityEvent OnReachMax;
    public UnityEvent OnReachMin;

    private bool hasTriggeredMax = false;
    private bool hasTriggeredMin = false;

    void Start()
    {
        if (hinge == null)
            hinge = GetComponent<HingeJoint>();

        if (hinge != null && useSpring)
        {
            JointSpring spring = hinge.spring;
            spring.targetPosition = springTargetAngle;
            spring.spring = springForce;
            spring.damper = springDamper;
            hinge.spring = spring;
            hinge.useSpring = true;
        }
    }

    void Update()
    {
        if (hinge == null) return;

        float angle = Mathf.Abs(hinge.angle);

        bool nearMax = Mathf.Abs(angle - maxAngle) <= eventThreshold;
        bool nearMin = Mathf.Abs(angle - minAngle) <= eventThreshold;

        if (nearMax && !hasTriggeredMax)
        {
            hasTriggeredMax = true;
            hasTriggeredMin = false;
            Debug.Log(gameObject.name + " hinge reached max angle");
            OnReachMax?.Invoke();
        }
        else if (!nearMax && hasTriggeredMax)
        {
            // 离开 max 区域后才允许再次触发
            hasTriggeredMax = false;
        }

        if (nearMin && !hasTriggeredMin)
        {
            hasTriggeredMin = true;
            hasTriggeredMax = false;
            Debug.Log(gameObject.name + " hinge reached min angle");
            OnReachMin?.Invoke();
        }
        else if (!nearMin && hasTriggeredMin)
        {
            // 离开 min 区域后才允许再次触发
            hasTriggeredMin = false;
        }
    }
}