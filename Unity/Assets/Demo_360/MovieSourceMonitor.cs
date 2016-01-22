using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MovieSourceMonitor : MonoBehaviour {

	public UnityEvent_String			OnFoundNewSource;
	private Dictionary<string,float>	mFilenamesAndDiscoveryTime = new Dictionary<string,float>();
	[Range(0,10)]
	public float						UpdateFrequencySecs = 1;
	private float						mUpdateCountdown = 0;

	public string						ExcludeFilter = "window:";
	public string						IncludeFilter;
	public string						IncludeDirectory;

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
		//	enum sources
		var Sources = PopMovie.EnumSources (IncludeFilter, ExcludeFilter, 50000, IncludeDirectory );

		if (Sources!=null) {
			foreach (var Filename in Sources) {
				if ( mFilenamesAndDiscoveryTime.ContainsKey( Filename ) )
					continue;

				OnFoundFilename( Filename );
			}
		}
	}

	void OnFoundFilename(string Filename)
	{
		Debug.Log ("Found file: " + Filename);

		mFilenamesAndDiscoveryTime [Filename] = Time.time;
		
		if (OnFoundNewSource != null)
			OnFoundNewSource.Invoke (Filename);
	}
}
