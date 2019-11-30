using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMe : MonoBehaviour
{
	[HideInInspector] public int index;
	public Camera cam;
	public GameObject trueEffect;
	public GameObject falseEffect;
	public GameObject[] plates;

	Vector3 mouseRef;
	bool isDragging;
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curPosition;
	private Vector3 initialPos;
	private Quaternion initialRot;

	public static int questionsCounter = 0;
	private void Awake()
	{
		initialPos = transform.localPosition;
		initialRot = transform.localRotation;
	}

	private void Update()
	{
		if (isDragging)
		{
			transform.position = curPosition;
		}
		else
		{
			transform.localPosition = initialPos;
			transform.localRotation = initialRot;
		}
	}

	private void OnMouseDown()
	{
		isDragging = true;
		screenPoint = cam.WorldToScreenPoint(transform.position);
		offset = transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		isDragging = true;
	}

	private void OnMouseDrag()
	{
		if (isDragging)
		{
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			curPosition = cam.ScreenToWorldPoint(curScreenPoint) + offset;
		}
	}

	private void OnMouseUp()
	{
		isDragging = false;
		transform.localPosition = initialPos;
		transform.localRotation = initialRot;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(transform.tag))
		{
			Debug.Log(index + ", " + TrueOrFalseManager.currentQuestions[index].id);
			StartCoroutine(QuestionsManager.Instance.ReportQuestionsStatus(TrueOrFalseManager.currentQuestions[index].id, 1));
			Instantiate(trueEffect, transform.position, Quaternion.identity);
			PlayerStats.CorrectAnswers++;
			questionsCounter++;
			if(questionsCounter >= 3)
			{
				QuestionsManager.Instance.ClearAllQuestionGames();
				Invoke("ReActivatePlates", 0.5f);
			}
			this.gameObject.SetActive(false);
		}
		else if(other.CompareTag("TrueTF") || other.CompareTag("False"))
		{
			StartCoroutine(QuestionsManager.Instance.ReportQuestionsStatus(TrueOrFalseManager.currentQuestions[index].id, 0));
			Instantiate(falseEffect, transform.position, Quaternion.identity);
			questionsCounter++;
			if (questionsCounter >= 3)
			{
				questionsCounter = 0;
				QuestionsManager.Instance.ClearAllQuestionGames();
				ReActivatePlates();
			}
			this.gameObject.SetActive(false);
		}
	}

	private void ReActivatePlates()
	{
		foreach (GameObject gameObject in plates)
		{
			gameObject.SetActive(true);
		}
	}
}
