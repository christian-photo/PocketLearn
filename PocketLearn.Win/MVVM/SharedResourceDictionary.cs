#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PocketLearn.Win.MVVM
{
    public class SharedResourceDictionary : ResourceDictionary
    {
        /// <summary>
        /// Internal cache of loaded dictionaries.
        /// </summary>
        private static Dictionary<Uri, WeakReference> sharedDictionaries =
            new Dictionary<Uri, WeakReference>();

        private static bool isInDesignerMode;

        private Uri sourceUri;

        /// <summary>
        /// Initializes static members of the <see cref="SharedResourceDictionary"/> class.
        /// </summary>
        static SharedResourceDictionary()
        {
            isInDesignerMode = (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
        }

        public static Dictionary<Uri, WeakReference> SharedDictionaries
        {
            get { return sharedDictionaries; }
        }

        public new Uri Source
        {
            get
            {
                return this.sourceUri;
            }

            set
            {
                this.sourceUri = new Uri(value.OriginalString, UriKind.RelativeOrAbsolute);

                if (!sharedDictionaries.ContainsKey(value) || isInDesignerMode)
                {
                    base.Source = value;

                    if (!isInDesignerMode)
                    {
                        AddToCache();
                    }
                }
                else
                {
                    WeakReference weakReference = sharedDictionaries[sourceUri];
                    if (weakReference != null && weakReference.IsAlive)
                    {
                        MergedDictionaries.Add((ResourceDictionary)weakReference.Target);
                    }
                    else
                    {
                        AddToCache();
                    }
                }
            }
        }

        private void AddToCache()
        {
            base.Source = sourceUri;
            if (sharedDictionaries.ContainsKey(sourceUri))
            {
                sharedDictionaries.Remove(sourceUri);
            }
            sharedDictionaries.Add(sourceUri, new WeakReference(this, false));
        }
    }
}