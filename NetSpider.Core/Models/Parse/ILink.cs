using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Core.Models
{
    public interface ILink
    {
        IEnumerable<string> Parse(string origin);
    }
}
