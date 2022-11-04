using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    internal abstract class DatabaseAccessManager
    {
        protected readonly string connStr;

        protected DatabaseAccessManager(string connStr)
        {
            this.connStr = connStr;
        }
    }
}
