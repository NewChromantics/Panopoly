using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MovieButtonConfig : MonoBehaviour {

	public string				mFilename;
	public MovieController		mMovieController;


	void OnClick()
	{
		if (mMovieController != null) {
			mMovieController.StartMovie(mFilename);
		}
	}


	public void SetFilename(string Filename)
	{
		mFilename = Filename;


		//	setup gui callback
		var ButtonChild = transform.GetComponent<Button> ();
		if (ButtonChild != null) {
			if ( ButtonChild.onClick == null )
				ButtonChild.onClick = new Button.ButtonClickedEvent();
			ButtonChild.onClick.AddListener( OnClick );
		}
			
		//	setup gui
		var TextChild = transform.FindChild("PlayButtonText");
		if ( TextChild != null )
		{
			var Text = TextChild.GetComponent<Text>();
			if ( Text != null )
			{
				Text.text = Filename;
			}
		}

	}
}
