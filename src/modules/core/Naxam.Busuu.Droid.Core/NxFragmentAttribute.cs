using System;
using MvvmCross.Droid.Shared.Attributes;

namespace Naxam.Busuu.Droid.Core
{
    public static class BusuuFragmentHosts
    {
        public const string MainView = nameof(MainView);
        public const string Memorise = "Memorise"; 
        public const string Dialogue = "Dialogue";
        public const string Vocabulary = "Vocabulary";
    }

    public class NxFragmentAttribute : MvxFragmentAttribute
    {
        public static readonly System.Collections.Generic.Dictionary<string, Type> typeMappings = new System.Collections.Generic.Dictionary<string, Type>();
        public static readonly System.Collections.Generic.Dictionary<string, int> framgentContainerMappings = new System.Collections.Generic.Dictionary<string, int>();

        public NxFragmentAttribute(string type, bool addToBackStack = false) : base(GetViewModelType(type), GetFragmentContainerId(type), addToBackStack) 
        {
            IsCacheableFragment = true;
        }

        public static Type GetViewModelType(string key)
        {
            Type type;

            if (typeMappings.TryGetValue(key, out type)) { return type; }

            return null;
        }

        public static int GetFragmentContainerId(string key)
        {
            int resId;

            if (framgentContainerMappings.TryGetValue(key, out resId)) { return resId; }

            return 0;
        }
    }
}
