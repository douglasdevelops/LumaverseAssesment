using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Engineers
{
    public class List
    {
        public class Query : IRequest<Result<List<Engineer>>> { }
        public class Handler : IRequestHandler<Query, Result<List<Engineer>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILogger<List> _logger;

            public Handler(ApplicationDbContext context, ILogger<List> logger)
            {
                _logger = logger;
                _context = context;
            }
            public async Task<Result<List<Engineer>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var engineers = await _context.Engineers
                    .ToListAsync(cancellationToken);

                return Result<List<Engineer>>.Success(engineers);
            }
        }
    }
}