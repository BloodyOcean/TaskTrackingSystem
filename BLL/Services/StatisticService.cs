using AutoMapper;
using BLL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    /// <summary>
    /// Service class allows operations
    /// with statistic of completion of project
    /// </summary>
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

        /// <summary>
        /// Calculates completion of all projects
        /// and sorts it in descending order
        /// </summary>
        /// <param name="count"></param>
        /// <returns>Takes first N elements from sequence</returns>
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

        /// <summary>
        /// Calculates completion of all projects made by current user
        /// and sorts it in descending order
        /// </summary>
        /// <param name="count"></param>
        /// <returns>Takes all elements from sequence</returns>
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
