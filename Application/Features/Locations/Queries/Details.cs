using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Locations
{
    public class Details
    {
        public class Query : IRequest<Result<Location>>
        {
            public int Id { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<Location>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Result<Location>> Handle(Query request, CancellationToken cancellationToken)
            {
                var location = await _context.Locations
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<Location>.Success(location);
            }
        }
    }
}