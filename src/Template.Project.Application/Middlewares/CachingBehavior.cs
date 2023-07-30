using MediatR;
using Template.Project.Application.Middlewares.Interfaces;
using Template.Project.Domain.Interfaces;

namespace Template.Project.Application.Middlewares
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> , ICacheableMediatr
    {
        private readonly ICacheService _cache;
        public CachingBehavior(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;

            var check = await _cache.GetValueAsync((string)request.CacheKey);
            if (check is null)
            {
                await _cache.SetValueAsync((string)request.CacheKey, (string)request.CacheKey);
            }
            else
            {
                throw new Exception($"CustomerId is locked!");
            }

            try
            {
                response = await next();

                await _cache.Clear((string)request.CacheKey);
                return response;
            }
            catch (Exception e)
            {
                await _cache.Clear((string)request.CacheKey);
                throw new Exception($"Error : {e.Message}");
            }
        }
    }
}
