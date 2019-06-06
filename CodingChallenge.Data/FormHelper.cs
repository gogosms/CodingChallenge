using System;
using System.Linq;
using System.Reflection;
using CodingChallenge.Data.Forms;

namespace CodingChallenge.Data
{
    public static class FormHelper
    {
        public static string[] GetSupportForms()
        {
            return GetSupportFormTypes<Form>().Select(n => n.Name).ToArray();
        }

        public static Type[] GetSupportFormTypes<T>() where T : IForm
        {
            return  Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass
                                 && !myType.IsAbstract
                                 && myType.IsSubclassOf(typeof(T))).ToArray();
        }
    }
}