using System.Collections.Generic;
using ProjectManagementApp.Application.Responses;

namespace ProjectManagementApp.Application.Features.Project.Queries.GetAllProjects
{
    public class GetAllProjectResponse:BaseResponse
    {
      public List<GetAllProjectsDto> Projects { get; set; }
    }
} 