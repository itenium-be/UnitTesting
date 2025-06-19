package be.itenium.sockstore.acceptance;

import be.itenium.sockstore.api.*;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.query.GeefProducten;
import be.itenium.sockstore.usecases.*;
import org.springframework.boot.test.context.TestConfiguration;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Primary;

@TestConfiguration
public class AcceptanceTestContext {
    @Bean
    @Primary
    public MockProductPort mockProductPort() {
        return new MockProductPort();
    }


    @Bean
    @Primary
    public CreateProduct createProductUseCase(ProductPort productPort) {
        return new CreateProductUseCase(productPort);
    }

    @Bean
    @Primary
    public UpdateProduct updateProductUseCase(ProductPort productPort) {
        return new UpdateProductUseCase(productPort);
    }

    @Bean
    @Primary
    public UpdateVoorraad updateVoorraadUseCase(ProductPort productPort) {
        return new UpdateVoorraadUseCase(productPort);
    }

    @Bean
    @Primary
    public ProductQuery productQuery(ProductPort productPort) {
        return new GeefProducten(productPort);
    }
}