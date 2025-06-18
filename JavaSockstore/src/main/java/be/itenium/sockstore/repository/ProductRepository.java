package be.itenium.sockstore.repository;

import be.itenium.sockstore.model.Product;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Collection;

@Repository
public interface ProductRepository extends JpaRepository<Product, String> {
    Collection<Product> findByTitelContainingIgnoreCase(String title);
}
