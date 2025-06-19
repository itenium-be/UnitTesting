package be.itenium.sockstore.api.commands;

import be.itenium.sockstore.vocabulary.*;

public record UpdateProductCommand(
        ProductId id,
        Naam naam,
        Categorie categorie,
        Prijs prijs,
        int voorraad
) {}