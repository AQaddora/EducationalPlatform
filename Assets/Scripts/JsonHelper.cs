using System;
using UnityEngine;

[System.Serializable]
public static class JsonHelper
{
	public static string FixJsonArray(string json)
	{
		string newJson = "";
		int i = 0;

		for (; i < json.Length; i++)
		{
			if (json[i].Equals('['))
			{
				newJson += "\"{\\\"Items\\\":[";
				i++;
				break;
			}
			else
			{
				newJson += json[i];
			}
		}
		for (; i < json.Length; i++)
		{
			if (json[i].Equals('"'))
			{
				newJson += "\\\"";
			}else if (json[i].Equals(']'))
			{
				newJson += "]}\"";
				i++;
				break;
			}
			else
			{
				newJson += json[i];
			}
		}
		for (; i < json.Length; i++)
		{
			newJson += json[i];
		}
		
		return newJson;
	}
	public static T[] FromJson<T>(string json)
	{
		try
		{
			Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
			return wrapper.Items;
		}
		catch
		{
			Debug.Log("cought sthn");
		}

		T[] emptyArray = new T[1];
		return emptyArray;
	}

	public static string ToJson<T>(T[] array)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.Items = array;
		return JsonUtility.ToJson(wrapper);
	}

	public static string ToJson<T>(T[] array, bool prettyPrint)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.Items = array;
		return JsonUtility.ToJson(wrapper, prettyPrint);
	}

	[Serializable]
	private class Wrapper<T>
	{
		public T[] Items;
	}
}