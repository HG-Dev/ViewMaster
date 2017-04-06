using System;
using System.Collections;
using System.Collections.Generic;

namespace Suffixian.ViewMaster.Core
{
    #region Enums
    /// <summary>
    /// Describes the current state of a View.
    /// </summary>
    public enum ViewState
    {
        NULL, Initializing, Opening, Closing, Inactive, Active, Updating
    }

    /// <summary>
    /// Describes the render space of a View.
    /// Orthographic views will be rendered above Perspective, and Perspective views
    /// will be rendered above World views. Each has its own input layers; orthographic modals
    /// will block perspective modals, perspective modals will block world modals, and
    /// world modals will block inputs into the world-- however, this will require some event handling.
    /// </summary>
    public enum ViewSpace
    {
        NULL, Orthograhic, Perspective, World
    }

    /// <summary>
    /// Describes the type, and possibly layer, of a View.
    /// Modal: Interrupts all UI when opened.
    /// Frame: Sits on top of all UI.
    /// Widget: Common UI components that are loaded into Pages.
    /// Page: A vessel for widgets and other information.
    /// </summary>
    public enum ViewType
    {
        NULL, Modal, Frame, Widget, Page
    }

    #endregion
    
    /// <summary>
    /// The universal View component.
    /// This starts as an interface and not an abstract class so that
    /// the actual implementation can inherit from a MonoBehaviour in Unity.
    /// 
    /// Once initialized, the ViewBody should register itself with the ViewMediator.
    /// </summary>
    public interface IViewBody
    {
        /// <summary>
        /// A ViewBody's Data defines where the view is loaded from and the serialized object payload used
        /// to display information in this view.
        /// </summary>
        ViewState CurrentState { get; }
        ViewSpace Space { get; }
        ViewType  Type { get; }

    }

}
