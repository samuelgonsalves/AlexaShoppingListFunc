﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Trello.Interfaces
{
    public interface IResponse
    {
        object Body { get; }
        string ContentType { get; }
    }
}
