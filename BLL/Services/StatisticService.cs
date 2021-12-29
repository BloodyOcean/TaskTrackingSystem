﻿using AutoMapper;
using BLL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class StatisticService : IStatisticService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public StatisticService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        public IEnumerable<CompletionPercentage> GetCompletionPercentages(int count)
        {
            var res = _uow.ProjectRepository.GetAllWithDetails()
                .Select(p => new
                {
                    Project_id = p.Id,
                    Ended_count = p.Assignments.Where(l => l.AssignmentStatus.Status == "New").Count(),
                    Whole_count = p.Assignments.Count()
                })
                .Select(p => new CompletionPercentage
                {
                    ProjectId = p.Project_id,
                    Percentage = p.Ended_count / p.Whole_count
                })
                .OrderByDescending(p => p.Percentage)
                .Take(count);

            return res;
        }
    }
}