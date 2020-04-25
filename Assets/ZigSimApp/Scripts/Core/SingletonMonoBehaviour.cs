using System;
using UnityEngine;

namespace ZigSimTools
{
    /// <summary>
    /// Singleton class.
    /// https://yumineko.com/unity-singletonmonobehaviour/
    /// </summary>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        private bool dontDestroyOnLoad = false;
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof (T);

                    instance = (T) FindObjectOfType (t);
                    if (instance == null)
                    {
                        Debug.LogError ($"There is no game object with {t} attached in the scene.");
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake ()
        {
            if (this != Instance)
            {
                Destroy (this);
                return;
            }

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad (this.gameObject);
            }
        }

        protected virtual void OnDestroy ()
        {
            instance = null;
        }
    }
}