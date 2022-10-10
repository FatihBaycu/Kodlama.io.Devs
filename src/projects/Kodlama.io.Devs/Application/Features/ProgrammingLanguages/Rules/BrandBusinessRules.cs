using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _repo;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository repo)
        {
            _repo = repo;
        }

        public async Task ProgLangNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _repo.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Prog lang name exists.");
        }


        public void ProgLangShouldExistWhenRequested(ProgrammingLanguage programmingLanguage)
        {
            if (programmingLanguage == null) throw new BusinessException("Requested Prog lang does not exist");
        }

        public async Task ForDelete(ProgrammingLanguage programmingLanguage)
        {
            if (programmingLanguage == null) throw new BusinessException("Requested Prog lang does not exist");
        }


        public async Task ProgLangCannotBeNull(string name)
        {
            if (name == null) throw new BusinessException("Prog lang cannot be null");

        }
    }
}
