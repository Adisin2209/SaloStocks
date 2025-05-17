using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
	public RectTransform canvasRectTransform; // Canvas RectTransform
	public Vector2 offset; // Offset in X und Y, im Inspector anpassbar

	private RectTransform rectTransform;

	void Awake()
	{
		canvasRectTransform = GameObject.FindWithTag("mainCanvas").GetComponent<RectTransform>();
		rectTransform = GetComponent<RectTransform>();
	}

	void Update()
	{
		Vector2 localPoint;
		Vector2 screenPoint = Input.mousePosition;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPoint, null, out localPoint))
		{
			rectTransform.localPosition = localPoint + offset;
		}
	}
}
