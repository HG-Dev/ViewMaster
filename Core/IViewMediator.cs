using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The core classes used by ViewMaster, incluing ViewMediator, which needs an Init()
/// at the beginning of app execution. However, before usage, many core classes require
/// adapter classes to make them work with whatever app-building engine is at hand: 
/// Unity, for instance.
/// </summary>
namespace Suffixian.ViewMaster.Core
{
    public enum ViewRelationship
    {
        Unrelated, Parents, Children, Siblings, Cousins, NotSelf
    }

    /// <summary>
    /// Management class for "views," defined as visual UI elements in this context.
    /// Views can be used to organize the screens within an app, such that loading views
    /// allows access to different business logic and input methods.
    /// 
    /// ViewMediator is intended to be the go-to class for transitioning between views
    /// and ensuring that loaded views do not conflict with each other.
    /// </summary>
    public interface IViewMediator
    {
        //On start: load orthographic, perspective, and world roots
        //These root indices will NEVER close, and thus, should not have anything but methods

        void Open(string requestPath);
        void Close(string requestPath);

    }
}