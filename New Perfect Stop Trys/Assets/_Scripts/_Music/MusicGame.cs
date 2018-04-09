using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicGame : MonoBehaviour {



	public AudioClip[] music;
	public AudioSource stingSource;

	private AudioClip prevSong;
	private int randClip;
	// Use this for initialization
	void Start () 
	{
		if (PlayerPrefs.GetString ("Music") != "off") {
			stingSource = GetComponent<AudioSource> ();
			PlaySting ();
		}
	}

	void Update()
	{	
		if (PlayerPrefs.GetString ("Music") != "off") {

			if (!stingSource.isPlaying) {
				PlaySting ();
			}
		}

	}


	void PlaySting()
	{
			randClip = Random.Range (0, music.Length);
			stingSource.clip = music [randClip];
			stingSource.Play ();
	}


}