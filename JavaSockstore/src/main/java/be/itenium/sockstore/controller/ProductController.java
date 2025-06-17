package be.itenium.sockstore.controller;

import be.itenium.sockstore.model.rest.ProductDto;
import be.itenium.sockstore.service.ProductService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/producten")
public class ProductController {
    private final ProductService service;

    public ProductController(ProductService service) {
        this.service = service;
    }

    @PostMapping
    public ProductDto create(@RequestBody ProductDto product) {
        return service.create(product);
    }

    @PutMapping("/{id}")
    public ProductDto update(@PathVariable String id, @RequestBody ProductDto product) {
        return service.update(id, product);
    }

    @GetMapping
    public List<ProductDto> findAll() {
        return service.findAll();
    }

    @GetMapping("/{id}")
    public ProductDto findById(@PathVariable String id) {
        return service.findById(id);
    }

    @DeleteMapping("/{id}")
    public void delete(@PathVariable String id) {
        service.delete(id);
    }

    @PostMapping("/{id}/verkoop")
    public ProductDto verkoop(@PathVariable String id) {
        return service.verkoop(id);
    }
}