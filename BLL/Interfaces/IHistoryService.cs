﻿using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHistoryService : ICrud<HistoryModel>
    {
        public IEnumerable<HistoryModel> GetAllByEmployeeId(int id);
    }
}
