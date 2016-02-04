using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MovieSourceMonitor : MonoBehaviour {

	public UnityEvent_String			OnFoundNewSource;
	private Dictionary<string,float>	mFilenamesAndDiscoveryTime = new Dictionary<string,float>();
	[Range(0,10)]
	public float						UpdateFrequencySecs = 1;
	[Range(0,10)]
	public float						mUpdateCountdown = 0;

	public string						IncludeDirectory;

	uint									mIndex = 0;

	public void Disable()
	{
		this.gameObject.SetActive (false);
	}

	public void Enable()
	{
		this.gameObject.SetActive (true);
	}

	void Update () 
	{
		mUpdateCountdown -= Time.deltaTime;
		if (mUpdateCountdown < 0) {
			UpdateSources ();
			mUpdateCountdown = UpdateFrequencySecs;
		}
	}

	void UpdateSources()
	{
		PopMovie.EnumDirectory( IncludeDirectory, true );
		PopMovie.EnumDirectory( Application.streamingAssetsPath, true );
		PopMovie.EnumDirectory( Application.persistentDataPath, true );
		PopMovie.EnumDirectory( PopMovie.FilenamePrefix_Sdcard, true );

		//	enum next source
		var Source = PopMovie.EnumSource (mIndex);
		if (Source == null)
			return;
		AddSource (Source,mIndex);
		
		mIndex++;
	}

	void AddSource(string Filename,uint Index)
	{
		if ( mFilenamesAndDiscoveryTime.ContainsKey( Filename ) )
			return;
		
		OnFoundFilename( Filename );
	}


	void OnFoundFilename(string Filename)
	{
		Debug.Log ("Found file: " + Filename);

		mFilenamesAndDiscoveryTime [Filename] = Time.time;
		
		if (OnFoundNewSource != null)
			OnFoundNewSource.Invoke (Filename);
	}
}
