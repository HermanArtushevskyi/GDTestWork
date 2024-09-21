using UnityEngine;

namespace _Project.CodeBase.Runtime.Utils
{
    public class DDOL : MonoBehaviour
    {
        private void Awake()
        {
            GameObject go = GameObject.Find(gameObject.name);
            if (go != null && go != gameObject)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(this);
        }
    }
}