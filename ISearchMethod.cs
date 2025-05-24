using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArraySearch
{
    internal interface ISearchMethod
    {
        int Search(string[] sortedArray, string[] originalArray, string key);
    }
}