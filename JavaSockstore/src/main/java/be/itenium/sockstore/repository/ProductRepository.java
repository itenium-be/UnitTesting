package be.itenium.sockstore.repository;

import be.itenium.sockstore.model.Product;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ProductRepository extends JpaRepository<Product, String> {

    // Hier kunnen we eventueel custom query methodes toevoegen
    // Bijvoorbeeld: List<Product> findByTitelContaining(String titel);
}
