using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Locations
{
    public class List
    {
        public class Query : IRequest<Result<List<Location>>> { }
        public class Handler : IRequestHandler<Query, Result<List<Location>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILogger<List> _logger;

            public Handler(ApplicationDbContext context, ILogger<List> logger)
            {
                _logger = logger;
                _context = context;
            }
            public async Task<Result<List<Location>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var locations = await _context.Locations
                    .ToListAsync(cancellationToken);

                return Result<List<Location>>.Success(locations);
            }
        }
    }
}