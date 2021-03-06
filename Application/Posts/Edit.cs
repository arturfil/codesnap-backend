using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Posts {

  public class Edit {

    public class Command : IRequest {
      public Post Post { get; set; }
    }

    public class Handler : IRequestHandler<Command> {
      private readonly IMapper _mapper;

      private readonly DataContext _context;
      public Handler(DataContext context, IMapper mapper) {
        _mapper = mapper;
        _context = context;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) {
        var post = await _context.Posts.FindAsync(request.Post.Id);
        _mapper.Map(request.Post, post);
        await _context.SaveChangesAsync();
        return Unit.Value;
      }
    }

  }

}