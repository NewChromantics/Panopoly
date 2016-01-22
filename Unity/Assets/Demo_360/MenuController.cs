using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject	mFrontendCanvas;
	public GameObject	mGuiCanvas;
	public GameObject	m360Geo;		//	maybe this should be in a different context
	public bool			mToggleSkybox = true;

	public GameObject	mMovieButtonPrefab;
	public GameObject	mMovieButtonParent;
	public Text			mMovieText;
	public MovieController	mMovieController;

	//[Range(0,360)]
	//public float	mRotationSpeed = 1;
	
	void Update () {

		/*
		if ( mCanvas != null )
		{
			float Rotation = mRotationSpeed * Time.deltaTime;
			mCanvas.transform.Rotate( new Vector3( 0, Rotation, 0 ) );
		}
		*/
	}

	public void HideFrontendMenu()
	{
		if (mFrontendCanvas != null)
			mFrontendCanvas.gameObject.SetActive (false);
		
		Camera.main.clearFlags = CameraClearFlags.Depth;
	}
	
	public void ShowFrontendMenu()
	{
		if (mFrontendCanvas != null)
			mFrontendCanvas.gameObject.SetActive (true);
		
		Camera.main.clearFlags = CameraClearFlags.Skybox;
	}
	
	public void HideGuiMenu()
	{
		if (mGuiCanvas != null)
			mGuiCanvas.gameObject.SetActive (false);
	}
	
	public void ShowGuiMenu()
	{
		if (mGuiCanvas != null)
			mGuiCanvas.gameObject.SetActive (true);
	}
	
	public void Hide360Geo()
	{
		if (m360Geo != null)
			m360Geo.SetActive (false);

		if (mToggleSkybox)
			Camera.main.clearFlags = CameraClearFlags.Skybox;
	}
	public void Show360Geo()
	{
		if (m360Geo != null)
			m360Geo.SetActive (true);
	
		if (mToggleSkybox)
			Camera.main.clearFlags = CameraClearFlags.Depth;
	}

	public void OnNewMovieSource(string Filename)
	{
		if (mMovieText != null) {
			mMovieText.text += "\n" + Filename;
		}

		if (mMovieButtonPrefab != null) {
			GameObject NewButton = Instantiate (mMovieButtonPrefab) as GameObject;

			var Config = NewButton.GetComponent<MovieButtonConfig>();
			if ( Config != null )
			{
				Config.mMovieController = mMovieController;
				Config.SetFilename( Filename );
			}
			NewButton.transform.SetParent( mMovieButtonParent.transform );
			NewButton.transform.localPosition = Vector3.zero;
		}
	}



}
