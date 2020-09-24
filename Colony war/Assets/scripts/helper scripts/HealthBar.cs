using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	/// <summary>
	/// set the maximum silder's health of character.
	/// </summary>
	/// <param name="health">maximum health</param>
	public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;
		fill.color = gradient.Evaluate(1f);
	}

	/// <summary>
	/// set the current silder's health of character.
	/// </summary>
	/// <param name="health">current health</param>
	public void SetHealth(float health)
	{
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
