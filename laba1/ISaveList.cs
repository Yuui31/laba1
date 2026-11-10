using System;
using System.Collections.Generic;
using System.Text;

namespace laba1
{
    public interface ISaveList<T>
    {
        void SaveToJson(string path);
        void LoadFromJson(string path);
    }
}