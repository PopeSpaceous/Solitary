// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Nathan Misener
// Date: 04/13/2018
/* Summary: 
 * For fading out audio.
*/
using System.Collections;
using UnityEngine;

public static class AudioFadeOut {
	public static IEnumerator FadeOut (AudioSource audiosource, float FadeTime){
		float startVolume = audiosource.volume;

		while (audiosource.volume > 0) {
			audiosource.volume -= startVolume * Time.deltaTime / FadeTime;

			yield return null;
		}
		audiosource.Stop ();
		audiosource.volume = startVolume;
	}
}
