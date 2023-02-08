using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bookleus.Domain.Entities;
using Bookleus.Application.Dtos.Book;

namespace Bookleus.Application.Common.Mappings
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile() 
        {
            CreateMap<Book, BookDto>();
        }
    }
}
