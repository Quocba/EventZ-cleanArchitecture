﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Wrappers
{
    public class PagedRequestWithSearch : PagedRequest
    {
        public string Search { get; set; }
    }
}
