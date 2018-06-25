using UnityEngine;
using System.Collections;
namespace GameFramework
{
    public class  Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
	private static T _instance;
	public static T Instance
	{
		get  
		{
			if (_instance == null)  
			{  

				_instance = (T) FindObjectOfType(typeof(T));  
				if ( FindObjectsOfType(typeof(T)).Length > 1 )  
				{  
					DebugHandler.LogError("[Singleton] Something went really wrong " +  
					               " - there should never be more than 1 singleton!" +  
					               " Reopenning the scene might fix it.");  
					return _instance;  
				} 
				if (_instance == null)  
				{  
					GameObject singleton = new GameObject();  
					_instance=singleton.AddComponent<T>();
					singleton.name =typeof(T).ToString();
					DontDestroyOnLoad(singleton);
					GameObject  inspar= GameObject.Find("_instance_");
					if(inspar==null)
					{
						inspar=new GameObject();
                        DontDestroyOnLoad(inspar);
						inspar.name="_instance_";
					}
					singleton.transform.parent=inspar.transform;

						DebugHandler.Log("[Singleton] An instance of " + typeof(T) +   
					          " is needed in the scene, so '" + singleton +  
					          "' was created with DontDestroyOnLoad.");  
				}
				else
				{
						DebugHandler.Log("[Singleton] Using instance already created: " +  
					          _instance.gameObject.name);
				}
				
			}

			return _instance;
		}

	}
}
}