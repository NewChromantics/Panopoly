using UnityEngine;
using System.Collections;
using UnityEngine.Events;


//	cannot serialise (and therefore expoes to the inspector) templated UnityEvents.
//	here is a workaround.
//	http://forum.unity3d.com/threads/unityevent-t1-will-be-ever-serialized.263761/

[System.Serializable]
public class UnityEvent_String : UnityEvent <string>{ }



