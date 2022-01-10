using AutoMapper;
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

        private readonly string _taskFinishStatusName = "Ended";

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
                    Project_name = p.Title,
                    Ended_count = p.Assignments.Where(l => l.AssignmentStatus.Status == _taskFinishStatusName).Count(),
                    Whole_count = p.Assignments.Count()
                })
                .Select(p => new CompletionPercentage
                {
                    ProjectId = p.Project_id,
                    ProjectTitle = p.Project_name,
                    Percentage = (double)p.Ended_count / (double)p.Whole_count
                })
                .OrderByDescending(p => p.Percentage)
                .Take(count);

            return res;
        }

        public IEnumerable<CompletionPercentage> GetCompletionPercentagesByManager(int id)
        {
            var res = _uow.ProjectRepository.GetAllWithDetails()
                .Where(p => p.ManagerId == id)
                .Select(p => new
                {
                    Project_id = p.Id,
                    Project_name = p.Title,
                    Ended_count = p.Assignments.Where(l => l.AssignmentStatus.Status == _taskFinishStatusName).Count(),
                    Whole_count = p.Assignments.Count()
                })
                .Select(p => new CompletionPercentage
                {
                    ProjectId = p.Project_id,
                    ProjectTitle = p.Project_name,
                    Percentage = (double)p.Ended_count / (double)p.Whole_count
                })
                .OrderByDescending(p => p.Percentage);

            return res;
        }
    }
}
