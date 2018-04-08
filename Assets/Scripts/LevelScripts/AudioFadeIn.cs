using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour {

	public static IEnumerator FadeIn (AudioSource audiosource, float FadeTime){
		float startVolume = 0f;
		float RegVolume = audiosource.volume;
		audiosource.volume = startVolume;
		while (audiosource.volume < RegVolume) {
			audiosource.volume += RegVolume * Time.deltaTime / FadeTime;

			yield return null;
		}
		audiosource.volume = RegVolume;
	}
}
