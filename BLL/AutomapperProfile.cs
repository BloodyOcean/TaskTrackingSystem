﻿using AutoMapper;
using BLL.Models;
using DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Assignment, AssignmentModel>()
                .ForMember(p => p.HistoryIds, c => c.MapFrom(t => t.Histories.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<Employee, EmployeeModel>()
                .ForMember(p => p.HistoryIds, c => c.MapFrom(t => t.Hostories.Select(x => x.Id)))
                .ForMember(p => p.ProjectIds, c => c.MapFrom(t => t.Projects.Select(x => x.Id)))
                .ForMember(p => p.AssignmentIds, c => c.MapFrom(t => t.Assignments.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<Project, ProjectModel>()
                .ForMember(p => p.AssignmentIds, c => c.MapFrom(t => t.Assignments.Select(x => x.Id)))
                .ReverseMap();
        }
    }
}