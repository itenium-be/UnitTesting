package be.itenium.sockstore.api;

import be.itenium.sockstore.api.commands.UpdateProductCommand;
import be.itenium.sockstore.vocabulary.Product;

public interface UpdateProduct {
    Product update(UpdateProductCommand command);
}
