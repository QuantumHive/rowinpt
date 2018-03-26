namespace AlperAslanApps.Core
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
