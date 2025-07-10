using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementApp.Application.Responses;

namespace ProjectManagementApp.Application.Features.Project.Queries.GetProjectById
{
    public class GetProjectByIdResponse:BaseResponse
    {
     public ProjectDto Project { get; set; }
    }
}
