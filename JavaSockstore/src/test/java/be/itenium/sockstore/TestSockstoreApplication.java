package be.itenium.sockstore;

import org.springframework.boot.SpringApplication;

public class TestSockstoreApplication {

    public static void main(String[] args) {
        SpringApplication.from(SockstoreApplication::main).with(TestcontainersConfiguration.class).run(args);
    }

}
