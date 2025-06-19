package acceptance;

import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.*;

import static org.assertj.core.api.Assertions.*;

import java.math.BigDecimal;
import java.util.UUID;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
@ActiveProfiles("test")
public class ProductAcceptanceTest {

    @Autowired
    private TestRestTemplate restTemplate;

    @Test
    void createProduct_and_fetch_it() {
        // Arrange
        var request = new ProductRequest(
                null,
                "Sokken Blauw",
                "Sport",
                new BigDecimal("12.99"),
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
        assertThat(response.getBody().naam()).isEqualTo("Sokken Blauw");
    }

    // DTOs (or import them from shared test utils)
    public record ProductRequest(String id, String naam, String categorie, BigDecimal prijs, int voorraad) {
    }

    public record ProductResponse(String id, String naam, String categorie, BigDecimal prijs, int voorraad) {
    }
}