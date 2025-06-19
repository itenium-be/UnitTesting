package be.itenium.sockstore.adapters.rdbms;

import be.itenium.sockstore.adapters.rdbms.entities.ProductEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Collection;

@Repository
public interface ProductRepository extends JpaRepository<ProductEntity, String> {
}
