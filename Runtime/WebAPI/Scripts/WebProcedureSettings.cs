
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace WebAPI.Scripts
{
    [CreateAssetMenu(fileName = "WebSettings", menuName = "ScriptableObjects/WebApiSettings", order = 1)]
    class WebProcedureSettings : ScriptableObject
    {
        [Header("AUTHORIZATION")]
        public string Username = string.Empty;
        public string Password = string.Empty;
        
        [Header("SETTINGS")]
        public string Ip = string.Empty;
        public bool ShowDebug = true;
        public static WebProcedureSettings Instance => Resources.Load<WebProcedureSettings>("WebSettings");
        
        
#if UNITY_EDITOR
        [MenuItem("WebAPI/Settings")]
        private static void Select()
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = Instance;
        }
#endif
   
    }
}