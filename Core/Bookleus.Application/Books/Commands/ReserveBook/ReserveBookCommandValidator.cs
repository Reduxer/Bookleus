using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookleus.Application.Books.Commands.ReserveBook
{
    public class ReserveBookCommandValidator : AbstractValidator<ReserveBookCommand>
    {
        public ReserveBookCommandValidator()
        {

        }
    }
}
