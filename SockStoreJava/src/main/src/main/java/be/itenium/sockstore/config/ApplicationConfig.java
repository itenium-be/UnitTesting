package be.itenium.sockstore.config;

import be.itenium.sockstore.api.CreateProduct;
import be.itenium.sockstore.api.ProductQuery;
import be.itenium.sockstore.api.UpdateProduct;
import be.itenium.sockstore.api.UpdateVoorraad;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.query.GeefProducten;
import be.itenium.sockstore.rdbms.ProductRepository;
import be.itenium.sockstore.rdbms.ProductRepositoryAdapter;
import be.itenium.sockstore.usecases.CreateProductUseCase;
import be.itenium.sockstore.usecases.UpdateProductUseCase;
import be.itenium.sockstore.usecases.UpdateVoorraadUseCase;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class ApplicationConfig {
    @Bean
    public ProductPort productPort(ProductRepository repository) {
        return new ProductRepositoryAdapter(repository);
    }

    @Bean
    public ProductQuery productQuery(ProductPort productPort) {
        return new GeefProducten(productPort);
    }

    @Bean
    public CreateProduct createProduct(ProductPort productPort) {
        return new CreateProductUseCase(productPort);
    }

    @Bean
    public UpdateProduct updateProduct(ProductPort productPort) {
        return new UpdateProductUseCase(productPort);
    }

    @Bean
    public UpdateVoorraad updateVoorraad(ProductPort productPort) {
        return new UpdateVoorraadUseCase(productPort);
    }
}
