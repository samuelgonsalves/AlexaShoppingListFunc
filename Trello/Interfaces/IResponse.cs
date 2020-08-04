using System;
using System.Collections.Generic;
using System.Text;

namespace Trello.Interfaces
{
    public interface IResponse<out T>
    {
        T Body { get; }
    }
}
