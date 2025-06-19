package be.itenium.sockstore.adapters.rdbms.entities;

import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class GebruikerEntity {
    @Id
    private String id;
    private String naam;
    private String email;
    private String wachtwoord;
    private String adres;
}
