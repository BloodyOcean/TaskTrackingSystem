﻿using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Enitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public ProjectService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        public async Task AddAsync(ProjectModel model)
        {
            var element = _mapper.Map<Project>(model);
            await _uow.ProjectRepository.AddAsync(element);
            await _uow.SaveAsync();
        }

        public async Task<ProjectModel> AssignManagerToProject(AssignManagerToProjectModel model)
        {
            var res = await _uow.ProjectRepository.GetByIdAsync(model.ProjectId);
            res.ManagerId = model.ManagerId;
            _uow.ProjectRepository.Update(res);
            await _uow.SaveAsync();

            return _mapper.Map<ProjectModel>(res);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _uow.ProjectRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        public IEnumerable<ProjectModel> GetAll()
        {
            var res = _mapper.Map<IEnumerable<ProjectModel>>(_uow.ProjectRepository.GetAllWithDetails());
            return res;
        }

        public ProjectModel GetByAssignmentId(int id)
        {
            var res = _uow.ProjectRepository.GetAllWithDetails().First(p => p.Assignments.Select(l => l.Id).Contains(id));
            return _mapper.Map<ProjectModel>(res);
        }

        public async Task<ProjectModel> GetByIdAsync(int id)
        {
            var res = await _uow.ProjectRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<ProjectModel>(res);
        }

        public IEnumerable<ProjectModel> GetProjectsByEmployee(int id)
        {
            var res = _mapper.Map<IEnumerable<ProjectModel>>(_uow.ProjectRepository.GetAllWithDetails().Where(p => p.ManagerId == id));
            return res;
        }

        public async Task UpdateAsync(ProjectModel model) 
        {
            _uow.ProjectRepository.Update(_mapper.Map<Project>(model));
            await _uow.SaveAsync();
        }
    }
}
