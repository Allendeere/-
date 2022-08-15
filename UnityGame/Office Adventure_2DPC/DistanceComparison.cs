using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DistanceComparison : IComparer
    {

    private Transform compareTransform;

    public DistanceComparison(Transform compTransform)
    {
        compareTransform = compTransform;
    }
       public int Compare(object x,object y)
    {
        Collider xcollider = x as Collider;
        Collider ycollider = y as Collider;

        Vector3 offset = xcollider.transform.position - compareTransform.position;
        float xDistane = offset.sqrMagnitude;

        offset = ycollider.transform.position - compareTransform.position;
        float yDistane = offset.sqrMagnitude;

        return xDistane.CompareTo(yDistane);
    }
}
