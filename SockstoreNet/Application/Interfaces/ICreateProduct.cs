using Application.Commands;
using Vocabulary;

namespace Application.Interfaces;

public interface ICreateProduct {
    Product Create(CreateProductCommand command);
}
