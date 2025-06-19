package be.itenium.sockstore.api.commands;

import be.itenium.sockstore.vocabulary.*;

public record CreateProductCommand(
        Naam naam,
        Categorie categorie,
        Prijs prijs,
        int voorraad
) {}