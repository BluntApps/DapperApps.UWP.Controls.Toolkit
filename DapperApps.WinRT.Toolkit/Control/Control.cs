/*
 * Copyright (c) Dapper Apps.  All rights reserved.
 * Use of this sample source code is subject to the terms of the Dapper Apps license 
 * agreement under which you licensed this sample source code and is provided AS-IS.
 * If you did not accept the terms of the license agreement, you are not authorized 
 * to use this sample source code.  For the terms of the license, please see the 
 * license agreement between you and Dapper Apps.
 *
 * To see the article about this app, visit http://www.dapper-apps.com/DapperToolkit
 */

using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace DapperApps.WinRT.Toolkit.Control
{
    //TODO Write a controll that displays some sort of error popup on the top of a page.
    // TODO Fix nested searching for vs. Right now only searches top element.
    /// <summary>
    /// A specialized Control that adds varied functionality to the Control class.
    /// </summary>
    public class Control : Windows.UI.Xaml.Controls.Control
    {
        /// <summary>
        /// Retrieves the first instance of the named VisualStateGroup element in the instantiated
        /// System.Windows.Controls.ControlTemplate visual tree.
        /// </summary>
        /// <param name="control">The control whose Visual State Groups to get.</param>
        /// <param name="vsgName">The name of the VisualStateGroup to find.</param>
        /// <returns>The named VisualStateGroup from the template, if found.
        /// Can return null if no element with the provided name was found in the template.</returns>
        public VisualStateGroup GetTemplateVisualStateGroup(string vsgName)
        {
            DependencyObject dOControl = (DependencyObject)this;
            if (null != dOControl)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(dOControl);
                for (int i = 0; i < childrenCount; i++)
                {
                    // Downcast the child control if possible.
                    FrameworkElement fERoot = VisualTreeHelper.GetChild(this, i) as FrameworkElement;
                    if (null != fERoot)
                    {
                        //Collection<VisualStateGroup>
                        foreach (VisualStateGroup vsg in VisualStateManager.GetVisualStateGroups(fERoot))
                        {
                            if (vsg.Name == vsgName)
                            {
                                return vsg;
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves the first instance of the named VisualStateGroup element in the instantiated
        /// System.Windows.Controls.ControlTemplate visual tree.
        /// </summary>
        /// <param name="control">The control whose Visual State Groups to get.</param>
        /// <param name="vsgName">The name of the VisualStateGroup to find.</param>
        /// <returns>The named VisualStateGroup from the template, if found.
        /// Can return null if no element with the provided name was found in the template.</returns>
        public VisualStateGroup GetTemplateVisualStateGroup(string vsgName, IComparer<string> nameComparer)
        {
            DependencyObject dOControl = (DependencyObject)this;
            if (null != dOControl)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(dOControl);
                for (int i = 0; i < childrenCount; i++)
                {
                    // Downcast the child control if possible.
                    FrameworkElement fERoot = VisualTreeHelper.GetChild(this, i) as FrameworkElement;
                    if (null != fERoot)
                    {
                        //Collection<VisualStateGroup>
                        foreach (VisualStateGroup vsg in VisualStateManager.GetVisualStateGroups(fERoot))
                        {
                            if (nameComparer.Compare(vsg.Name, vsgName) == 0)
                            {
                                return vsg;
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves the first named VisualState element in the instantiated
        /// System.Windows.Controls.ControlTemplate visual tree.
        /// </summary>
        /// <param name="control">The control whose Visual State to get.</param>
        /// <param name="vsName">The name of the VisualState to find.</param>
        /// <returns>The named VisualState from the template, if found.
        /// Can return null if no element with the provided name was found in the template.</returns>
        public VisualState GetTemplateVisualState(string vsName, IComparer<string> nameComparer)
        {
            DependencyObject dOControl = (DependencyObject)this;
            if (null != dOControl)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(dOControl);
                for (int i = 0; i < childrenCount; i++)
                {
                    // Downcast the child control if possible.
                    FrameworkElement fERoot = VisualTreeHelper.GetChild(this, i) as FrameworkElement;
                    if (null != fERoot)
                    {
                        //Collection<VisualStateGroup>
                        foreach (VisualStateGroup vsg in VisualStateManager.GetVisualStateGroups(fERoot))
                        {
                            //Collection<VisualState>
                            foreach (VisualState vs in vsg.States)
                            {
                                if (nameComparer.Compare(vs.Name, vsName) == 0)
                                {
                                    return vs;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves the first named VisualState element in the instantiated
        /// System.Windows.Controls.ControlTemplate visual tree.
        /// </summary>
        /// <param name="control">The control whose Visual State to get.</param>
        /// <param name="vsName">The name of the VisualState to find.</param>
        /// <returns>The named VisualState from the template, if found.
        /// Can return null if no element with the provided name was found in the template.</returns>
        public VisualState GetTemplateVisualState(string vsName)
        {
            DependencyObject dOControl = (DependencyObject)this;
            if (null != dOControl)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(dOControl);
                for (int i = 0; i < childrenCount; i++)
                {
                    // Downcast the child control if possible.
                    FrameworkElement fERoot = VisualTreeHelper.GetChild(this, i) as FrameworkElement;
                    if (null != fERoot)
                    {
                        //Collection<VisualStateGroup>
                        foreach (VisualStateGroup vsg in VisualStateManager.GetVisualStateGroups(fERoot))
                        {
                            //Collection<VisualState>
                            foreach (VisualState vs in vsg.States)
                            {
                                if (vs.Name == vsName)
                                {
                                    return vs;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the root element of this control's controltemplate.
        /// </summary>
        /// <param name="control">The control whose root element to get.</param>
        /// Can return null if no root element was found in the template.</returns>
        public DependencyObject GetTemplateRoot()
        {
            return VisualTreeHelper.GetChild((DependencyObject)this, 0);
        }
    }
}