using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioVisualizer : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		// code sample from AudioSource.GetSpectrumData() in Unity docs
		float[] spectrum = audio.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);
        int i = 1;
        while (i < 1023) {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.yellow);
            i++;
        }
		// end code sample
		
		Debug.Log(spectrum[10]); // look in the console and you'll see that these are VERY SMALL NUMBERS, so we use Logs to amplify them
		float log = Mathf.Log( spectrum[10] ); // solve for y in the equation "x = e ^ y" (e = 2.718... a math constant like pi)
		Debug.Log("and " + spectrum[10] + " = e ^ " + log ); 
		
		transform.localScale = new Vector3(log, log, log);
	}
}
