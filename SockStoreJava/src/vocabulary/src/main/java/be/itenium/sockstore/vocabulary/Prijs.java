package be.itenium.sockstore.vocabulary;

import java.math.BigDecimal;

public record Prijs(BigDecimal value) {
    public Prijs {
        if (value == null || value.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException("Prijs moet positief zijn");
        }
    }
}