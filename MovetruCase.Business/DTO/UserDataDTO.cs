using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovetruCase.Business.DTO
{
    public class UserDataDTO
    {
        public bool IsUserNewlyRegistered { get; set; }
        public int? WeightAsKilogram { get; set; }
        public int? HeightAsCentimeter { get; set; }
        public int? Age { get; set; }
        public string? Name { get; set; }
        public int DailyStepGoal { get; set; }
    }
}
