using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDeform : MonoBehaviour
{

	private float _animationTimer;

	public float growTime;
	public float growRate;

	void Update ()
	{
		Vector3 thisScale = this.transform.localScale;
		_animationTimer += Time.deltaTime;

		if (_animationTimer <= growTime) {
			thisScale.x -= growRate;
			thisScale.y += growRate;
		} else if (_animationTimer >= growTime && _animationTimer <= growTime*2) {
			thisScale.x += growRate;
			thisScale.y -= growRate;
		} else {
			_animationTimer = 0.0f;
		}

		this.transform.localScale = Vector3.Lerp(this.transform.localScale, thisScale, 0.5f); 
	}
}
