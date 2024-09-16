using System.ComponentModel.DataAnnotations;
using MovetruCase.Entities.Entities;
using MovetruCaseEntities.Abstract;

namespace MovetruCaseEntities.Entities
{
    public class DailyStepLog : IEntityBase
    {
        public int TotalSteps { get; set; }
        [Key]
        public string UserId { get; set; }
        [Key]
        public DateTimeOffset? CreationDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public UserData UserData { get; set; }
    }
}
