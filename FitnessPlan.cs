using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDProject
{
    sealed class FitnessPlan
    {

        public readonly int Id;

        public readonly DateTime PlanDate;

        public readonly int LengthOfRun;

        public readonly int NumberOfPushUps;

        public readonly int NumberOfSquats;
        
        public FitnessPlan(int id, DateTime planDate, int lengthOfRun, int numberOfPushUps, int numberOfSquats)
        {
            this.Id = id;
            this.PlanDate = planDate;
            this.LengthOfRun = lengthOfRun;
            this.NumberOfPushUps = numberOfPushUps;
            this.NumberOfSquats = numberOfSquats;
        }
        public FitnessPlan(int id, int lengthOfRun, int numberOfPushUps, int numberOfSquats)
        {
            this.Id = id;
            this.PlanDate = System.DateTime.Now;
            this.LengthOfRun = lengthOfRun;
            this.NumberOfPushUps = numberOfPushUps;
            this.NumberOfSquats = numberOfSquats;
        }
        public String ToFileFormat()
        {
            return (string.Format("{0},{1},{2},{3},{4}", this.Id, this.PlanDate, this.LengthOfRun,this.LengthOfRun,this.NumberOfPushUps,this.NumberOfSquats));
        }
        public override string ToString()
        {
            return "ID: " + string.Concat(this.Id) + " Length of run :"+string.Concat(this.LengthOfRun)+" Number of Squats: "+string.Concat(this.NumberOfSquats)+" Number of push ups: "+string.Concat(this.NumberOfPushUps)+" Date: "+string.Concat(this.PlanDate);
        }
    }
}
