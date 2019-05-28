using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTask.Models.Geo
{
    public class ResponseGeo
    {
	    public List<ResultGeo> Results { get; set; }

	    public string Status { get; set; }
    }
}
