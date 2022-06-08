using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogasiNaploAsztaliAlkalmazas.Stores
{
    public static class RunEnvironment
    {
        static bool TestEnvironment = false;

        static public void ChangeToTestEnvironment()
        {
            TestEnvironment = true;
        }

        public static bool IsTestEnvironment
        {
            get
            {
                return TestEnvironment;
            }
        }
    }
}
