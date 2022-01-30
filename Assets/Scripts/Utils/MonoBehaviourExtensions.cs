using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Contains extensions we're adding to default MonoBehaviour behaviour.
/// </summary>
public static class MonoBehaviourExtensions
{
    /// <summary>
    /// InvokeAction() should be used in place of Invoke() because Find
    /// References will miss any function executed via Invoke().
    /// Usage should always look like this:
    /// <code>
    /// this.InvokeAction(MyFunctionName, floatOptionalTimeToDelay);
    /// </code>
    /// You always need the "this."
    /// If you want to call a function with parameters, pass as theDelegate a
    /// lambda function that makes the parameterized function call.
    /// Code taken from here, although the site may not be up anymore:
    /// https://wp.flyingshapes.com/dont-use-monobehaviour-invoke-or-how-to-properly-invoke-a-method/
    /// </summary>
    public static void InvokeAction(this MonoBehaviour me, Action theDelegate, float delay = 0f)
    {
        if (delay > 0f)
        {
            me.StartCoroutine(CoroExecuteAfterTime(theDelegate, delay));
        }
        else
        {
            theDelegate();
        }
    }

    /// <summary>This function is used by InvokeAction() when it's called with
    /// a delay parameter.</summary>
    private static IEnumerator CoroExecuteAfterTime(Action theDelegate, float delay)
    {
        yield return new WaitForSeconds(delay);
        theDelegate();
    }
}