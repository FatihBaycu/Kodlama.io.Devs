using Application.Features.GithubProfiles.Dtos;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.Technologies.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Models
{
    public class GithubProfileListModel:BasePageableModel
    {
        public IList<GithubProfileListDto> Items { get; set; }

    }
}
