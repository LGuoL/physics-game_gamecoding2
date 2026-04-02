using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //weight settings
    //how much total weight is needed to activate the plate
    public float weightThreshold = 5f;

    //if true the plate stays activated even after the object is removed
    public bool lockOnActivate = false;

    //event 
    //friend when total weight exceeds the threshold
    //diff from event action which we were previously doing, unity event need to be wired in the inspector and is more like but 
    //static event action is just code, doesnt need ref to sender bc static, not as designer friendly
    //fired when total exceeds the threshold
    public UnityEvent onActivated;

    public UnityEvent onDeactivated;

    public Transform plate;

    public float pressDepth = 0.05f;

    float currentWeight = 0f;
    bool isActivated = false;
    bool isLocked = false;
    Vector3 plateResetPos;
    Vector3 platePressedPos;

    HashSet<PhysicsObjects> objectsOnPlate = new HashSet<PhysicsObjects>();


    void Start()
    {
        if(plate != null)
        {
            plateResetPos = plate.localPosition;

            platePressedPos = plateResetPos + Vector3.down * pressDepth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhysicsObjects physOb = other.GetComponent<PhysicsObjects>();
        if (physOb == null) return;

        if (physOb.isHeld) return;

        /*currentWeight += physOb.puzzleWeight;
        Debug.Log($"{other.gameObject.name} entered plate. total weight: {currentWeight}");
        CheckActivation();*/

        if (objectsOnPlate.Add(physOb))
        {
            currentWeight += physOb.puzzleWeight;
            CheckActivation();
        }
    }


    void CheckActivation()
    {
        if(!isActivated && currentWeight >= weightThreshold)
        {
            isActivated = true;
            if(lockOnActivate) isLocked = true;
            
            onActivated.Invoke();
            Debug.Log("Pressure plate is activated");

            if(plate != null)
            {
                plate.localPosition = platePressedPos;
            }
        }
    }


    void checkDeactivation()
    {
        if(isActivated && !isLocked && currentWeight < weightThreshold)
        {
            isActivated = false;
            onDeactivated.Invoke();
            Debug.Log("Pressure plate is deactivated");

            if(plate != null)
            {
                plate.localPosition = plateResetPos;
            }
        }
    }
}
