package be.itenium.sockstore.service;

import java.util.*;

import be.itenium.sockstore.model.Product;
import be.itenium.sockstore.model.rest.ProductDto;
import be.itenium.sockstore.repository.ProductRepository;
import jakarta.transaction.Transactional;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@AllArgsConstructor
public class ProductService {

    ProductRepository productRepository;

    @Transactional
    public ProductDto create(ProductDto productDto) {
        return Product.toProductDto(productRepository.save(ProductDto.toProduct(productDto)));
    }

    @Transactional
    public ProductDto update(String id, ProductDto productDto) {
        Product productEntity = getProduct(id);
        productEntity.setTitel(productDto.titel());
        productEntity.setOmschrijving(productDto.omschrijving());
        productEntity.setPrijs(productDto.prijs());

        return Product.toProductDto(productRepository.save(productEntity));

    }

    public List<ProductDto> findAll() {
        return productRepository.findAll().stream().map(Product::toProductDto)
                .toList();
    }

    public ProductDto findById(String id) {
        return Product.toProductDto(getProduct(id));
    }

    private Product getProduct(String id) {
        Optional<Product> product = productRepository.findById(id);
        if(product.isPresent()){
            return product.get();
        } else {
            throw new NoSuchElementException("Product with id " + id + " not found");
        }
    }

    @Transactional
    public boolean delete(String id) {
        if (productRepository.existsById(id)) {
            productRepository.deleteById(id);
            return true;
        }
        return false;
    }

    @Transactional
    public ProductDto verkoop(String id) {
        Product product = getProduct(id);
        if (product.getVoorraad() > 0) {
            product.setVoorraad(product.getVoorraad() - 1);
            return Product.toProductDto(productRepository.save(product));
        }else{
            throw new RuntimeException("Product with id " + id + " has no stock left");
        }
    }
}
