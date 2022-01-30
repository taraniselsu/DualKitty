using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: This isn't a true assert yet because it doesn't get compiled out
public static class Assert
{
    public static void IsTrue(bool conditionThatMustBeTrue, string msgIfNot = "Assert failed")
    {
        if (!conditionThatMustBeTrue)
        {
            Debug.LogError(msgIfNot);
        }
    }

    public static void IsFalse(bool conditionThatMustBeFalse, string msgIfNot = "Assert failed")
    {
        if (conditionThatMustBeFalse)
        {
            Debug.LogError(msgIfNot);
        }
    }

    public static void IsNotNull<T>(T obj, string msgIfNull = "Object is null")
    {
        if (obj == null)
        {
            Debug.LogError(msgIfNull);
        }
    }

    public static void IsNull<T>(T obj, string msgIfNotNull = "Object is not null")
    {
        if (obj != null)
        {
            Debug.LogError(msgIfNotNull);
        }
    }
}
