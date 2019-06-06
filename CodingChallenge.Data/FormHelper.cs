using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodingChallenge.Data.Forms;

namespace CodingChallenge.Data
{
    public static class FormHelper
    {
        public static string[] GetSupportForms()
        {
            return GetSupportFormTypes().Select(n => n.Name).ToArray();
        }

        public static IEnumerable<Type> GetSupportFormTypes()
        {
            return Assembly.GetAssembly(typeof(Form)).GetTypes()
                .Where(myType => myType.IsClass
                                 && !myType.IsAbstract
                                 && myType.IsSubclassOf(typeof(Form)));
        }
    }
}