using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDProject
{
    class FitnessPlan
    {
        public int Id { get; set; }

        public DateTime PlanDate { get; set; }

        public int LengthOfRun { get; set; }

        public int NumberOfPushUps { get; set; }

        public int NumberOfSquats { get; set; }
    }
}
