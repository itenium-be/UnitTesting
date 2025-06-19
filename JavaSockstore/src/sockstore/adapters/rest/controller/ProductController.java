package be.itenium.sockstore.adapters.rest.controller;

import be.itenium.sockstore.adapters.rest.dto.ProductResponse;
import be.itenium.sockstore.adapters.rest.dto.SaveProductRequest;
import be.itenium.sockstore.application.api.ProductService;
import be.itenium.sockstore.vocabulary.model.ProductId;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/producten")
public class ProductController {
    private final ProductService productService;

    public ProductController(ProductService service) {
        this.productService = service;
    }

    @PostMapping
    public ProductResponse create(@RequestBody SaveProductRequest request) {
        var product = request.fromRest();
        var saved = productService.save(product);
        return ProductResponse.toRest(saved);
    }

    @PutMapping("/{id}")
    public ProductResponse update(@PathVariable String id, @RequestBody SaveProductRequest request) {
        var product = request.fromRestWithId(id);
        var updated = productService.save(product);
        return ProductResponse.toRest(updated);
    }

    @PutMapping("/{id}/voorraad")
    public ProductResponse updateVoorraad(@PathVariable String id, @RequestBody int voorraad) {
        var updated = productService.updateVoorraad(new ProductId(id), voorraad);
        return ProductResponse.toRest(updated);
    }

    @GetMapping
    public List<ProductResponse> list() {
        return productService.findAll().stream()
                .map(ProductResponse::toRest)
                .collect(Collectors.toList());
    }
}