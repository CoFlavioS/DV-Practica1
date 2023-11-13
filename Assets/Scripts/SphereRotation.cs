using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotation : MonoBehaviour
{
    Matrix4x4 transformMatrix;
    public Vector3 axis;
    public float velocity;
    public Transform sphere;

    // Start is called before the first frame update
    void Start()
    {
        transformMatrix = transform.localToWorldMatrix;
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 otherTransformMatrix = sphere.localToWorldMatrix;
        Quaternion r = Quaternion.AngleAxis(velocity * Time.deltaTime, axis);
        Vector3 curPos = new Vector3(transformMatrix.m03, transformMatrix.m13, transformMatrix.m23);
        Vector3 colisionPos = new Vector3(otherTransformMatrix.m03, otherTransformMatrix.m13, otherTransformMatrix.m23);
        Vector3 next = r * curPos;

        if (Vector3.Distance(curPos, colisionPos) < 1.0f) Debug.Log("Colision!");

        transformMatrix.m03 = next.x;
        transformMatrix.m13 = next.y;
        transformMatrix.m23 = next.z;

        //Dont remove, it will apply the changes made in transFromMatrix variable every frame.
        WorkaroundSetMatrix4x4(transformMatrix);
    }

    void WorkaroundSetMatrix4x4(Matrix4x4 newMatrix)
    {
        transform.localPosition = newMatrix.GetColumn(3);
        transform.localScale = new Vector3(newMatrix.GetColumn(0).magnitude, newMatrix.GetColumn(1).magnitude, newMatrix.GetColumn(2).magnitude);
        transform.localRotation = Quaternion.LookRotation(newMatrix.GetColumn(2), newMatrix.GetColumn(1));
    }
}
