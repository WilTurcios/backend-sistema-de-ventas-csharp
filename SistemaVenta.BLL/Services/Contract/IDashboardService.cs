﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contract
{
    public interface IDashboardService
    {
        Task<DashboardDTO> Summary();
    }
}
