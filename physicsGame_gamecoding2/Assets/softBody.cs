using System;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class softBody : MonoBehaviour
{
    [Range(0, 2f)]
    public float softness = 1;
    [Range(0.01f, 1f)]
    public float damping = 0.1f;

    public float stiffness = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void CreateSoftBodyPhysics()
    {
        SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
        if (smr == null) return;

        Cloth cloth = gameObject.AddComponent<Cloth>();
        cloth.damping = damping;
        cloth.bendingStiffness = stiffness;

        cloth.coefficients = GenerateClothCoefficents(smr.sharedMesh.vertices.Length);
    }

    //we are making an array so we have multiple coefficents for all the verticles
    //ex: mesh might have 500 verticles so cloth need 500 coefficents
    private ClothSkinningCoefficient[] GenerateClothCoefficents(int vertexCount)
    {
        ClothSkinningCoefficient[] coefficients = new ClothSkinningCoefficient[vertexCount];

        for(int i = 0; i < vertexCount; i++)
        {
            coefficients[i].maxDistance = softness;
            coefficients[i].collisionSphereDistance = 0f;
        }

        return coefficients;
    }
}
