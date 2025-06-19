package be.itenium.sockstore.adapters.rest.dto;

public record UpdateStockRequest(String id, Integer voorraad) {
    public UpdateStockRequest {
        if (voorraad == null) {
            voorraad = 0;
        }
        if (id == null || id.isBlank()) {
            throw new IllegalArgumentException("Id mag niet leeg zijn");
        }
        if (voorraad < 0) {
            throw new IllegalArgumentException("Voorraad mag niet negatief zijn.");
        }
    }
}