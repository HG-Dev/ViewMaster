using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using Suffixian.ViewMaster.Core;
using Suffixian.Unity;
using Suffixian.Extensions;
using Suffixian.Collections;
using Suffixian.Unity.Extensions;
using Suffixian.Unity.Abstract;

namespace Suffixian.ViewMaster.Unity
{
    public class ViewMediator : SingletonMB<ViewMediator>, IViewMediator
    {
        #region Fields
        [SerializeField]
        string _resourceFolder;
        [SerializeField]
        List<Transform> _viewRoots = new List<Transform>();

        [SerializeField]
        public Camera perspectiveCamera;
        
        #endregion

        #region Properties

        public static bool IsInitialized { get { return __instance != null; } }
        private static List<Transform> ViewRoots { get { return Instance._viewRoots; } }

        private Dictionary<string, Queue<ViewBody>> _loadedViews = new Dictionary<string, Queue<ViewBody>>();
        private string _lastLoadedResourcePath = ""; //Needed to determine where a View will be placed in hierarchy on registering

        public bool IsViewLoaded(string path)
        {
            return _loadedViews.ContainsKey(string.Concat(_resourceFolder, "/", path));
        }
        public int NumViewCopiesLoaded(string path)
        {
            string resourcePath = string.Concat(_resourceFolder, "/", path);
            if (IsViewLoaded(path))
            {
                return _loadedViews[resourcePath].Count;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region Unity Callbacks

        void Start()
        {
            //Clear any debug or sandbox game objects
            foreach (Transform root in _viewRoots)
                if (root != null)
                    root.KillChildren();

            _loadedViews.Clear();
        }

        void OnValidate()
        {
            if (ViewRoots.Count < Enum.GetValues(typeof(ViewSpace)).Length)
            {
                Debug.LogError("ViewMediator is not fully set up (requires " + Enum.GetValues(typeof(ViewSpace)).Length.ToString() + " root locations)");
            }
        }
        #endregion

        #region Interface Shells

        public void Open(string path)
        {
            if (IsViewLoaded(path))
            {
                //TODO: Activate view
            }
            else
            {
                StartCoroutine(LoadPrefab(path));
            }           
        }

        public void Close(string path)
        {
            throw new NotImplementedException();
        }

        //Terminology suggestion: "OpenCopy" for "open new instance of"

        #endregion

        #region Interface Implementation (Open)

        #endregion

        #region Interface Implementation (Close)

        #endregion

        #region General Methods

        protected IEnumerator LoadPrefab(string path)
        {
            string resourcePath = string.Concat(_resourceFolder, "/", path);
            Debug.Log("Loading " + resourcePath);
            UnityEngine.Object obj = Resources.Load(resourcePath);
            _lastLoadedResourcePath = resourcePath;

            if (obj != null)
            {
                GameObject.Instantiate(obj as GameObject);
            }
            else
            {
                throw new NullReferenceException(string.Format("ViewMediator: No asset was found at {0}.", path));
            }
            yield return 0;
        }

        internal void Register(ViewBody newBody)
        {
            if (!_loadedViews.ContainsKey(_lastLoadedResourcePath))
            {
                _loadedViews[_lastLoadedResourcePath] = new Queue<ViewBody>();
            }
            _loadedViews[_lastLoadedResourcePath].Enqueue(newBody);
        }

        #endregion
    }
}
