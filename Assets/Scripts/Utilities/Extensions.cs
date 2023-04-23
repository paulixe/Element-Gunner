using System.Collections;
namespace Utilities
{
    /// <summary>
    /// Class containing common <see href="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods">extensions</see> in C#/Unity projects
    /// </summary>
    public static class Extensions
    {
        public static IEnumerable Iterate(this IEnumerator iterator)
        {
            while (iterator.MoveNext())
                yield return iterator.Current;
        }
    }
}