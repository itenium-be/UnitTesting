using Application.Commands;
using Vocabulary;

namespace Application.Interfaces;

public interface IUpdateProduct {
    Product Update(UpdateProductCommand command);
}
