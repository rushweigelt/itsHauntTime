using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// 
/// https://answers.unity.com/questions/1408574/destroying-and-recreating-a-singleton.html
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_Instance = null;
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<T>();
                // fallback, might not be necessary.
                if (m_Instance == null)
                    m_Instance = new GameObject(typeof(T).Name).AddComponent<T>();

                // This breaks scene reloading
                // DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }
}