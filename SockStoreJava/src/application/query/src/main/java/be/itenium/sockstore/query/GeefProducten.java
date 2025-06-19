package be.itenium.sockstore.query;

import be.itenium.sockstore.api.ProductQuery;
import be.itenium.sockstore.domain.ProductAggregate;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.vocabulary.Product;
import be.itenium.sockstore.vocabulary.ProductId;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

public class GeefProducten implements ProductQuery {
    private final ProductPort productPort;

    public GeefProducten(ProductPort productPort) {
        this.productPort = productPort;
    }

    @Override
    public Optional<Product> findById(ProductId id) {
        var product = productPort.findById(id);
        return product.map(ProductAggregate::toProduct);
    }

    @Override
    public List<Product> findAll() {
        return productPort.findAll().stream().map(ProductAggregate::toProduct).collect(Collectors.toList());
    }
}
