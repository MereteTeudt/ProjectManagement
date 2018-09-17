using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAPI.EF
{
    public interface IWorkable
    {
        string Name
        {
            get;
            set;
        }
        string Description
        {
            get;
            set;
        }
        DateTime StartDate
        {
            get;
            set;
        }
        DateTime EndDate
        {
            get;
            set;
        }

        decimal Calculate();
    }
}
