using Application;
using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Engineers
{
    public class Details
    {
        public class Query : IRequest<Result<Engineer>>
        {
            public int Id { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<Engineer>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Result<Engineer>> Handle(Query request, CancellationToken cancellationToken)
            {
                var engineer = await _context.Engineers
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<Engineer>.Success(engineer);
            }
        }
    }
}