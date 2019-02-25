using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDemo
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Customer
    {

    }

    class LongDictionary : DictionaryBase<long, Customer>
    {

    }

    class IntDictionary : DictionaryBase<int, Customer>
    {

    }


    class DictionaryBase<TKey, TValue>
        where TKey : struct
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
        }

        TValue Get(TKey key)
        {
            return dictionary[key];
        }
    }


    class DictionaryBase
    {
        Dictionary<int, object> dictionary = new Dictionary<int, object>();

        void Add(int key, object value)
        {
            dictionary.Add(key, value);
        }

        object Get(int key)
        {
            return dictionary[key];
        }
    }


}
