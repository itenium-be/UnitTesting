package be.itenium.sockstore.rest.controller;

import be.itenium.sockstore.api.CreateProduct;
import be.itenium.sockstore.api.ProductQuery;
import be.itenium.sockstore.api.UpdateProduct;
import be.itenium.sockstore.api.UpdateVoorraad;
import be.itenium.sockstore.api.commands.CreateProductCommand;
import be.itenium.sockstore.api.commands.UpdateProductCommand;
import be.itenium.sockstore.api.commands.UpdateVoorraadCommand;
import be.itenium.sockstore.rest.dto.ProductResponse;
import be.itenium.sockstore.rest.dto.SaveProductRequest;
import be.itenium.sockstore.vocabulary.*;
import org.springframework.web.bind.annotation.*;

import java.math.BigDecimal;
import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/producten")
public class ProductController {
    private final ProductQuery productQuery;
    private final CreateProduct createProduct;
    private final UpdateProduct updateProduct;
    private final UpdateVoorraad updateVoorraad;

    public ProductController(ProductQuery service, CreateProduct createProduct, UpdateProduct updateProduct, UpdateVoorraad updateVoorraad) {
        this.productQuery = service;
        this.createProduct = createProduct;
        this.updateProduct = updateProduct;
        this.updateVoorraad = updateVoorraad;
    }

    @PostMapping
    public ProductResponse create(@RequestBody SaveProductRequest request) {
        var saved = createProduct.create(new CreateProductCommand(
                new Naam(request.naam()),
                new Categorie(request.categorie()),
                new Prijs(BigDecimal.valueOf(request.prijs())),
                request.voorraad()));
        return ProductResponse.toRest(saved);
    }

    @PutMapping("/{id}")
    public ProductResponse update(@PathVariable String id, @RequestBody SaveProductRequest request) {
        var updated = updateProduct.update(new UpdateProductCommand(
                new ProductId(id),
                new Naam(request.naam()),
                new Categorie(request.categorie()),
                new Prijs(BigDecimal.valueOf(request.prijs())),
                request.voorraad()));
        return ProductResponse.toRest(updated);
    }

    @PutMapping("/{id}/voorraad")
    public ProductResponse updateVoorraad(@PathVariable String id, @RequestBody int voorraad) {
        var updated = updateVoorraad.updateVoorraad(new UpdateVoorraadCommand(new ProductId(id), voorraad));
        if (updated.isEmpty()) {
            throw new IllegalArgumentException("Product voor dit Id niet gevonden");
        }
        return ProductResponse.toRest(updated.get());
    }

    @GetMapping
    public List<ProductResponse> list() {
        return productQuery.findAll().stream()
                .map(ProductResponse::toRest)
                .collect(Collectors.toList());
    }
}