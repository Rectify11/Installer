using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Rectify11Installer.Core
{
    class SingleAssemblyComponentResourceManager : ComponentResourceManager
	{
        private Type _contextTypeInfo;
        private CultureInfo _neutralResourcesCulture;

        public SingleAssemblyComponentResourceManager(Type t)
            : base(t)
        {
            _contextTypeInfo = t;
        }

        protected override ResourceSet InternalGetResourceSet(CultureInfo culture,
            bool createIfNotExists, bool tryParents)
        {
#pragma warning disable CS0618 // Type or member is obsolete
			var rs = (ResourceSet)this.ResourceSets[culture];
#pragma warning restore CS0618 // Type or member is obsolete
	        if (rs != null) return rs;

			//lazy-load default language (without caring about duplicate assignment in race conditions, no harm done);
			this._neutralResourcesCulture ??= GetNeutralResourcesLanguage(this.MainAssembly);

			// if we're asking for the default language, then ask for the
			// invariant (non-specific) resources.
			if (_neutralResourcesCulture.Equals(culture))
				culture = CultureInfo.InvariantCulture;
			var resourceFileName = GetResourceFileName(culture);

			var store = this.MainAssembly.GetManifestResourceStream(
				this._contextTypeInfo, resourceFileName);

			//If we found the appropriate resources in the local assembly
			if (store != null)
			{
				rs = new ResourceSet(store);
				//save for later.
#pragma warning disable CS0618 // Type or member is obsolete
				AddResourceSet(this.ResourceSets, culture, ref rs);
#pragma warning restore CS0618 // Type or member is obsolete
			}
			else
			{
				rs = base.InternalGetResourceSet(culture, createIfNotExists, tryParents);
			}
			return rs;
        }

        //private method in framework, had to be re-specified here.
        private static void AddResourceSet(Hashtable localResourceSets,
            CultureInfo culture, ref ResourceSet rs)
        {
            lock (localResourceSets)
            {
                var objA = (ResourceSet)localResourceSets[culture];
                if (objA != null)
                {
	                if (Equals(objA, rs)) return;
	                rs.Dispose();
	                rs = objA;
                }
                else
                {
                    localResourceSets.Add(culture, rs);
                }
            }
        }
    }
}
