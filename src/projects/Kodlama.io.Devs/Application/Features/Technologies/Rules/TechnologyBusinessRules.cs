using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
       private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
       private readonly ITechnologyRepository _techRepo;
       
       public TechnologyBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository, ITechnologyRepository techRepo)
       {
           _programmingLanguageRepository = programmingLanguageRepository;
            _techRepo = techRepo;   
       }
       
       public async Task ProgLangControl(int id)
       {
           ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository.GetAsync(c => c.Id == id);
           if (programmingLanguage == null) throw new BusinessException("Programming Language not exist.");
       }

        public async Task TechControl(int id)
        {
            Technology technology = await _techRepo.GetAsync(c => c.Id == id);
            if (technology == null) throw new BusinessException("Tech not exist.");
        }

        public async Task TechIsExist(string name)
        {
            Technology? tech = await _techRepo.GetAsync(c => c.Name == name);
            if (tech!=null) throw new BusinessException("Tech is already exist.");
        }

        public async Task TechIsNotExist(string name)
        {
            Technology? tech = await _techRepo.GetAsync(c => c.Name == name);
            if (tech == null) throw new BusinessException("Tech is not exist.");
        }


        public async Task TechNameCannotBeNull(string name)
        {
            if (name == null) throw new BusinessException("Tech name cannot be null");

        }
    }
}
