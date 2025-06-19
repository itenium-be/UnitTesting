using Application.Commands;
using Vocabulary;

namespace Application.Interfaces;

public interface IUpdateVoorraad
{
    Task<Product> UpdateVoorraad(UpdateVoorraadCommand command);
}
