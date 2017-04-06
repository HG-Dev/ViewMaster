using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Suffixian.ViewMaster.Core;
using Suffixian.Unity.Extensions;
using Suffixian.Unity.Abstract;
using Suffixian.Extensions;
using Suffixian.Externs;

namespace Suffixian.ViewMaster.Unity
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class ViewBody : MonoBehaviour, IViewBody
    {
        #region Properties
        public bool IsInitialized
        {
            get
            {
                return (this.CurrentState > ViewState.Initializing);
            }
        }

        public virtual bool IsInteractable
        {
            get
            {
                return canvasGroup.interactable;
            }
            set
            {
                canvasGroup.interactable = value;
            }
        }

        public ViewState CurrentState
        {
            get
            {
                return _currentState;
            }
        }
        [SerializeField]
        [HideInInspector]
        protected ViewState _currentState = ViewState.NULL;

        public ViewSpace Space
        {
            get
            {
                return _space;
            }
        }
        [SerializeField]
        [HideInInspector]
        ViewSpace _space = ViewSpace.NULL;

        public ViewType Type
        {
            get
            {
                return _type;
            }
        }
        [SerializeField]
        [HideInInspector]
        ViewType _type = ViewType.NULL;

        [SerializeField]
        protected UnityEngine.CanvasGroup canvasGroup;

        public void _OverwriteValues(ViewSpace space, ViewType type)
        {
#if UNITY_EDITOR
            this._space = space;
            this._type = type;
#endif
        }

        #endregion

        public override string ToString()
        {
            return string.Format("ViewBody: {0}", this.name);
        }

        #region View Events

        void Start()
        {
            this._currentState = ViewState.Inactive;
            ViewMediator.Instance.Register(this);
        }

        #endregion
    }
}
