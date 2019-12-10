using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Core.Models
{
    public interface IContent
    {
        void Parse(string origin);
    }
}
