using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Core.Storage
{
    public interface IStorageScheduler
    {
        void Save<T>(T data, string key);
    }
}
