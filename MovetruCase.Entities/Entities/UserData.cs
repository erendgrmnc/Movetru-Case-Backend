using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovetruCaseEntities.Abstract;
using MovetruCaseEntities.Entities;

namespace MovetruCase.Entities.Entities
{
    public class UserData : IEntityBase
    {
        [Key]
        public string UserID { get; set; }
        public bool IsUserNewlyRegistered { get; set; }
        public int? WeightAsKilogram { get; set; }
        public int? HeightAsCentimeter { get; set; }
        public int? Age { get; set; }
        public string? Name { get; set; }
        public DateTimeOffset? CreationDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public IEnumerable<DailyStepLog> DailyStepLogs { get; set; }
    }
}
