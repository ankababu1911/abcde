using abcde.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcde.Model
{
    public class WorkItemCategories : BaseTenantEntity
    {
        // Foreign key
        /// <summary>
        /// WorkItem id
        /// </summary>
        public Guid WorkItemId { get; set; }


        /// <summary>
        ///Category id
        /// </summary>
        public int CategoryId { get; set; }

    }
}
