using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace AutoCANP.Api.BusinessLogic.Utilities.Extensions
{
    public static class CollectionExtensions
    {

        public static Dictionary<TK, TV> MergeLeft<TK, TV>(this Dictionary<TK, TV> me, params IDictionary<TK, TV>[] others)
        {
            var newMap = new Dictionary<TK, TV>(me, me.Comparer);
            foreach (var src in (new List<IDictionary<TK, TV>> { me }).Concat(others))
            {
                foreach (var p in src)
                {
                    newMap[p.Key] = p.Value;
                }
            }
            return newMap;
        }


        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        /// <summary>
        /// Produces a key value pair collection from selected fields of an IEnumerable list of objects.
        /// </summary>
        /// <typeparam name="T">The type of objected contained within the IEnumerable list.</typeparam>
        /// <param name="enumerable">A list of objects.</param>
        /// <param name="value"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairList<T>(this IEnumerable<T> enumerable, Func<T, string> value, Func<T, string> text)
        {
            return enumerable.Select(f => new KeyValuePair<string, string>(value(f), text(f)));
        }

      /*  public static ICollection<KeyValuePair<string, string>> ToKeyValuePairCollection(this List<FluentValidation.Results.ValidationFailure> errors)
        {
            var returnedCollection = new Collection<KeyValuePair<string, string>>();

            if (errors != null && errors.Any())
            {
                foreach (var error in errors)
                {
                    returnedCollection.Add(new KeyValuePair<string, string>(error.ErrorMessage, error.PropertyName));
                }
            }

            return returnedCollection;
        }*/

        public static int FindIndex<T>(this IEnumerable<T> list, Predicate<T> finder)
        {
            int index = 0;
            foreach (var item in list)
            {

                if (finder(item))
                {
                    return index;
                }
                index++;
                //  index++;
            }

            return -1;
        }

        /// <summary>
        /// Checks if a list is not null and contains objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool HasContent<T>(this IList<T> collection)
        {
            return collection != null && collection.Any();
        }

        public static bool HasContent<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Skip(0).Any();
        }

        public static bool HasContent<T, T1>(this IDictionary<T, T1> collection)
        {
            return collection != null && collection.Any();
        }

        public static bool IsEmpty<T>(this IList<T> collection)
        {
            return !(collection != null && collection.Any());
        }

        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return !(collection != null && collection.Skip(0).Any());
        }

        public static bool IsEmpty<T, T1>(this IDictionary<T, T1> collection)
        {
            return !(collection != null && collection.Any());
        }
        /// <summary>
        /// Serialises an object to Json using the ServiceStack Json serialiser.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonSerializer.SerializeToString(obj);
        }

        public static T FromJson<T>(this string obj)
        {
            return JsonSerializer.DeserializeFromString<T>(obj);
        }
    }
}
