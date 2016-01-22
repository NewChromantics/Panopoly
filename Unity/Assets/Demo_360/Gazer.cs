using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Gazer : MonoBehaviour {

	public GameObject	mReticle;
	public GameObject	mEyeAnchor;
	public LayerMask	mLayerMask;
	public EventSystem	mGuiEventSystem;

	void OnDisable()
	{
		//	if we're disabled, turn off the reticle
		if ( mReticle != null )
		{
			mReticle.SetActive(false);
		}
	}

	void Update () {

		var Anchor = mEyeAnchor ? mEyeAnchor : Camera.main.gameObject;// this.gameObject;

		Vector3 Origin = Anchor.transform.position;
		Vector3 Direction = Anchor.transform.TransformDirection(Vector3.forward);

		RaycastHit RayHit;
		Vector3 HitPos;

		float MaxDistance = 1000;

		GameObject HitObject = null;


		//	gr: 2D check... this seems wrong.
		if (mGuiEventSystem) {
			PointerEventData pointerEventData = new PointerEventData (mGuiEventSystem);
			pointerEventData.delta = Vector2.zero;
			pointerEventData.position = new Vector2( Screen.width/2, Screen.height/2 );
			/*
			pointerEventData.
			mGuiEventSystem
			Vector2 lookPosition;
			lookPosition.x = Screen.width/2;
			lookPosition.y = Screen.height/2;
			if (lookData == null) {
				lookData = new PointerEventData(eventSystem);
			}
			lookData.Reset();
			lookData.delta = Vector2.zero;
			lookData.position = lookPosition;
			lookData.scrollDelta = Vector2.zero;
*/
			List<RaycastResult> Results = new List<RaycastResult>();
			mGuiEventSystem.RaycastAll (pointerEventData, Results);

			string DebugHit = null;
			foreach ( var Result in Results )
			{
				if ( DebugHit == null)
					DebugHit = "";
				DebugHit += Result.gameObject.name + " ";
			}
			if ( DebugHit != null )
				Debug.Log(DebugHit);

			HitObject = Results.Count>0 ? Results[0].gameObject : null;

		}
/*

		if (Physics.Raycast (Origin, Direction, out RayHit, MaxDistance, mLayerMask)) {

			HitObject = RayHit.collider.gameObject;

			//	exclude some stuff
			if (HitObject == mReticle)
				HitObject = null;
		}
*/
		if ( HitObject != null )
		{
			Debug.Log (HitObject.name);
			HitPos = HitObject.transform.position;
/*				
			activeButton = hit.collider.gameObject;
			if ( ButtonExists(activeButton))
			{
				if(!crossHairActive){
					//hit.collider.gameObject.GetComponent<Image>().sprite = RingPlain;
					CrossHairAnimator.SetTrigger ("Expand");
					crossHairActive = true;
				}
			}
		} else {
			Debug.Log ("hit nothing");
			if(crossHairActive){
				CrossHairAnimator.SetTrigger ("Reduce");
				crossHairActive=false;
				activeButton=null;
			}
			*/
		} else {
			float NoHitDist = 100;
			//float NoHitDist = (MaxDistance/2.0f)
			HitPos = Origin + Direction.normalized*NoHitDist;
	
		}


		//	update reticle
		if (mReticle != null) {
			mReticle.SetActive (true);
			mReticle.transform.position = HitPos;
		}

	}
}
