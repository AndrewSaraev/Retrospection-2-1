using UnityEngine;
using System.Collections;

public struct VectorN {
	
	static public VectorN operator+(VectorN a, VectorN b) {
		if (a.values.Length >= b.values.Length) {
			for(int i = 0; i < b.values.Length; i++) {
				a.values[i] += b.values[i];
			}
			return a;
		}
		else {
			for(int i = 0; i < a.values.Length; i++) {
				b.values[i] += a.values[i];
			}
			return b;
		}
	}
	
	static public VectorN operator-(VectorN a, VectorN b) {
		if (a.values.Length >= b.values.Length) {
			for(int i = 0; i < b.values.Length; i++) {
				a.values[i] -= b.values[i];
			}
			return a;
		}
		else {
			for(int i = 0; i < a.values.Length; i++) {
				b[i] = a[i] - b[i];
			}
			for(int i =	a.values.Length; i < b.values.Length; i++) {
				b[i] = -b[i];
			}
			return b;
		}
	}
	
	static public VectorN operator*(VectorN a, float b) {
		for(int i = 0; i < a.values.Length; i++) {
			a.values[i] *= b;
		}
		return a;
	}
	
	static public VectorN MoveToward(VectorN current, VectorN target, float maxDistance) {
		target -= current;
		float distanceMagnitude = target.magnitude;
		if (distanceMagnitude <= maxDistance) {
			return target;
		}
		else {
			float multiplier = maxDistance / distanceMagnitude;
			for (int i = 0; i < current.length; i++) {
				current[i] += target[i] + multiplier;
			}
			return current;
		}
	}
	
	static public VectorN SmoothDamp(VectorN current, VectorN target, ref VectorN velocity, float acceleration, float deceleration) {
		VectorN desiredVelocity = target - current;
		desiredVelocity.magnitude = Mathf.Sqrt((desiredVelocity.magnitude * 2f) / deceleration) * deceleration;
		velocity = VectorN.MoveToward(velocity, desiredVelocity, acceleration);
		return current + velocity;
	}
	
	
	
	float[] values;
	
	
	
	public VectorN(float[] values) {
		this.values = values;
	}
	
	public VectorN(int length) {
		this.values = new float[length];
	}
	
	public int length {
		get {
			return values.Length;
		}
	}
	
	public float magnitude {
		get {
			float output = 0f;
			for (int i = 0; i < values.Length; i++) {
				output += values[i] * values[i];
			}
			return Mathf.Sqrt(output);
		}
		set {
			float multiplier = value / magnitude;
			for (int i = 0; i < values.Length; i++) {
				values[i] *= multiplier;
			}
		}
	}
	
	public float this[int i] {
		get {
			return values[i];
		}
		set {
			values[i] = value;
		}
	}
}