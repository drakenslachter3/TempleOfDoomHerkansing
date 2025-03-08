namespace TempleOfDoom.Core.Factories
{
    public interface IGameObjectFactory<TDto, TModel>
    {
        string Type { get; }
        TModel Create(TDto data);
    }
}
