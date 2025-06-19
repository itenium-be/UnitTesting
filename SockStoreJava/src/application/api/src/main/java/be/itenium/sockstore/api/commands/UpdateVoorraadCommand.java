package be.itenium.sockstore.api.commands;

import be.itenium.sockstore.vocabulary.ProductId;

public record UpdateVoorraadCommand(ProductId id, int nieuweVoorraad) {}