package be.itenium.sockstore.rest;

import be.itenium.sockstore.rest.dto.ProductResponse;
import be.itenium.sockstore.rest.dto.SaveProductRequest;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.test.context.ActiveProfiles;

import java.math.BigDecimal;

import static org.assertj.core.api.Assertions.assertThat;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
@ActiveProfiles("test")
public class ProductRestTest {

    @Autowired
    private TestRestTemplate restTemplate;

    @Test
    void createProduct_and_fetch_it() {
        // Arrange
        var request = new SaveProductRequest(
                "Sokken Blauw",
                "Sport",
                12.99,
                100
        );

        // Act
        ResponseEntity<ProductResponse> response = restTemplate.postForEntity(
                "/producten",
                request,
                ProductResponse.class
        );

        // Assert
        assertThat(response.getStatusCode()).isEqualTo(HttpStatus.OK);
        assertThat(response.getBody()).isNotNull();
        assertThat(response.getBody().id()).isNotNull();
        assertThat(response.getBody().naam()).isEqualTo("Sokken Blauw");
    }

}