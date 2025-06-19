package be.itenium.sockstore.api;

import be.itenium.sockstore.api.commands.CreateProductCommand;
import be.itenium.sockstore.vocabulary.Product;

public interface CreateProduct {
    Product create(CreateProductCommand command);
}
