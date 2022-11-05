using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    internal class TeachersManager : DatabaseAccessManager
    {
        private object locker = new object();

        public TeachersManager(string connStr) : base(connStr)
        {

        }
    }
}
