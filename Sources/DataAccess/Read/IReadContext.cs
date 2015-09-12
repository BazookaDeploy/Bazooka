﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Read
{
    public interface IReadContext
    {
        IQueryable<T> Query<T>() where T : class;
    }
}
