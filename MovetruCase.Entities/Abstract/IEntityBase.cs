using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovetruCaseEntities.Abstract
{
    public interface IEntityBase
    {
        public DateTimeOffset? CreationDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
    }
}
