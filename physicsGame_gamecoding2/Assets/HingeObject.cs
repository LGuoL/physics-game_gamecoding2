using UnityEngine;
using UnityEngine.Events;

public class HingeObject : MonoBehaviour
{
    public float minAngle=0f;
    public float maxAngle=90f;
    public bool useSpring = true;
    public float springTargetAngle= 0f;
    public float springForce = 50f;
    public float springDamper = 5f;
    //events
    public UnityEvent OnReachMax;
    public UnityEvent OnReachMin;
    public float eventThreshold = 5f;

    HingeJoint hinge;
    bool maxEventFired = false;
    bool minEventFired = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        ConfigureHinge();
    }

    // Update is called once per frame
    void Update()
    {
        float currentAngle = hinge.angle;

        if (!maxEventFired && currentAngle >= maxAngle - eventThreshold)
        {
            maxEventFired = true;
            minEventFired = false;
            OnReachMax?.Invoke();
            Debug.Log(gameObject.name + "hinge reached max angle");
        }else if( !minEventFired && currentAngle <= minAngle + eventThreshold)
        {
            minEventFired = true;
            maxEventFired= false;
            OnReachMin?.Invoke();
            Debug.Log(gameObject.name + "hinge reached min angle");
        }
    }

    void ConfigureHinge()
    {
        JointLimits limits = hinge.limits;
        limits.min = minAngle;
        limits.max = maxAngle;
        limits.bounciness = 0f;
        limits.bounceMinVelocity = 0.2f;
        hinge.limits = limits;
        hinge.useLimits = true;

        if (useSpring)
        {
            JointSpring spring = hinge.spring;
            spring.targetPosition = springTargetAngle;
            spring.spring = springForce;
            spring.damper = springDamper;
            hinge.spring = spring;
            hinge.useSpring = true;
        }
        else
        {
            hinge.useSpring = false;
        }
    }

    public void DriveToMax()
    {
        //function
        SetMotorTarget(maxAngle);
    }

    public void DriveToMin()
    {
        SetMotorTarget(minAngle);
    }

    void SetMotorTarget(float targetAngle)
    {
        JointMotor motor = hinge.motor;
        motor.targetVelocity = targetAngle > hinge.angle ? 50f : -50f;
        motor.force = 100f;
        motor.freeSpin = false;
        hinge.motor = motor;
        hinge.useMotor = true;
    }
}
