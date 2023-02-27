using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Locations
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Location Location { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var location = await _context.Locations.FindAsync(request.Location.Id);

                if (location == null) return null;
                
                _mapper.Map(request.Location, location);

                var result = await _context.SaveChangesAsync() > 0;

                if (result == false) return Result<Unit>.Failure("Failed To Update Location");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}