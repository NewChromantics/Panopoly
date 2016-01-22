using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MovieSourceMonitor : MonoBehaviour {

	public UnityEvent_String			OnFoundNewSource;
	private Dictionary<string,float>	mFilenamesAndDiscoveryTime = new Dictionary<string,float>();
	[Range(0,10)]
	public float						UpdateFrequencySecs = 1;
	private float						mUpdateCountdown = 0;

	public List<string>					ExcludeFilenames;

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
		var Sources = PopMovie.EnumSources (null, 50000);

		if (Sources!=null) {
			foreach (var Filename in Sources) {
				if ( mFilenamesAndDiscoveryTime.ContainsKey( Filename ) )
					continue;

				bool Skip = false;
				for ( int i=0;	ExcludeFilenames != null && i<ExcludeFilenames.Count;	i++ )
				{
					Skip |= Filename.StartsWith( ExcludeFilenames[i] );
				}
				if ( Skip )
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
