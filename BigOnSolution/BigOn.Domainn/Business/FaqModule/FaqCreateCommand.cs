using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.FaqModule
{
 public class FaqCreateCommand:IRequest<Faq>
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public class FaqCreateCommandHandler : IRequestHandler<FaqCreateCommand, Faq>
        {
            private readonly BigOnDbContext command;

            public FaqCreateCommandHandler(BigOnDbContext command)
            {
                this.command = command;
            }

            public async Task<Faq> Handle(FaqCreateCommand request, CancellationToken cancellationToken)
            {
                var faq = new Faq
                {
                    Question = request.Question,
                    Answer = request.Answer
                };
                await command.AddAsync(faq, cancellationToken);
                await command.SaveChangesAsync();
                return faq;
            }
        }
    }
}
