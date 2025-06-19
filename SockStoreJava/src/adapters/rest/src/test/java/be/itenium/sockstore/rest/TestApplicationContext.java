package be.itenium.sockstore.rest;

import be.itenium.sockstore.domain.ProductAggregate;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.vocabulary.ProductId;
import org.springframework.boot.test.context.TestConfiguration;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Primary;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.concurrent.ConcurrentHashMap;

@TestConfiguration
public class TestApplicationContext {

    @Bean
    @Primary
    public ProductPort inMemoryProductPort() {
        return new InMemoryProductPort();
    }

    // In-memory implementatie voor testen
    public static class InMemoryProductPort implements ProductPort {
        private final Map<String, ProductAggregate> products = new ConcurrentHashMap<>();
        private int idCounter = 1;

        @Override
        public ProductAggregate save(ProductAggregate product) {
            if (product.getId() == null) {
                // Nieuwe product - genereer ID
                ProductId newId = new ProductId(String.valueOf(idCounter++));
                ProductAggregate newProduct = new ProductAggregate(
                        newId,
                        product.getNaam(),
                        product.getCategorie(),
                        product.getPrijs(),
                        product.getVoorraad()
                );
                products.put(newId.value(), newProduct);
                return newProduct;
            } else {
                // Bestaande product updaten
                products.put(product.getId().value(), product);
                return product;
            }
        }

        @Override
        public Optional<ProductAggregate> findById(ProductId id) {
            return Optional.ofNullable(products.get(id.value()));
        }

        @Override
        public List<ProductAggregate> findAll() {
            return new ArrayList<>(products.values());
        }
    }
}