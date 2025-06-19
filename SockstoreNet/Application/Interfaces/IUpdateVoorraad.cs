using Application.Commands;
using Vocabulary;

namespace Application.Interfaces;

public interface IUpdateVoorraad {
    Product UpdateVoorraad(UpdateVoorraadCommand command);
}
