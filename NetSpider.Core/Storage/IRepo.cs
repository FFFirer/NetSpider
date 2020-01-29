using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Core.Storage
{
    public interface IRepo
    {
        void SaveMany<T>(IEnumerable<T> datas);
        void Save<T>(T data);
    }
}
