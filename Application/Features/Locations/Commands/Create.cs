using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Persistence;

namespace Application.Features.Locations
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Location Location { get; set; }
        }


        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {

                _context.Locations.Add(request.Location);

                var result = await _context.SaveChangesAsync() > 0;

                if (result == false)
                    return Result<Unit>.Failure("Failed To Create Location");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}