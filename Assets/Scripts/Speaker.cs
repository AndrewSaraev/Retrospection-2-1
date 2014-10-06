using UnityEngine;
using System.Collections;

public class Speaker : MonoBehaviour {
	
	public AudioSource lamp;
	public AudioSource watch;
	public float lampVolume;
	public float watchVolume;
	float ambientVolume = 0f;
	
	public Slider slider;
	public AudioClip loadingSound;
	public Lever lever;

	public void LoadSound(AudioClip clip) {
		audio.Stop();
		audio.clip = clip;
		audio.PlayOneShot(loadingSound);
		if (clip != null) {
			slider.targetPosition = 0f;
			audio.time = 0f;
			Invoke("Play", 1.5f);
			slider.ColliderBlocking++;
			lever.ColliderBlocking++;
			ArchiveButton.blockSources++;
		}
	}

	public void Play() {
		audio.Play();
		slider.ColliderBlocking--;
		lever.ColliderBlocking--;
		ArchiveButton.blockSources--;
	}

	public float AudioTime {
		get {
			if (audio.clip != null) {
				return audio.time / audio.clip.length;
			}
			else {
				return 1f;
			}
		}
		set {
			if (audio.clip != null) {
				if (value == 1f) {
					audio.time = 0f;
				}
				else {
					audio.time = value * audio.clip.length;
				}
			}
		}
	}

	public void Pause() {
		audio.Pause();
	}
	

	public void PlayFrom(float position) {
		audio.time = position * audio.clip.length;
		audio.Play();
	}
	
	void Start() {
		lamp.volume = 0f;
		watch.volume = 0f;
	}
	
	void Update () {
		if (audio.isPlaying) {
			slider.targetPosition = audio.time / audio.clip.length;
			ambientVolume = Mathf.MoveTowards(ambientVolume, 0f, Time.deltaTime * 0.5f);
		}
		else {
			ambientVolume = Mathf.MoveTowards(ambientVolume, 1f, Time.deltaTime * 0.5f);
		}
		if (audio.clip != null && audio.clip.length == audio.time) {
			audio.time = 0f;
			audio.Stop();
		}
		lamp.volume = lampVolume * ambientVolume;
		watch.volume = watchVolume * ambientVolume;
	}

/*	void OnGUI() {
		GUI.Label(new Rect(10f, 10f, 100f, 100f), audio.time.ToString());
	} */
}
