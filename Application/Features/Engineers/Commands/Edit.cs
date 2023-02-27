using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Engineers
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Engineer Engineer { get; set; }
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
                var engineer = await _context.Engineers.FindAsync(request.Engineer.Id);

                if (engineer == null) return null;
                
                _mapper.Map(request.Engineer, engineer);

                var result = await _context.SaveChangesAsync() > 0;

                if (result == false) return Result<Unit>.Failure("Failed To Update Engineer");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}