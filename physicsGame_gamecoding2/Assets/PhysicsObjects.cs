using UnityEngine;

public class PhysicsObjects : MonoBehaviour
{
    [Header("Mass and Motion")]
    //how heavy the obj is in kg, affects how much force is needed to move it
    [Range(0.1f, 100f)]
    public float mass = 1f;

    [Range(0f, 10f)]
    public float drag = 0.5f;

    [Range(0, 10f)]
    public float angularDrag = 0.5f;

    [Header("Surface Properties")]

    [Range(0, 1f)]
    public float bounciness = 0f;
    [Range(0, 1f)]
    public float friction = 0.6f;

    [Header("Puzzle Properties")]
    public float puzzleWeight = -1f;

    Rigidbody rb;
    PhysicsMaterial physMat;
    public bool isHeld;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ApplyRigidbodySettings();
        ApplySurfaceSettings();

    }

    void ApplyRigidbodySettings()
    {
        rb.mass = mass;
        rb.linearDamping = drag;
        rb.angularDamping = angularDrag;
    }

    //physics material in unity control bounce and friction
    //we create a physmat at runtime and assign it

    void ApplySurfaceSettings()
    {
        physMat = new PhysicsMaterial(gameObject.name);
        physMat.bounciness = bounciness;
        physMat.dynamicFriction = friction;
        physMat.staticFriction = friction;

        physMat.frictionCombine = PhysicsMaterialCombine.Average;
        physMat.bounceCombine = PhysicsMaterialCombine.Maximum;

        Collider col = GetComponent<Collider>();
        if(col != null)
        {
            col.material = physMat;
        }
    }

    //preview in editor
    //when u change values in the inspector during play mode
    //this makes it apply immdiately without restarting

    private void OnValidate()
    {
        //On validate runs in the editor whenever an inspector value changes
        if(rb != null) ApplyRigidbodySettings();
    }
}
