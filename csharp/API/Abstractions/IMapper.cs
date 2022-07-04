namespace API.Abstractions;

public interface IMapper<TFrom, TTo>
{
    static abstract TTo Map(TFrom value);
}