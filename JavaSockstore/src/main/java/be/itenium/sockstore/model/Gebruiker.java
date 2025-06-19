package be.itenium.sockstore.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import lombok.Data;

@Data
@Entity
public class Gebruiker {
    @Id
    @GeneratedValue(generator = "uuid")
    private String id;
    private String naam;
    private String email;
    private String wachtwoord;
    private String adres;

}
